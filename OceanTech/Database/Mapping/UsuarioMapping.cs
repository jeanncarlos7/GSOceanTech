using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OceanTech.Domain.Entities;


namespace OceanTech.Database.Mapping
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("TB_GS_Usuario");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("us_int_id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Nome)
                .HasColumnName("us_string_nome")
                .HasMaxLength(225);

            builder.Property(x => x.Email)
                .HasColumnName("us_string_email")
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Senha)
                .HasColumnName("us_string_senha")
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Inscricao)
                .HasColumnName("us_dat_inscricao")
                .HasColumnType("TIMESTAMP");

            builder.Property(x => x.Ativo)
                .HasColumnName("ativo")
                .IsRequired()
                .HasColumnType("NUMBER(1)")
                .HasConversion(
                    z => z ? 1 : 0,
                    z => z == 1
                );


            builder.HasMany(u => u.Pontuacoes)
                .WithOne(p => p.Usuario)
                .HasForeignKey(p => p.UsuarioId)
                .HasConstraintName("FK_GS_PontuacaoUsuario");

            builder.HasMany(u => u.GameDiarios)
                .WithOne(g => g.Usuario)
                .HasForeignKey(g => g.UsuarioId)
                .HasConstraintName("FK_GS_GameDiarioUsuario");
        }
    }
}