﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler_DBobjects
{
    public class MainDatabase : Scheduler_DBobjects_Intefraces.IMainDataBase
    {
        List<Scheduler_DBobjects_Intefraces.IEntity> Scheduler_DBobjects_Intefraces.IMainDataBase.SelectReceptionsFromDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        Scheduler_Forms_Interfaces.ISpecialistList Scheduler_DBobjects_Intefraces.IMainDataBase.SpecialistList
        {
            get { throw new NotImplementedException(); }
        }

        Scheduler_Forms_Interfaces.IClientList Scheduler_DBobjects_Intefraces.IMainDataBase.ClientList
        {
            get { throw new NotImplementedException(); }
        }

        Scheduler_Controls_Interfaces.ISpecializationList Scheduler_DBobjects_Intefraces.IMainDataBase.SpecializationList
        {
            get { throw new NotImplementedException(); }
        }

        Scheduler_Forms_Interfaces.ICabinetList Scheduler_DBobjects_Intefraces.IMainDataBase.CabinetList
        {
            get { throw new NotImplementedException(); }
        }

        Scheduler_Common_Interfaces.IFactory Scheduler_DBobjects_Intefraces.IMainDataBase.EntityFactory
        {
            get { throw new NotImplementedException(); }
        }
    }
}
