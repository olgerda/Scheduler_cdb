namespace EF6Connector.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Scheduler.telephones")]
    public partial class telephone
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public telephone()
        {
            telephones2clients = new HashSet<telephones2clients>();
        }

        [Key]
        public int idtelephones { get; set; }

        [Required]
        //[StringLength(20)]
        public string telephonescol { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<telephones2clients> telephones2clients { get; set; }
    }
}
