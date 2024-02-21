using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PortalColaboradores.Business.Entities;
using PortalColaboradores.Business.Models.Colaborador;
using PortalColaboradores.Core;

namespace PortalColaboradores.Infra.Data;

public class ApplicationDbContext : IdentityDbContext<Usuario>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt)
    {
        
    }
    
    public DbSet<Usuario> Usuarios { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
    }
}

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var conectString =
            "Server=localhost;Database=master;User Id=sa;Password=Sqlserver123@;Encrypt=false;TrustServerCertificate=True;";

        optionsBuilder.UseSqlServer(conectString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}