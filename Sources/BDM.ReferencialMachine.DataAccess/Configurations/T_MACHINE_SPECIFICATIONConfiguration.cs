using BDM.ReferencialMachine.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDM.ReferencialMachine.DataAccess.Configurations
{
    public class T_MACHINE_SPECIFICATIONConfiguration : IEntityTypeConfiguration<T_MACHINE_SPECIFICATION>
    {
        public void Configure(EntityTypeBuilder<T_MACHINE_SPECIFICATION> entity)
        {
            entity.HasKey(e => e.MACHINE_ID)
                .HasName("PK_MACHINE_ID");

            entity.ToTable("T_MACHINE_SPECIFICATION");

            entity.HasIndex(e => new { e.CODE, e.END_DATETIME_SUBSCRIPTION_PERIOD }, "IX_T_MACHINE_SPECIFICATION_CODE_END_DATETIME_SUBSCRIPTION_PERIOD");

            entity.Property(e => e.MACHINE_ID).HasDefaultValueSql("(newid())");


            entity.Property(e => e.ALL_PLACE_CORVERED).HasMaxLength(15);

            entity.Property(e => e.CODE)
                .IsRequired()
                .HasMaxLength(5);

            entity.Property(e => e.DESCRIPTION).HasMaxLength(2000);

            entity.Property(e => e.KEYWORDS).HasMaxLength(300);

            entity.Property(e => e.LABEL).HasMaxLength(100);

            entity.Property(e => e.MACHINE_RATE).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.NAME)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.PRODUCT).HasMaxLength(20);
            
            entity.Property(e => e.SCORE).HasMaxLength(10);
            
            entity.Property(e => e.EDITION_CLAUSE_CODES).HasMaxLength(100);
            
            entity.Property(e => e.FINANCIAL_VALUATION_MACHINE_CURRENCY).HasMaxLength(10);

        }

    }
}
