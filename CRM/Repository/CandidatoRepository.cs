using CRM.Models;

namespace CRM.Repository;

public class CandidatoRepository
{
  private MyContext _context;
  public CandidatoRepository(MyContext context)
  {
    _context = context;
  }

  public Candidato Create(Candidato candidato)
  {
    _context.Candidatos.Add(candidato);
    _context.SaveChanges();
    return candidato;
  }

}