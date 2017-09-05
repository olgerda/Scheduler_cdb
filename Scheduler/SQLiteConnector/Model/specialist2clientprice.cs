namespace EF6Connector.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Scheduler.specialist2clientprice")]
    public partial class specialist2clientprice
    {
        [Key]
        public int idspecialist2clientcost { get; set; }

        public int specid { get; set; }

        public int clid { get; set; }

        //[Column(TypeName = "uint")]
        public long price { get; set; }
    }
}
