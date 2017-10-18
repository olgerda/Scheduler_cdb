using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler_Common_Interfaces;
using Scheduler_Controls_Interfaces;
using Scheduler_DBobjects_Intefraces;
using Scheduler_Forms_Interfaces;
using EF6Connector.Model;
using System.Data.Entity;

namespace EF6Connector
{
    public class Connector : Scheduler_DBconnector
    {
        private EF6Connector.Model.EF6Model context;
        public Connector(IFactory factory) : base(factory)
        {
            (new ContextMigration()).UpdateIfNeeded();

            context = new Model.EF6Model();
            //MySQL need to be UTF8!!!
            //https://stackoverflow.com/questions/1008287/illegal-mix-of-collations-mysql-error
            /*
             SET collation_connection = 'utf8_general_ci'

then for your databases

ALTER DATABASE your_database_name CHARACTER SET utf8 COLLATE utf8_general_ci

ALTER TABLE your_table_name CONVERT TO CHARACTER SET utf8 COLLATE utf8_general_ci
             
             */
#if DEBUG
            //context.Database.Log = s => Console.WriteLine(s);
#endif
        }

        public override string ConnectionString
        {
            get { return context.Database.Connection.ConnectionString; }
            set { }
        }

        public override void AddClient(IClient client)
        {
            if (client.ID > 0)
            {
                //throw new Exception($"При создании нового клиента идентификатор не может быть положительным! (<{client.ID}>)");
                return;
            }

            var obj = new client()
            {
                name = client.Name,
                blacklisted = client.BlackListed,
                comment = client.Comment,
                administrator = client.Administrator,
                email = client.EMail,
                balance = client.Balance,
                needSms = client.NeedSMS,
                clientType = client.ClientType,
                isActive = client.Active

            };

            context.clients.Add(obj);

            var telsInDB = context.telephones.ToDictionary(x => x.telephonescol);

            foreach (var num in client.Telephones)
            {
                telephone tel = null;
                if (!telsInDB.ContainsKey(num))
                {
                    tel = new telephone() { telephonescol = num };
                    context.telephones.Add(tel);
                }
                else
                    tel = telsInDB[num];

                var tel2cli = new telephones2clients() { telephone = tel, client = obj };
                context.telephones2clients.Add(tel2cli);
            }

            context.SaveChanges();

            context.clientgenerallyparams.Add(new clientgenerallyparam()
            {
                clientId = obj.idclients,
                generallyPrice = client.GenerallyPrice,
                generallyTime = client.GenerallyTime.Ticks
            });
            context.SaveChanges();
            client.ID = obj.idclients;
        }

        public override void UpdateClientData(IClient client)
        {
            var cli = context.clients.FirstOrDefault(x => x.idclients == client.ID);

            var cliGenerallParams = context.clientgenerallyparams.FirstOrDefault(x => x.clientId == client.ID);
            if (cli == null || cliGenerallParams == null)
            {
                throw new NullReferenceException($"Client with id <{client.ID}> not found.");
            }

            cli.blacklisted = client.BlackListed;
            cli.comment = client.Comment;
            cli.name = client.Name;
            cli.administrator = client.Administrator;
            cli.needSms = client.NeedSMS;
            cli.email = client.EMail;
            cli.balance = client.Balance;
            cli.clientType = client.ClientType;
            cli.isActive = client.Active;

            var oldCliTelephones = context.telephones2clients.Include("client")
                .Where(x => x.client.idclients == cli.idclients)
                .Select(x => new { t2c = x, telnum = x.telephone.telephonescol })
                .ToDictionary(x => x.telnum, x => x.t2c);
            var telephones = context.telephones.ToDictionary(x => x.telephonescol);
            foreach (var num in client.Telephones)
            {
                if (!oldCliTelephones.ContainsKey(num))
                {
                    telephone telnum;
                    if (telephones.ContainsKey(num))
                        telnum = telephones[num];
                    else
                        telnum = new telephone() { telephonescol = num };

                    context.telephones2clients.Add(new telephones2clients() { telephone = telnum, client = cli });
                }
                else
                {
                    oldCliTelephones.Remove(num);
                }
            }
            //remove deprecated links between telnumbers and client,
            foreach (var tel2remove in oldCliTelephones)
                context.telephones2clients.Remove(tel2remove.Value);
            TelephoneWOLinksCleanup();

            cliGenerallParams.generallyPrice = client.GenerallyPrice;
            cliGenerallParams.generallyTime = client.GenerallyTime.Ticks;

            context.SaveChanges();
        }

