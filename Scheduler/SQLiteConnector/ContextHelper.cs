using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF6Connector
{
    class ContextHelper
    {
        public ContextHelper()
		{
			Migrations = new Dictionary<int, IList<string>>();

			MigrationVersion1();
		    MigrationVersion2();
		}

		public Dictionary<int, IList<string>> Migrations { get; set; }

		private void MigrationVersion1()
		{
			IList<string> steps = new List<string>();

		    steps.Add(@"CREATE TABLE IF NOT EXISTS ""telephones2clients"" ([idtelephones2clients] INTEGER PRIMARY KEY, [telid] int, [clid] int, FOREIGN KEY (telid) REFERENCES ""telephones""(idtelephones) ON DELETE CASCADE, FOREIGN KEY (clid) REFERENCES ""clients""(idclients) ON DELETE CASCADE)");
		    steps.Add(@"CREATE TABLE IF NOT EXISTS ""telephones"" ([idtelephones] INTEGER PRIMARY KEY, [telephonescol] varchar NOT NULL)");
		    steps.Add(@"CREATE TABLE IF NOT EXISTS ""specializations2specialist"" ([idspecializations2specialist] INTEGER PRIMARY KEY, [specialization] int NOT NULL, [specialist] int NOT NULL, FOREIGN KEY (specialization) REFERENCES ""specializations""(idspecializations), FOREIGN KEY (specialist) REFERENCES ""specialists""(idspecialists) ON DELETE CASCADE)");
		    steps.Add(@"CREATE TABLE IF NOT EXISTS ""specializations"" ([idspecializations] INTEGER PRIMARY KEY, [name] varchar)");
		    steps.Add(@"CREATE TABLE IF NOT EXISTS ""specialists"" ([idspecialists] INTEGER PRIMARY KEY, [name] varchar, [notworking] tinyint NOT NULL)");
		    steps.Add(@"CREATE TABLE IF NOT EXISTS ""specialistDuty"" ([idspecialistDuty] INTEGER PRIMARY KEY, [dutytimestart] integer NOT NULL, [dutytimeend] integer NOT NULL, [specialistid] int NOT NULL, [supplimentary] bit NOT NULL, FOREIGN KEY (specialistid) REFERENCES ""specialists""(idspecialists) ON DELETE CASCADE)");
		    steps.Add(@"CREATE TABLE IF NOT EXISTS ""specialist2clientprice"" ([idspecialist2clientcost] INTEGER PRIMARY KEY, [specid] int NOT NULL, [clid] int NOT NULL, [price] integer NOT NULL)");
		    steps.Add(@"CREATE TABLE IF NOT EXISTS ""receptions"" ([idreceptions] INTEGER PRIMARY KEY, [clientid] integer NOT NULL, [specialistid] integer NOT NULL, [cabinetid] integer NOT NULL, [specializationid] integer NOT NULL, [isrented] bit NOT NULL, [isSpecialRent] bit NOT NULL, [timestart] integer NOT NULL, [timeend] integer NOT NULL, [timedate] datetime NOT NULL, [administrator] nvarchar, [receptionDidNotTakePlace] bit NOT NULL, [price] int, [comment] nvarchar)");
		    steps.Add(@"CREATE TABLE IF NOT EXISTS ""clients"" ([idclients] INTEGER PRIMARY KEY, [name] varchar, [comment] varchar, [email] nvarchar, [blacklisted] bit NOT NULL, [needSms] bit NOT NULL, [balance] int NOT NULL, [clientType] int NOT NULL, [administrator] nvarchar)");
		    steps.Add(@"CREATE TABLE IF NOT EXISTS ""clientgenerallyparams"" ([idclientGenerallyParams] INTEGER PRIMARY KEY, [clientId] integer NOT NULL, [generallyTime] integer NOT NULL, [generallyPrice] integer NOT NULL)");
		    steps.Add(@"CREATE TABLE IF NOT EXISTS ""cabinet"" ([idcabinet] INTEGER PRIMARY KEY, [name] varchar NOT NULL, [availability] integer NOT NULL, [commentOnly] bit NOT NULL)");
		    steps.Add(@"CREATE INDEX IF NOT EXISTS ""IX_telephones2clients_telid"" ON ""telephones2clients"" (telid)");
		    steps.Add(@"CREATE INDEX IF NOT EXISTS ""IX_telephones2clients_clid"" ON ""telephones2clients"" (clid)");
		    steps.Add(@"CREATE INDEX IF NOT EXISTS ""IX_specializations2specialist_specialization"" ON ""specializations2specialist"" (specialization)");
		    steps.Add(@"CREATE INDEX IF NOT EXISTS ""IX_specializations2specialist_specialist"" ON ""specializations2specialist"" (specialist)");
		    steps.Add(@"CREATE INDEX IF NOT EXISTS ""IX_specialistDuty_specialistid"" ON ""specialistDuty"" (specialistid)");

            Migrations.Add(1, steps);
		}

        private void MigrationVersion2()
        {
            IList<string> steps = new List<string>();

            steps.Add(@"CREATE TABLE IF NOT EXISTS ""administratorDuty"" ([idadministratorDuty] INTEGER PRIMARY KEY, [dutytimestart] integer NOT NULL, [dutytimeend] integer NOT NULL, [administratorid] int NOT NULL, [supplimentary] bit NOT NULL, FOREIGN KEY (administratorid) REFERENCES ""administrators""(idadministrators) ON DELETE CASCADE)");
            steps.Add(@"CREATE TABLE IF NOT EXISTS ""administrators"" ([idadministrators] INTEGER PRIMARY KEY, [name] varchar, [notworking] tinyint NOT NULL)");
            steps.Add(@"ALTER TABLE ""clients"" ADD COLUMN [isActive] tinyint NOT NULL DEFAULT 0");

            Migrations.Add(2, steps);
        }
    }
}
