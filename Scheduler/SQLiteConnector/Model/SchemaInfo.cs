namespace EF6Connector.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Scheduler.SchemaInfo")]
    public class SchemaInfo
    {
        [Key]
        public int Id { get; set; }

        public int Version { get; set; }
    }
}