        public override void RemoveClient(IClient client)
        {
            var cli = context.clients.FirstOrDefault(x => x.idclients == client.ID);
            if (cli == null)
            {
                throw new NullReferenceException($"Client with id <{client.ID}> not found.");
            }

            context.clients.Remove(cli);

            TelephoneWOLinksCleanup();

            context.clientgenerallyparams.Remove(context.clientgenerallyparams.First(x => x.clientId == cli.idclients));

            context.SaveChanges();
        }

        private void TelephoneWOLinksCleanup()
        {
            context.telephones.RemoveRange(context.telephones.Where(x => x.telephones2clients.Count == 0));
        }

        protected override IClientList AllClientsInternal(int clientType = 0)
        {
            List<IClient> list = new List<IClient>();
            foreach (var dbcli in context.clients.Where(x => x.clientType == clientType).Select(
                x => new
                {
                    x.idclients,
                    x.name,
                    x.blacklisted,
                    x.comment,
                    x.administrator,
                    telephones = x.telephones2clients.Select(y => y.telephone.telephonescol),
                    x.email,
                    x.needSms,
                    x.balance,
                    x.clientType,
                    x.isActive
                }))
            {
                var generallParams = context.clientgenerallyparams
                    .First(x => x.clientId == dbcli.idclients);
                var client = EntityFactory.NewClient();
                client.BlackListed = dbcli.blacklisted;
                client.Comment = dbcli.comment;
                client.ID = dbcli.idclients;
                client.Name = dbcli.name;
                client.Administrator = dbcli.administrator;
                client.Telephones = new HashSet<string>(dbcli.telephones);
                client.GenerallyPrice = (int)generallParams.generallyPrice;
                client.GenerallyTime = TimeSpan.FromTicks(generallParams.generallyTime);
                client.Balance = dbcli.balance;
                client.NeedSMS = dbcli.needSms;
                client.EMail = dbcli.email;
                client.ClientType = dbcli.clientType;
                client.Active = dbcli.isActive;

                list.Add(client);
            };

            return AllClients(list, ListItemAddHandler, ListItemChangedHandler, ListItemRemoveHandler);
        }

        public override void AddSpecialist(ISpecialist specialist)
        {
            if (specialist.ID > 0)
            {
                //throw new Exception($"При создании нового специалиста идентификатор не может быть положительным! (<{specialist.ID}>)");
                return;
            }

            var dbspec = new specialist();
            dbspec.name = specialist.Name;
            dbspec.notworking = (byte)(specialist.NotWorking ? 1 : 0);

            var specs = context.specializations.Select(x => new { x.name, dbSpecialization = x }).ToDictionary(x => x.name);

            foreach (var s in specialist.Specialisations)
            {
                specialization spec;
                if (!specs.ContainsKey(s))
                {
                    spec = new specialization() { name = s };
                    context.specializations.Add(spec);
                }
                else
                    spec = specs[s].dbSpecialization;
                context.specializations2specialist.Add(
                    new specializations2specialist() { specialist1 = dbspec, specialization1 = spec });
            }

            context.specialists.Add(dbspec);

            context.SaveChanges();
            specialist.ID = dbspec.idspecialists;
        }

        public override void UpdateSpecialistData(ISpecialist specialist)
        {
            var dbspec = context.specialists.Include("specializations2specialist").FirstOrDefault(x => x.idspecialists == specialist.ID);

            if (dbspec == null)
            {
                throw new NullReferenceException($"Specialist with id <{specialist.ID}> not found.");
            }

            dbspec.name = specialist.Name;
            dbspec.notworking = (byte)(specialist.NotWorking ? 1 : 0);

            //TODO: обработка навыков
            var dbSpecializations = context.specializations.ToDictionary(x => x.name);
            foreach (var specSpecialization in specialist.Specialisations)
            {
                if (!dbSpecializations.ContainsKey(specSpecialization))
                    throw new Exception($"Specialization {specSpecialization} not found in db."); //should be never thrown.
                if (!dbspec.specializations2specialist.Any(
                    x => x.specialization1 == dbSpecializations[specSpecialization]))
                {
                    var link = new specializations2specialist()
                    {
                        specialization1 = dbSpecializations[specSpecialization],
                        specialist1 = dbspec
                    };
                    context.specializations2specialist.Add(link);
                }
            }

            context.SaveChanges();
        }

