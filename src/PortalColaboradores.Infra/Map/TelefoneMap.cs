using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalColaboradores.Business.Entities;

namespace PortalColaboradores.Infra.Map;

public class TelefoneMap : IEntityTypeConfiguration<Telefone>
{
    public void Configure(EntityTypeBuilder<Telefone> builder)
    {
        builder.ToTable("Telefones");

        builder.HasKey(p => p.Id);
            
        builder.Property(p => p.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .UseSequence()
            .IsRequired();
        
        builder.Property(p => p.Numero)
            .HasColumnName("numero")
            .HasColumnType("varchar(20)")
            .IsRequired();
        
        builder.Property(p => p.Tipo)
            .HasColumnName("tipo")
            .HasColumnType("smallint")
            .IsRequired();

        builder.HasOne(x => x.PessoaFisica)
            .WithMany(x => x.Telefones)
            .HasForeignKey(x => x.PessoaFisicaId);
    }
}