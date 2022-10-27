using CRM.Models;

namespace CRM.Repository;

public class InscricaoRepository
{
  private MyContext _context;
  public InscricaoRepository(MyContext context)
  {
    _context = context;
  }

  public Inscricao Create(Inscricao inscricao)
  {
    Console.WriteLine(inscricao.CandidatoId);
    _context.Inscricoes.Add(inscricao);
    _context.SaveChanges();
    return inscricao;
  }

}