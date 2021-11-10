using Microsoft.EntityFrameworkCore;
using RegistrationFormSyncer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationFormSyncer.CheckDbContext
{
    public class CheckupsDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // PostgreSQL uses the public schema by default - not dbo.
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);

            //Rename Identity tables to lowercase
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var currentTableName = modelBuilder.Entity(entity.Name).Metadata.GetDefaultTableName();
                modelBuilder.Entity(entity.Name).ToTable(currentTableName.ToLower());
            }
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<PatientInfoVM> PatientInfoVMs { get; set; }
        public DbSet<PatientInfo> PatientInfos { get; set; }
        public DbSet<TestType> TestTypes { get; set; }
        public void SetCommandTimeOut(int timeOut)
        {
            Database.SetCommandTimeout(timeOut);
        }
        //
        public CheckupsDbContext(DbContextOptions<CheckupsDbContext> options)
           : base(options)
        {
            SetCommandTimeOut(1000);
        }
    }
}
