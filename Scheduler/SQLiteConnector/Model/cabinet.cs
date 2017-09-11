namespace EF6Connector.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Scheduler.cabinet")]
    public partial class cabinet
    {
        [Key]
        public int idcabinet { get; set; }

        [Required]
        //[StringLength(45)]
        public string name { get; set; }

        //[Column(TypeName = "uint")]
        public long availability { get; set; }

        public bool commentOnly { get; set; }
    }
}
