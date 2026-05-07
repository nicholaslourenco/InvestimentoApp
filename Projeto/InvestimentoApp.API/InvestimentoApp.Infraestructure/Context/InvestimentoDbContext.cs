using InvestimentoApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace InvestimentoApp.Infrastructure.Context;

public class InvestimentoDbContext : DbContext
{
    public InvestimentoDbContext(DbContextOptions<InvestimentoDbContext> options) : base(options)
    {
    }

    public DbSet<Cotacao> Cotacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cotacao>().ToTable("Cotacao");

        modelBuilder.Entity<Cotacao>()
            .Property(c => c.Valor)
            .HasPrecision(10, 2);

        base.OnModelCreating(modelBuilder);
    }
}