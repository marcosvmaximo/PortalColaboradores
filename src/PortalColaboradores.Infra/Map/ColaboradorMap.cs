using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalColaboradores.Business.Entities;

namespace PortalColaboradores.Infra.Map;

public class ColaboradorMap : IEntityTypeConfiguration<Colaborador>
{
    public void Configure(EntityTypeBuilder<Colaborador> builder)
    {
        builder.ToTable("Colaboradores");
        
        builder.HasKey(p => p.Id);
            
        builder.Property(p => p.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .UseSequence()
            .IsRequired();
        
        builder.Property(p => p.Matricula)
            .HasColumnName("matricula")
            .HasColumnType("int")
            .IsRequired();
        
        builder.Property(p => p.Tipo)
            .HasColumnName("tipo")
            .HasColumnType("smallint")
            .IsRequired();

        builder.Property(p => p.DataAdmissao)
            .HasColumnName("data_admissao")
            .HasColumnType("datetime");
        
        builder.Property(p => p.ValorContribuicao)
            .HasColumnName("valor_contribuicao")
            .HasColumnType("numeric(10,2)");
    }
}