        public override void RemoveSpecialist(ISpecialist specialist)
        {
            var dbspec = context.specialists.Include("specializations2specialist").FirstOrDefault(x => x.idspecialists == specialist.ID);

            if (dbspec == null)
            {
                throw new NullReferenceException($"Specialist with id <{specialist.ID}> not found.");
            }

            context.specialists.Remove(dbspec);

            context.SaveChanges();
        }

        protected override ISpecialistList AllSpecialistsInternal()
        {
            var result = EntityFactory.NewSpecialistList();

            foreach (var dbsp in context.specialists.Select(
                x => new
                {
                    x.idspecialists,
                    x.name,
                    x.notworking,
                    specializations = x.specializations2specialist.Select(y => y.specialization1.name)
                }))
            {
                var spec = EntityFactory.NewSpecialist();
                spec.ID = dbsp.idspecialists;
                spec.Name = dbsp.name;
                spec.NotWorking = dbsp.notworking == 1;
                spec.Specialisations = new HashSet<string>(dbsp.specializations);

                result.List.Add(spec);
            };

            result.OnItemAdded += ListItemAddHandler;
            result.OnItemChanged += ListItemChangedHandler;
            result.OnItemRemoved += ListItemRemoveHandler;

            return result;
        }

        public override void AddSpecialization(string specialization)
        {
            if (context.specializations.FirstOrDefault(x => x.name == specialization) != null)
            {
                //throw new Exception($"Specialization {specialization} already in db.");
                return;
            }

            context.specializations.Add(new Model.specialization() { name = specialization });
            context.SaveChanges();
        }

        public override void RemoveSpecialization(string specialization)
        {
            Model.specialization spn = context.specializations.FirstOrDefault(x => x.name == specialization);
            if (spn == null)
            {
                throw new NullReferenceException($"Specialization {specialization} not found in db.");
            }

            context.specializations.Remove(spn);
            context.SaveChanges();
        }

        public override ISpecializationList AllSpecializations()
        {
            var result = EntityFactory.NewSpecializationList();

            foreach (var dbspn in context.specializations.Select(x => new { x.name }))
            {
                result.SpecializationList.Add(dbspn.name);
            }

            result.OnItemAdded += ListItemAddHandler;
            result.OnItemRemoved += ListItemRemoveHandler;
            return result;
        }

        public override void AddCabinet(ICabinet cabinet)
        {
            if (cabinet.ID > 0)
            {
                //throw new Exception($"При создании нового кабинета идентификатор не может быть положительным! (<{cabinet.ID}>)");
                return; //this cab already in DB
            }

            var dbcab = new cabinet()
            {
                name = cabinet.Name,
                availability = cabinet.Availability.ToByte(),
                commentOnly = cabinet.CommentOnly
            };

            context.cabinets.Add(dbcab);

            context.SaveChanges();
            cabinet.ID = dbcab.idcabinet;
        }

        public override void UpdateCabinetData(ICabinet cabinet)
        {
            var dbcab = context.cabinets.FirstOrDefault(x => x.idcabinet == cabinet.ID);
            if (dbcab == null)
                throw new NullReferenceException($"Cabinet with id <{cabinet.ID}> not found in db.");
            dbcab.name = cabinet.Name;
            dbcab.availability = cabinet.Availability.ToByte();
            dbcab.commentOnly = cabinet.CommentOnly;
            context.SaveChanges();
        }

        public override void RemoveCabinet(ICabinet cabinet)
        {
            var dbcab = context.cabinets.FirstOrDefault(x => x.idcabinet == cabinet.ID);
            if (dbcab == null)
                throw new NullReferenceException($"Cabinet with id <{cabinet.ID}> not found in db.");
            context.cabinets.Remove(dbcab);
            context.SaveChanges();
        }

