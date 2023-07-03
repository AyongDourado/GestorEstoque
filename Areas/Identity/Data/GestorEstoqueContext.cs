using GestorEstoque.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GestorEstoque.Models;

namespace GestorEstoque.Data;

public class GestorEstoqueContext : IdentityDbContext<GestorEstoqueUsuario>
{
    public GestorEstoqueContext(DbContextOptions<GestorEstoqueContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.Entity<ProdutoEstoque>().ToTable(nameof(ProdutoEstoque)).HasOne(pe => pe.Produto).WithMany();
    }

    public DbSet<Produto>? Produto { get; set; }

    public DbSet<ProdutoEstoque>? ProdutoEstoque { get; set; }
}
