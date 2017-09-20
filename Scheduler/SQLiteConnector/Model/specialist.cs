namespace EF6Connector.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Scheduler.specialists")]
    public partial class specialist
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public specialist()
        {
            specializations2specialist = new HashSet<specializations2specialist>();
        }

        [Key]
        public int idspecialists { get; set; }

        //[Column(TypeName = "text")]
        //[StringLength(65535)]
        public string name { get; set; }

        public byte notworking { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<specializations2specialist> specializations2specialist { get; set; }
        public virtual ICollection<specialistDuty> specialistDuty { get; set; }
    }
}