        protected override ICabinetList AllCabinetsInternal()
        {
            var result = EntityFactory.NewCabinetList();
            result.OnItemAdded += ListItemAddHandler;
            result.OnItemChanged += ListItemChangedHandler;
            result.OnItemRemoved += ListItemRemoveHandler;

            foreach (var dbcab in context.cabinets.Select(x => new { x.idcabinet, x.name, x.availability, x.commentOnly }).AsEnumerable())
            {
                var cab = EntityFactory.NewCabinet();
                cab.Name = dbcab.name;
                cab.ID = dbcab.idcabinet;
                cab.Availability = dbcab.availability == 1;
                cab.CommentOnly = dbcab.commentOnly;
                result.Add(cab);
            }

            return result;
        }

        public override List<IEntity> GetReceptionsFromDate(DateTime date)
        {
            var result = new List<IEntity>();
            //костыль! Православно обходится созданием соответствующей функции
            var dayBefore = date.Date.AddDays(-1);
            var dayafter = date.Date.AddDays(1);
            foreach (var dbreception in context.receptions.Where(x => x.timedate > dayBefore && x.timedate < dayafter).AsEnumerable().Where(x => x.timedate.Date == date.Date))
            {
                result.Add(ConvertToEntity(dbreception));
            }

            return result;
        }

        public override List<IEntity> GetReceptionsBetweenDates(DateTime startDate, DateTime endDate)
        {
            var result = new List<IEntity>();
            var dayBefore = startDate.Date.AddDays(-1);
            var dayAfter = endDate.Date.AddDays(1);
            foreach (var dbreception in context.receptions.Where(x => x.timedate >= dayBefore && x.timedate <= dayAfter).AsEnumerable()
                .Where(x => x.timedate.Date >= startDate.Date && x.timedate.Date <= endDate.Date))
            {
                result.Add(ConvertToEntity(dbreception));
            }

            return result;
        }

        public override List<IReception> GetReceptionsForClient(IClient client)
        {
            var result = new List<IReception>();

            foreach (var dbreception in context.receptions.Where(x => x.clientid == client.ID))
            {
                result.Add(ConvertToEntity(dbreception));
            }

            return result;
        }

        public override Dictionary<int, int> GetCostsForSpecialist(ISpecialist spec)
        {
            return context.specialist2clientprice.Where(x => x.specid == spec.ID)
                .Select(x => new { x.price, x.clid })
                .ToDictionary(x => x.clid, x => (int)x.price);
        }

        public override void AddReception(IEntity reception)
        {
            if (reception.ID > 0)
            {
                //throw new Exception($"При создании нового приёма идентификатор не может быть положительным! (<{reception.ID}>)");
                return;
            }
            var dbreception = new reception();

            dbreception.administrator = reception.Administrator;
            dbreception.cabinetid = reception.Cabinet.ID;
            dbreception.clientid = reception.Client?.ID ?? DEFAULTCLIENTID;
            dbreception.isrented = reception.Rent;
            dbreception.isSpecialRent = reception.SpecialRent;
            dbreception.specialistid = reception.Specialist?.ID ?? DEFAULTCLIENTID;
            var specid = 0;
            if (!reception.Rent)
            {
                specid = context.specializations.FirstOrDefault(x => x.name.Equals(reception.Specialization, StringComparison.OrdinalIgnoreCase))?.idspecializations ?? 0;
            }
            dbreception.specializationid = specid;
            dbreception.timedate = reception.ReceptionTimeInterval.Date;
            dbreception.timestart = reception.ReceptionTimeInterval.StartDate.TimeOfDay.Ticks;
            dbreception.timeend = reception.ReceptionTimeInterval.EndDate.TimeOfDay.Ticks;
            dbreception.comment = reception.Comment;
            dbreception.receptionDidNotTakePlace = reception.ReceptionDidNotTakePlace;
            dbreception.price = reception.Price;

            context.receptions.Add(dbreception);

            var currentSpecClientPrice =
                context.specialist2clientprice.FirstOrDefault(
                    x => x.specid == dbreception.specialistid && x.clid == dbreception.clientid);
            if (currentSpecClientPrice == null)
            {
                context.specialist2clientprice.Add(currentSpecClientPrice =
                    new specialist2clientprice()
                    {
                        specid = (int)dbreception.specialistid,
                        clid = (int)dbreception.clientid
                    });
                context.specialist2clientprice.Add(currentSpecClientPrice);
            }
            currentSpecClientPrice.price = reception.Price;

            context.SaveChanges();
            reception.ID = dbreception.idreceptions;
        }

