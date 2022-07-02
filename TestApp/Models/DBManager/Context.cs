using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TestApp.Utils;

namespace TestApp.Models.DBManager
{
    public class Context : DbContext
    {
        static DbContextOptionsBuilder<Context> dbBuilder = new DbContextOptionsBuilder<Context>();
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public static Context GetNew()
        {
            dbBuilder = new DbContextOptionsBuilder<Context>();
            dbBuilder.UseSqlServer(JasonManger.GetConnectionString());
            return new Context(dbBuilder.Options);
        }

        
         
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys())
                             .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);
        }
    }

    //we need this class in design time for update database, but in run time we do not need it
    public class SiteContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer();
            return new Context(optionsBuilder.Options);
        }

    }
}
