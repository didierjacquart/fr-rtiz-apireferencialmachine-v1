using BDM.ReferencialMachine.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDM.ReferencialMachine.DataAccess.Configurations
{
    public class T_PRICING_RATEConfiguration : IEntityTypeConfiguration<T_PRICING_RATE>
    {
        public void Configure(EntityTypeBuilder<T_PRICING_RATE> entity)
        {
            entity.HasKey(e => e.PRICING_RATE_ID)
                .HasName("PK_PRICING_RATE_ID");

            entity.ToTable("T_PRICING_RATE");

            entity.Property(e => e.PRICING_RATE_ID).HasDefaultValueSql("(newid())");

            entity.Property(e => e.CODE).HasMaxLength(3);

            entity.Property(e => e.RATE).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.T_MACHINE_SPECIFICATION)
                .WithMany(p => p.T_PRICING_RATE)
                .HasForeignKey(d => d.FK_MACHINE_ID)
                .HasConstraintName("C_T_PRICING_RATE_MACHINE_SPECIFICATION_FK");
        }
    }
}