        public override void UpdateReception(IEntity reception)
        {
            var dbreception = context.receptions.FirstOrDefault(x => x.idreceptions == reception.ID);
            if (dbreception == null)
                throw new Exception($"Reception with id <{reception.ID}> not found in db.");
            dbreception.clientid = reception.Client?.ID ?? DEFAULTCLIENTID;
            dbreception.administrator = reception.Administrator;
            dbreception.cabinetid = reception.Cabinet.ID;
            dbreception.isrented = reception.Rent;
            dbreception.isSpecialRent = reception.SpecialRent;
            dbreception.specialistid = reception.Specialist?.ID ?? DEFAULTCLIENTID;
            var specid = 0;

            specid = context.specializations.First(x => x.name.Equals(reception.Specialization, StringComparison.OrdinalIgnoreCase)).idspecializations;

            dbreception.specializationid = specid;
            dbreception.timedate = reception.ReceptionTimeInterval.Date;
            dbreception.timestart = reception.ReceptionTimeInterval.StartDate.TimeOfDay.Ticks;
            dbreception.timeend = reception.ReceptionTimeInterval.EndDate.TimeOfDay.Ticks;
            dbreception.comment = reception.Comment;
            dbreception.receptionDidNotTakePlace = reception.ReceptionDidNotTakePlace;
            dbreception.price = reception.Price;

            var currentSpecClientPrice =
                context.specialist2clientprice.FirstOrDefault(
                    x => x.specid == dbreception.specialistid && x.clid == dbreception.clientid);
            if (currentSpecClientPrice == null)
            {
                context.specialist2clientprice.Add(currentSpecClientPrice =
                    new specialist2clientprice()
                    {
                        specid = (int)dbreception.specialistid,
                        clid = (int)dbreception.clientid
                    });
                context.specialist2clientprice.Add(currentSpecClientPrice);
            }
            currentSpecClientPrice.price = reception.Price;

            context.SaveChanges();
        }

        public override void RemoveReception(IEntity reception)
        {
            var dbreception = context.receptions.FirstOrDefault(x => x.idreceptions == reception.ID);
            if (dbreception == null)
                throw new Exception($"Reception with id <{reception.ID}> not found in db.");
            context.receptions.Remove(dbreception);
            context.SaveChanges();
        }

        public override void MakeBackup(string filename)
        {
            throw new NotImplementedException("Функция не реализована.");
            //System.Xml.Serialization.XmlSerializer serializer =
            //    new System.Xml.Serialization.XmlSerializer(typeof(cabinet[]));
            //using (var filestream = System.IO.File.CreateText(filename + ".cabinets"))
            //    serializer.Serialize(filestream, context.cabinets.ToArray());
        }

        public override void RestoreBackup(string filename)
        {
            throw new NotImplementedException("Функция не реализована.");
        }

        public override bool CheckDBConnection(out string message)
        {
            var badstate = (context.Database.Connection.State &
                           (System.Data.ConnectionState.Closed |
                            System.Data.ConnectionState.Broken)) != 0;
            message = badstate ? "DB connection closed or broken." : null;
            return !badstate;
        }

        private T GetByID<T>(long id) where T : class, IDummy
        {
            T result = null;
            if (id == 0 || id == DEFAULTCLIENTID)
                return null;
            if (typeof(T) == typeof(IClient))
            {
                result = (T)(AllClients().List.FirstOrDefault(x => x.ID == id) ?? AllArendators().List.First(x => x.ID == id));
            }
            else if (typeof(T) == typeof(ISpecialist))
            {
                var list = AllSpecialists();
                result = (T)list.List.First(x => x.ID == id);
            }
            else if (typeof(T) == typeof(ICabinet))
            {
                var list = AllCabinets();
                result = (T)list.List.First(x => x.ID == id);
            }
            else if (typeof(T) == typeof(IAdministrator))
            {
                var list = AllAdministrators();
                result = (T)list.List.First(x => x.ID == id);
            }
            return result;
        }

