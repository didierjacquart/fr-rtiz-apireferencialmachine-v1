using BDM.ReferencialMachine.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDM.ReferencialMachine.DataAccess.Configurations
{
    public class T_EDITION_CLAUSEConfiguration : IEntityTypeConfiguration<T_EDITION_CLAUSE>
    {
        public void Configure(EntityTypeBuilder<T_EDITION_CLAUSE> entity)
        {
            entity.HasKey(e => e.CLAUSE_ID)
                .HasName("PK_CLAUSE_ID");

            entity.ToTable("T_EDITION_CLAUSE");

            entity.Property(e => e.CLAUSE_ID).HasDefaultValueSql("(newid())");

            entity.Property(e => e.CODE)
                .IsRequired()
                .HasMaxLength(15);

            entity.Property(e => e.LABEL)
                .HasMaxLength(150);

            entity.Property(e => e.TYPE)
                .HasMaxLength(35);

            entity.Property(e => e.DESCRIPTION)
                .HasMaxLength(4000);

            entity.Property(e => e.CLAUSE_TYPE)
                .HasMaxLength(15);
        }
    }
}
