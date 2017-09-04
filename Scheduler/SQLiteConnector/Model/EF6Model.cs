namespace EF6Connector.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EF6Model : DbContext
    {
        public EF6Model()
            : base("name=EF6ModelSQLite")
        {
            //Action<Type> noop = _ => { };
            //var typesToCopy = new Type[]
            //    {typeof(MySql.Data.Entity.MySqlLogger)};
            //foreach (var t in typesToCopy)
            //    noop(t);
            
        }

        public virtual DbSet<cabinet> cabinets { get; set; }
        public virtual DbSet<clientgenerallyparam> clientgenerallyparams { get; set; }
        public virtual DbSet<client> clients { get; set; }
        public virtual DbSet<reception> receptions { get; set; }
        public virtual DbSet<specialist2clientprice> specialist2clientprice { get; set; }
        public virtual DbSet<specialist> specialists { get; set; }
        public virtual DbSet<specialization> specializations { get; set; }
        public virtual DbSet<specializations2specialist> specializations2specialist { get; set; }
        public virtual DbSet<telephone> telephones { get; set; }
        public virtual DbSet<telephones2clients> telephones2clients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cabinet>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .HasMany(e => e.telephones2clients)
                .WithOptional(e => e.client)
                .HasForeignKey(e => e.clid)
                .WillCascadeOnDelete();

            modelBuilder.Entity<specialist>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<specialist>()
                .HasMany(e => e.specializations2specialist)
                .WithRequired(e => e.specialist1)
                .HasForeignKey(e => e.specialist)
                .WillCascadeOnDelete();

            modelBuilder.Entity<specialization>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<specialization>()
                .HasMany(e => e.specializations2specialist)
                .WithRequired(e => e.specialization1)
                .HasForeignKey(e => e.specialization)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<telephone>()
                .Property(e => e.telephonescol)
                .IsUnicode(false);

            modelBuilder.Entity<telephone>()
                .HasMany(e => e.telephones2clients)
                .WithOptional(e => e.telephone)
                .HasForeignKey(e => e.telid)
                .WillCascadeOnDelete();
        }
    }
    
}
