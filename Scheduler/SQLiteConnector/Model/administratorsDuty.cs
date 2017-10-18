namespace EF6Connector.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Scheduler.administratorDuty")]
    public class administratorDuty
    {
        [Key]
        public int idadministratorDuty { get; set; }
        public long dutytimestart { get; set; }
        public long dutytimeend { get; set; }
        public int administratorid { get; set; }
        public bool supplimentary { get; set; }

        public virtual administrator administrator { get; }
    }
}