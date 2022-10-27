using Microsoft.EntityFrameworkCore;
using CRM.Models;

namespace CRM.Repository;

public class MyContext : DbContext
{
  public virtual DbSet<Candidato> Candidatos { get; set; } = null!;
  public virtual DbSet<Curso> Cursos { get; set; } = null!;
  public virtual DbSet<Inscricao> Inscricoes { get; set; } = null!;

  public MyContext(DbContextOptions<MyContext> options) : base(options) {}
  public MyContext() {}

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    if (!optionsBuilder.IsConfigured)
    {
      optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=CRM;User=SA;Password=Password12!");
    }
  }
}