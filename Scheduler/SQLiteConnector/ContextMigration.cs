using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using EF6Connector.Model;

namespace EF6Connector
{
    class ContextMigration : DbContext
    {
        public static int RequiredDatabaseVersion = 2;

        public DbSet<SchemaInfo> SchemaInfoes { get; set; }

        public ContextMigration() : base("name=EF6ModelSQLite")
        {
            
        }

        public void UpdateIfNeeded()
        {
            using (ContextMigration ctx = new ContextMigration())
            {

                ctx.Database.ExecuteSqlCommand(@"CREATE TABLE IF NOT EXISTS ""SchemaInfo"" (Id INTEGER PRIMARY KEY NOT NULL, Version INTEGER)");

                int currentVersion = 0;
                if (ctx.SchemaInfoes.Count() > 0)
                    currentVersion = ctx.SchemaInfoes.Max(x => x.Version);
                ContextHelper mmSqliteHelper = new ContextHelper();
                while (currentVersion < RequiredDatabaseVersion)
                {
                    currentVersion++;
                    foreach (string migration in mmSqliteHelper.Migrations[currentVersion])
                    {
                        ctx.Database.ExecuteSqlCommand(migration);
                    }
                    ctx.SchemaInfoes.Add(new SchemaInfo() { Version = currentVersion });
                    ctx.SaveChanges();
                }
            }
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SQLite.CodeFirst.SqliteCreateDatabaseIfNotExists<ContextMigration>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }
    }
}
