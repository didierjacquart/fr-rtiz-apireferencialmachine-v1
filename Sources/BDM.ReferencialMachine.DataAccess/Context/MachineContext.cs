using System.Diagnostics.CodeAnalysis;
using BDM.ReferencialMachine.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;
using BDM.ReferencialMachine.DataAccess.Models;


namespace BDM.ReferencialMachine.DataAccess.Context
{
    [ExcludeFromCodeCoverage]
    public class MachineContext : DbContext
    {
        public MachineContext(DbContextOptions<MachineContext> options) : base(options)  { }

        public virtual DbSet<T_FAMILY> T_FAMILY { get; set; }
        public virtual DbSet<T_EDITION_CLAUSE> T_EDITION_CLAUSE { get; set; }
        public virtual DbSet<T_MACHINE_SPECIFICATION> T_MACHINE_SPECIFICATION { get; set; }
        public virtual DbSet<T_PRICING_RATE> T_PRICING_RATE { get; set; }
        public virtual DbSet<T_RISK_PRECISION> T_RISK_PRECISION { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "French_CI_AS");
            modelBuilder.HasDefaultSchema("sch_mchn");

            modelBuilder.ApplyConfiguration(new T_FAMILYConfiguration());
            modelBuilder.ApplyConfiguration(new T_EDITION_CLAUSEConfiguration());
            modelBuilder.ApplyConfiguration(new T_MACHINE_SPECIFICATIONConfiguration());
            modelBuilder.ApplyConfiguration(new T_PRICING_RATEConfiguration());
            modelBuilder.ApplyConfiguration(new T_RISK_PRECISIONConfiguration());
        }  
    }
}

