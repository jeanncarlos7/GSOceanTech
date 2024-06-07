using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OceanTech.Domain.Entities;

namespace OceanTech.Database.Mapping
{
    public class PontuacaoDiariaMapping : IEntityTypeConfiguration<PontuacaoDiaria>
    {
        public void Configure(EntityTypeBuilder<PontuacaoDiaria> builder)
        {
            builder.ToTable("TB_GS_PontuacaoDiarias");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("pd_int_id");

            builder.Property(x => x.Valor)
                .HasColumnName("pd_int_valor")
                .IsRequired()
                .HasColumnType("NUMBER(10)");

            builder.Property(x => x.GameDiarioId)
                .HasColumnName("gd_int_id")
                .HasColumnType("NUMBER(10)");

            builder.HasOne(u => u.GameDiario)
                .WithMany(p => p.PontuacaoDiaria)
                .HasForeignKey(u => u.GameDiarioId)
                .HasConstraintName("FK_GS_PontuacaoDiariaGameDiario");
        }
    }
}