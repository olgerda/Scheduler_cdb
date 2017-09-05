namespace EF6Connector.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Scheduler.clientgenerallyparams")]
    public partial class clientgenerallyparam
    {
        [Key]
        public int idclientGenerallyParams { get; set; }

        //[Column(TypeName = "uint")]
        public long clientId { get; set; }

        public long generallyTime { get; set; }
    

        //[Column(TypeName = "uint")]
        public long generallyPrice { get; set; }
    }
}
