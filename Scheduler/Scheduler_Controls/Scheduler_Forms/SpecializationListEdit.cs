using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Scheduler_Controls_Interfaces;


namespace Scheduler_Forms
{
    public partial class SpecializationListEdit : Form
    {
        ISpecializationList specList;

        public SpecializationListEdit()
        {
            InitializeComponent();
            Init();
        }

        public SpecializationListEdit(ISpecializationList specList)
        {
            InitializeComponent();

            this.specList = specList;
            Init();
        }

        public ISpecializationList SpecializationList
        {
            get { return specList; }
            set 
            { 
                specList = value;
                specializationsInfo.SpecializationList = specList;
            }
        }

        void Init()
        {
            specializationsInfo.SpecializationList = specList;
            
            specializationsInfo.OnSaveChanges += new SaveChangesHandler<ISpecializationList>(specializationsInfo_OnSaveChanges);
        }

        void specializationsInfo_OnSaveChanges(object source, SaveChangesEventArgs<ISpecializationList> e)
        {
            specList = e.Entity;
        }


    }
}
