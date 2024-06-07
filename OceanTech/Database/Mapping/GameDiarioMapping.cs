using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OceanTech.Domain.Entities;


namespace OceanTech.Database.Mapping
{
    public class GameDiarioMapping : IEntityTypeConfiguration<GameDiario>
    {
        public void Configure(EntityTypeBuilder<GameDiario> builder)
        {
            builder.ToTable("TB_GS_GameDiario");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("gd_int_id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Jogou)
                .HasColumnName("gd_int_Jogou")
                .IsRequired()
                .HasColumnType("NUMBER(1)")
                .HasConversion(
                    z => z ? 1 : 0,
                    z => z == 1
                );

            builder.Property(x => x.dataJogo)
                .HasColumnName("gd_dat_DataJogou")
                .IsRequired()
                .HasColumnType("TIMESTAMP(6)");

            builder.Property(x => x.UsuarioId)
                .HasColumnName("us_int_id")
                .HasColumnType("NUMBER(10)");



            builder.HasOne(x => x.Usuario)
                .WithMany(u => u.GameDiarios)
                .HasForeignKey(x => x.UsuarioId)
                .HasConstraintName("FK_GS_GameDiarioUsuario");
        }
    }
}