namespace EF6Connector.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Scheduler.specializations2specialist")]
    public partial class specializations2specialist
    {
        [Key]
        public int idspecializations2specialist { get; set; }

        public int specialization { get; set; }

        public int specialist { get; set; }

        public virtual specialist specialist1 { get; set; }

        public virtual specialization specialization1 { get; set; }
    }
}
