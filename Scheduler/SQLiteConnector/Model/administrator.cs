namespace EF6Connector.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Scheduler.administrators")]
    public partial class administrator
    {
        [Key]
        public int idadministrators { get; set; }

        //[Column(TypeName = "text")]
        //[StringLength(65535)]
        public string name { get; set; }

        public bool notworking { get; set; }

        public virtual ICollection<administratorDuty> administratorDuty { get; set; }
    }
}
