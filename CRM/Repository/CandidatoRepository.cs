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

  public List<Candidato> GetAll()
  {
    var candidatos = _context.Candidatos.ToList();
    return candidatos;
  }

  public Candidato Delete(int id)
  {
    var candidato = _context.Candidatos.Where(x => x.CandidatoId == id).First();
    _context.Remove(candidato);
    _context.SaveChanges();
    return candidato;
  }

  public Candidato? GetById(int id)
  {
    var candidato = _context.Candidatos.Where(x => x.CandidatoId == id).FirstOrDefault();
    return candidato;
  }
}