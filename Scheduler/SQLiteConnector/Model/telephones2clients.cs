namespace EF6Connector.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Scheduler.telephones2clients")]
    public partial class telephones2clients
    {
        [Key]
        public int idtelephones2clients { get; set; }

        public int? telid { get; set; }

        public int? clid { get; set; }

        public virtual client client { get; set; }

        public virtual telephone telephone { get; set; }
    }
}
