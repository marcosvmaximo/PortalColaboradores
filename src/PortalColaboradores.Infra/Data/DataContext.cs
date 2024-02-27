using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PortalColaboradores.Business.Entities;
using PortalColaboradores.Core;

namespace PortalColaboradores.Infra.Data;

public class DataContext : DbContext, IUnityOfWork
{
    public DataContext(DbContextOptions<DataContext> opt) : base(opt)
    {
        
    }
    
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Colaborador> Colaboradores { get; set; }
    public DbSet<PessoaFisica> PessoasFisicas { get; set; }
    public DbSet<Telefone> Telefones { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PessoaFisica>().ToTable("PessoasFisicas");
        modelBuilder.Entity<Colaborador>().ToTable("Colaboradores");
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
    }

    public async Task<bool> Commit()
    {
        return await SaveChangesAsync() > 0;
    }
}
public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        var conectString =
            "Server=localhost;Database=master;User Id=sa;Password=Sqlserver123@;Encrypt=false;TrustServerCertificate=True;";

        optionsBuilder.UseSqlServer(conectString);

        return new DataContext(optionsBuilder.Options);
    }
}