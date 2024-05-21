using BDM.ReferencialMachine.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDM.ReferencialMachine.DataAccess.Configurations
{
    public class T_FAMILYConfiguration : IEntityTypeConfiguration<T_FAMILY>
    {
        public void Configure(EntityTypeBuilder<T_FAMILY> entity)
        {
            entity.HasKey(e => e.FAMILY_ID)
                .HasName("PK_FAMILYID");

            entity.ToTable("T_FAMILY", "sch_mchn");

            entity.HasIndex(e => new { e.CODE, e.SUB_CODE }, "IX_FAMILY_CODE_SUBCODE")
                .IsUnique();

            entity.Property(e => e.FAMILY_ID).HasDefaultValueSql("(newid())");

            entity.Property(e => e.NAME).HasMaxLength(200);
            entity.Property(e => e.SUB_NAME).HasMaxLength(200);
            entity.Property(e => e.CODE).HasMaxLength(5);
            entity.Property(e => e.SUB_CODE).HasMaxLength(5);
        }
    }
}
