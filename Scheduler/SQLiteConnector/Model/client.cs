namespace EF6Connector.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Scheduler.clients")]
    public partial class client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public client()
        {
            telephones2clients = new HashSet<telephones2clients>();
        }

        [Key]
        public int idclients { get; set; }

        //[Column(TypeName = "tinytext")]
        //[StringLength(255)]
        public string name { get; set; }

        //[Column(TypeName = "text")]
        //[StringLength(65535)]
        public string comment { get; set; }

        public string email { get; set; }

        public bool blacklisted { get; set; }
        public bool needSms { get; set; }

        public int balance { get; set; }

        public int clientType { get; set; }
        //[Column(TypeName = "tinytext")]
        //[StringLength(255)]
        public string administrator { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<telephones2clients> telephones2clients { get; set; }
    }
}
