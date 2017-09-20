namespace EF6Connector.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Scheduler.specialistDuty")]
    public class specialistDuty
    {
        [Key]
        public int idspecialistDuty { get; set; }
        public long dutytimestart { get; set; }
        public long dutytimeend { get; set; }
        public int specialistid { get; set; }
        public bool supplimentary { get; set; }

        public virtual specialist specialist { get; }
    }
}