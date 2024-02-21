using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalColaboradores.Business.Entities;

namespace PortalColaboradores.Infra.Map;

public class PessoaFisicaMap : IEntityTypeConfiguration<PessoaFisica>
{
    public void Configure(EntityTypeBuilder<PessoaFisica> builder)
    {
        builder.ToTable("PessoasFisicas");

        builder.HasKey(p => p.Id);
            
        builder.Property(p => p.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .UseSequence()
            .IsRequired();
            
        builder.Property(p => p.Cpf)
            .HasColumnName("cpf")
            .HasColumnType("varchar(11)")
            .IsRequired();
        
        builder.Property(p => p.Rg)
            .HasColumnName("rg")
            .HasColumnType("varchar(10)")
            .IsRequired();
        
        builder.Property(p => p.Nome)
            .HasColumnName("nome")
            .HasColumnType("varchar(50)")
            .IsRequired();
        
        builder.Property(p => p.DataNascimento)
            .HasColumnName("data_nascimento")
            .HasColumnType("datetime")
            .IsRequired();
        
        builder.HasMany(x => x.Telefones)
            .WithOne(x => x.PessoaFisica)
            .HasForeignKey(x => x.PessoaFisicaId);
        
        builder.HasMany(x => x.Enderecos)
            .WithOne(x => x.PessoaFisica)
            .HasForeignKey(x => x.PessoaFisicaId);
    }
}