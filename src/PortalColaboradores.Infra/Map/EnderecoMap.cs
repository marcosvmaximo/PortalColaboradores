using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalColaboradores.Business.Entities;

namespace PortalColaboradores.Infra.Map;

public class EnderecoMap : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.ToTable("Enderecos");

        builder.HasKey(p => p.Id);
            
        builder.Property(p => p.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .UseSequence()
            .IsRequired();
        
        builder.Property(p => p.Tipo)
            .HasColumnName("tipo")
            .HasColumnType("smallint")
            .IsRequired();
        
        builder.Property(p => p.Cep)
            .HasColumnName("tipo")
            .HasColumnType("varchar(8)")
            .IsRequired();
        
        builder.Property(p => p.Logradouro)
            .HasColumnName("logradouro")
            .HasColumnType("varchar(100)")
            .IsRequired();
        
        builder.Property(p => p.NumeroComplemento)
            .HasColumnName("numero_complemento")
            .HasColumnType("varchar(10)")
            .IsRequired();
        
        builder.Property(p => p.Bairro)
            .HasColumnName("bairro")
            .HasColumnType("varchar(50)")
            .IsRequired();
        
        builder.Property(p => p.Cidade)
            .HasColumnName("cidade")
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.HasOne(p => p.PessoaFisica)
            .WithMany(x => x.Enderecos)
            .HasForeignKey(x => x.PessoaFisicaId);
    }
}