        public void ListItemAddHandler(object item)
        {
            var client = item as IClient;
            if (client != null)
            {
                AddClient(client);
                return;
            }

            var specialist = item as ISpecialist;
            if (specialist != null)
            {
                AddSpecialist(specialist);
                return;
            }

            var cabinet = item as ICabinet;
            if (cabinet != null)
            {
                AddCabinet(cabinet);
                return;
            }

            var dts = item as ISpecialistDuty;
            if (dts != null)
            {
                AddSpecialistDuty(dts);
                return;
            }

            var dta = item as IAdministratorDuty;
            if (dta != null)
            {
                AddAdministratorDuty(dta);
                return;
            }

            var ad = item as IAdministrator;
            if (ad != null)
            {
                AddAdministrator(ad);
            }

            string specialization = item as string;
            if (specialization != null)
            {
                AddSpecialization(specialization);
                return;
            }
        }

        public void ListItemRemoveHandler(object item)// where T : Scheduler_Controls_Interfaces.IDummy
        {
            var client = item as IClient;
            if (client != null)
            {
                RemoveClient(client);
                return;
            }

            var specialist = item as ISpecialist;
            if (specialist != null)
            {
                RemoveSpecialist(specialist);
                return;
            }

            var cabinet = item as ICabinet;
            if (cabinet != null)
            {
                RemoveCabinet(cabinet);
                return;
            }

            var dt = item as ISpecialistDuty;
            if (dt != null)
            {
                RemoveSpecialistDuty(dt);
                return;
            }

            var dta = item as IAdministratorDuty;
            if (dta != null)
            {
                RemoveAdministratorDuty(dta);
                return;
            }

            var ad = item as IAdministrator;
            if (ad != null)
            {
                RemoveAdministrator(ad);
            }

            string specialization = item as string;
            if (specialization != null)
            {
                RemoveSpecialization(specialization);
                return;
            }
        }

        public void ListItemChangedHandler(object item)
        {
            var client = item as IClient;
            if (client != null)
            {
                UpdateClientData(client);
                return;
            }

            var specialist = item as ISpecialist;
            if (specialist != null)
            {
                UpdateSpecialistData(specialist);
                return;
            }

            var cabinet = item as ICabinet;
            if (cabinet != null)
            {
                UpdateCabinetData(cabinet);
                return;
            }

            var dt = item as ISpecialistDuty;
            if (dt != null)
            {
                UpdateSpecialistDuty(dt);
                return;
            }

            var dta = item as IAdministratorDuty;
            if (dta != null)
            {
                UpdateAdministratorDuty(dta);
                return;
            }
            var ad = item as IAdministrator;
            if (ad != null)
            {
                UpdateAdministratorData(ad);
            }
        }

        private IEntity ConvertToEntity(reception dbreception)
        {
            var ent = EntityFactory.NewEntity();
            ent.Administrator = dbreception.administrator;
            ent.Cabinet = GetByID<ICabinet>(dbreception.cabinetid);
            ent.Client = GetByID<IClient>(dbreception.clientid);
            ent.ID = dbreception.idreceptions;
            ent.Rent = dbreception.isrented;
            ent.Specialist = GetByID<ISpecialist>(dbreception.specialistid);
            ent.Specialization = context.specializations.FirstOrDefault(x => x.idspecializations == dbreception.specializationid)?.name ?? "NONE";
            ent.Comment = dbreception.comment ?? "";
            ent.ReceptionDidNotTakePlace = dbreception.receptionDidNotTakePlace;

            var timeinterval = EntityFactory.NewTimeInterval();
            timeinterval.SetStartEnd(dbreception.timedate.Date.Add(TimeSpan.FromTicks(dbreception.timestart)), dbreception.timedate.Date.Add(TimeSpan.FromTicks(dbreception.timeend)));
            ent.ReceptionTimeInterval = timeinterval;
            ent.Price = dbreception.price ?? 0;
            //TODO: тут плохо пахнет
            if (ent.Price == 0)
            {
                if (ent.Client == null || ent.Specialist == null)
                    ent.Price = 0;
                else
                    ent.Price = (int)(context.specialist2clientprice
                                           .FirstOrDefault(
                                               x => x.clid == ent.Client.ID && x.specid == ent.Specialist.ID)
                                           ?
                                           .price ?? ent.Client.GenerallyPrice);
            }
            return ent;
        }

