namespace EF6Connector.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using SQLite.CodeFirst;

    public enum EFTYPES
    {
        SQLITE,
        MYSQL,
        SQLCOMPACT,
        NONE
    }

    public partial class EF6Model : DbContext
    {
        public static EFTYPES UsedEFdb = EFTYPES.NONE;
        public EF6Model()
#if EF_SQLITE
            : base("name=EF6ModelSQLite")
#elif EF_MYSQL
            : base("name=EF6ModelMySql")
#elif EF_SQLCOMPACT
            : base("name=EF6ModelSQLCompact")
#endif
        {
            //Action<Type> noop = _ => { };
            //var typesToCopy = new Type[]
            //    {typeof(MySql.Data.Entity.MySqlLogger)};
            //foreach (var t in typesToCopy)
            //    noop(t);
            UsedEFdb = EFTYPES.
#if EF_SQLITE
                    SQLITE
#elif EF_MYSQL
                    MYSQL
#elif EF_SQLCOMPACT
                    SQLCOMPACT
#endif
                ;
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

#if EF_SQLITE
            var sqliteConnectionInitializer = new DbInitializer(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
#endif
        }

#if EF_SQLITE
        public class DbInitializer : SQLite.CodeFirst.SqliteCreateDatabaseIfNotExists<EF6Model>
        {
            public DbInitializer(DbModelBuilder builder) : base(builder)
            {

            }
            protected override void Seed(EF6Model context)
            {

            }
        }
#endif
    }

}
