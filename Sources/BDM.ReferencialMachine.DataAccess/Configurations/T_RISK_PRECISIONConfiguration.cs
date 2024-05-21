using BDM.ReferencialMachine.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDM.ReferencialMachine.DataAccess.Configurations
{
    public class T_RISK_PRECISIONConfiguration : IEntityTypeConfiguration<T_RISK_PRECISION>
    {
        public void Configure(EntityTypeBuilder<T_RISK_PRECISION> builder)
        {
            builder.HasKey(e => e.RISK_PRECISION_ID)
                .HasName("PK_RISK_PRECISION_ID");

            builder.ToTable("T_RISK_PRECISION");

            builder.Property(e => e.RISK_PRECISION_ID).HasDefaultValueSql("(newid())");

            builder.HasIndex(e => new { e.MACHINE_CODE }, "IX_T_RISK_PRECISION_MACHINE_CODE");

            builder.Property(e => e.CODE)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.LABEL)
                .HasMaxLength(150);

            builder.Property(e => e.MACHINE_CODE)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(e => e.DETAIL)
                .HasMaxLength(500);
        }
    }
}
