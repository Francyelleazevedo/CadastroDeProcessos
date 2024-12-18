using Microsoft.EntityFrameworkCore;
using CadastroDeProcessos.Domain.Entities;

namespace CadastroDeProcessos.Infra.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Processo> Processos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
