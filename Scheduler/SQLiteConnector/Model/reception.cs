namespace EF6Connector.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Scheduler.receptions")]
    public partial class reception
    {
        [Key]
        public int idreceptions { get; set; }

        [Column(TypeName = "uint")]
        public long clientid { get; set; }

        [Column(TypeName = "uint")]
        public long specialistid { get; set; }

        [Column(TypeName = "uint")]
        public long cabinetid { get; set; }

        [Column(TypeName = "uint")]
        public long specializationid { get; set; }

        public byte isrented { get; set; }

        public TimeSpan timestart { get; set; }

        public TimeSpan timeend { get; set; }

        [Column(TypeName = "date")]
        public DateTime timedate { get; set; }

        [Column(TypeName = "tinytext")]
        [StringLength(255)]
        public string administrator { get; set; }
    }
}
