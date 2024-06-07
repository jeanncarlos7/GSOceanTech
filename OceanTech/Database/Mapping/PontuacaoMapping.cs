using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OceanTech.Domain.Entities;


namespace OceanTech.Database.Mapping
{
    public class PontuacaoMapping : IEntityTypeConfiguration<Pontuacao>
    {
        public void Configure(EntityTypeBuilder<Pontuacao> builder)
        {
            builder.ToTable("TB_GS_Pontuacao");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("pc_int_id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.PontuacaoMensal)
                .HasColumnName("pc_int_pontuacaoMensal")
                .IsRequired()
                .HasColumnType("NUMBER(10)");

            builder.Property(x => x.UsuarioId)
                .HasColumnName("us_int_id")
                .IsRequired()
                .HasColumnType("NUMBER(25)");


            builder.HasOne(p => p.Usuario)
                .WithMany(u => u.Pontuacoes)
                .HasForeignKey(p => p.UsuarioId)
                .HasConstraintName("FK_GS_PontuacaoUsuario");
        }
    }
}