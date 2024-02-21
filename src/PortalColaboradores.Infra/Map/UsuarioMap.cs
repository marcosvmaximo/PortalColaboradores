using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalColaboradores.Business.Entities;

namespace PortalColaboradores.Infra.Map;

public class UsuarioMap : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");

        builder.HasKey(p => p.UserName);
            
        builder.Property(p => p.UserName)
            .HasColumnName("login")
            .HasColumnType("varchar(20)")
            .IsRequired();
        
        builder.Property(p => p.Nome)
            .HasColumnName("numero")
            .HasColumnType("varchar(20)")
            .IsRequired();
        
        builder.Property(p => p.PasswordHash)
            .HasColumnName("senha")
            .HasColumnType("varchar(max)")
            .IsRequired();

        builder.Property(p => p.Administrador)
            .HasColumnName("administrador")
            .HasColumnType("bit")
            .IsRequired();
    }
}