        protected override ISpecialistDutyList AllSpecDutyInternal()
        {
            var result = EntityFactory.NewSpecialistDutyList();

            foreach (var dbdt in context.specialistDuties.Select(
                x => new
                {
                    x.idspecialistDuty,
                    x.specialistid,
                    x.dutytimestart,
                    x.dutytimeend,
                    x.supplimentary
                }))
            {
                var dt = EntityFactory.NewSpecialistDuty();
                dt.Named = GetByID<ISpecialist>(dbdt.specialistid);
                dt.Start = DateTime.FromBinary(dbdt.dutytimestart);
                dt.End = DateTime.FromBinary(dbdt.dutytimeend);
                dt.Supplimentary = dbdt.supplimentary;
                dt.ID = dbdt.idspecialistDuty;

                result.List.Add(dt);
            };

            result.OnItemAdded += ListItemAddHandler;
            result.OnItemChanged += ListItemChangedHandler;
            result.OnItemRemoved += ListItemRemoveHandler;

            return result;
        }

        public override void AddSpecialistDuty(ISpecialistDuty dt)
        {
            if (dt.ID > 0)
            {
                //throw new Exception($"При создании нового кабинета идентификатор не может быть положительным! (<{cabinet.ID}>)");
                return; //this cab already in DB
            }

            var dbdt = new specialistDuty()
            {
                specialistid = dt.Named.ID,
                dutytimestart = dt.Start.ToBinary(),
                dutytimeend = dt.End.ToBinary(),
                supplimentary = dt.Supplimentary
            };

            context.specialistDuties.Add(dbdt);

            context.SaveChanges();
            dt.ID = dbdt.idspecialistDuty;
        }

        public override void RemoveSpecialistDuty(ISpecialistDuty dt)
        {
            var dbdt = context.specialistDuties.FirstOrDefault(x => x.idspecialistDuty == dt.ID);
            if (dbdt == null)
                throw new NullReferenceException($"Cabinet with id <{dt.ID}> not found in db.");
            context.specialistDuties.Remove(dbdt);
            context.SaveChanges();
        }

        public override void UpdateSpecialistDuty(ISpecialistDuty dt)
        {
            var dbdt = context.specialistDuties.FirstOrDefault(x => x.idspecialistDuty == dt.ID);
            if (dbdt == null)
                throw new NullReferenceException($"Cabinet with id <{dt.ID}> not found in db.");
            dbdt.specialistid = dt.Named.ID;
            dbdt.supplimentary = dt.Supplimentary;
            dbdt.dutytimestart = dt.Start.ToBinary();
            dbdt.dutytimeend = dt.End.ToBinary();
            context.SaveChanges();
        }

        protected override IAdministratorDutyList AllAdmDutyInternal()
        {
            var result = EntityFactory.NewAdministratorDutyList();

            foreach (var dbdt in context.administratorDuties.Select(
                x => new
                {
                    x.idadministratorDuty,
                    x.administratorid,
                    x.dutytimestart,
                    x.dutytimeend,
                    x.supplimentary
                }))
            {
                var dt = EntityFactory.NewAdministratorDuty();
                dt.Named = GetByID<IAdministrator>(dbdt.administratorid);
                dt.Start = DateTime.FromBinary(dbdt.dutytimestart);
                dt.End = DateTime.FromBinary(dbdt.dutytimeend);
                dt.Supplimentary = dbdt.supplimentary;
                dt.ID = dbdt.idadministratorDuty;

                result.List.Add(dt);
            };

            result.OnItemAdded += ListItemAddHandler;
            result.OnItemChanged += ListItemChangedHandler;
            result.OnItemRemoved += ListItemRemoveHandler;

            return result;
        }

        public override void AddAdministratorDuty(IAdministratorDuty dt)
        {
            if (dt.ID > 0)
            {
                //throw new Exception($"При создании нового кабинета идентификатор не может быть положительным! (<{cabinet.ID}>)");
                return; //this cab already in DB
            }

            var dbdt = new administratorDuty()
            {
                administratorid = dt.Named.ID,
                dutytimestart = dt.Start.ToBinary(),
                dutytimeend = dt.End.ToBinary(),
                supplimentary = dt.Supplimentary
            };

            context.administratorDuties.Add(dbdt);

            context.SaveChanges();
            dt.ID = dbdt.idadministratorDuty;
        }

        public override void RemoveAdministratorDuty(IAdministratorDuty dt)
        {
            var dbdt = context.administratorDuties.FirstOrDefault(x => x.idadministratorDuty == dt.ID);
            if (dbdt == null)
                throw new NullReferenceException($"Cabinet with id <{dt.ID}> not found in db.");
            context.administratorDuties.Remove(dbdt);
            context.SaveChanges();
        }

        public override void UpdateAdministratorDuty(IAdministratorDuty dt)
        {
            var dbdt = context.administratorDuties.FirstOrDefault(x => x.idadministratorDuty == dt.ID);
            if (dbdt == null)
                throw new NullReferenceException($"Cabinet with id <{dt.ID}> not found in db.");
            dbdt.administratorid = dt.Named.ID;
            dbdt.supplimentary = dt.Supplimentary;
            dbdt.dutytimestart = dt.Start.ToBinary();
            dbdt.dutytimeend = dt.End.ToBinary();
            context.SaveChanges();
        }

        protected override IAdministratorList AllAdministratorsInternal()
        {
            var result = EntityFactory.NewAdministratorList();

            foreach (var dbsp in context.administrators.Select(
                x => new
                {
                    x.idadministrators,
                    x.name,
                    x.notworking
                }))
            {
                var spec = EntityFactory.NewAdministrator();
                spec.ID = dbsp.idadministrators;
                spec.Name = dbsp.name;
                spec.NotWorking = dbsp.notworking;

                result.List.Add(spec);
            };

            result.OnItemAdded += ListItemAddHandler;
            result.OnItemChanged += ListItemChangedHandler;
            result.OnItemRemoved += ListItemRemoveHandler;

            return result;
        }

        public void AddAdministrator(IAdministrator specialist)
        {
            if (specialist.ID > 0)
            {
                //throw new Exception($"При создании нового специалиста идентификатор не может быть положительным! (<{specialist.ID}>)");
                return;
            }

            var dbspec = new administrator();
            dbspec.name = specialist.Name;
            dbspec.notworking = specialist.NotWorking;

            var specs = context.specializations.Select(x => new { x.name, dbSpecialization = x }).ToDictionary(x => x.name);

            context.administrators.Add(dbspec);

            context.SaveChanges();
            specialist.ID = dbspec.idadministrators;
        }

        public void UpdateAdministratorData(IAdministrator specialist)
        {
            var dbspec = context.administrators.FirstOrDefault(x => x.idadministrators == specialist.ID);

            if (dbspec == null)
            {
                throw new NullReferenceException($"Administrator with id <{specialist.ID}> not found.");
            }

            dbspec.name = specialist.Name;
            dbspec.notworking = specialist.NotWorking;
            context.SaveChanges();
        }

        public void RemoveAdministrator(IAdministrator specialist)
        {
            var dbspec = context.administrators.FirstOrDefault(x => x.idadministrators == specialist.ID);

            if (dbspec == null)
            {
                throw new NullReferenceException($"Administrator with id <{specialist.ID}> not found.");
            }

            context.administrators.Remove(dbspec);

            context.SaveChanges();
        }

        public override int SpecialistGetReceptionCount(ISpecialist spec)
        {
            return context.receptions.Count(x => x.specialistid == spec.ID);
        }

        public override int SpecialistGetClientCount(ISpecialist spec)
        {
            return context.receptions.Where(x => x.specialistid == spec.ID).Select(x => x.clientid).Distinct().Count();
        }

        public override List<IReception> SpecialistGetReceptions(ISpecialist spec)
        {
            return context.receptions.Where(x => x.specialistid == spec.ID)
                .AsEnumerable()
                .Select(x => ConvertToEntity(x))
                .Cast<IReception>()
                .ToList();
        }

        public override List<IClient> SpecialistGetClients(ISpecialist spec)
        {
            return context.receptions.Where(x => x.specialistid == spec.ID)
                .Select(x => x.clientid)
                .Distinct()
                .AsEnumerable()
                .Select(x => AllClients().List.First(y => x == y.ID))
                .ToList();
        }
    }

    public static class Extensions
    {
        public static byte ToByte(this bool input)
        {
            return (byte)(input ? 1 : 0);
        }
    }
}
