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

  public List<Inscricao> GetAll()
  {
    if (_context.Inscricoes.ToList().Count == 0)
    {
      return new List<Inscricao>();
    }
    var inscricoes = 
      from inscricao in _context.Inscricoes
      join candidato in _context.Candidatos on inscricao.CandidatoId equals candidato.CandidatoId
      join curso in _context.Cursos on inscricao.CursoId equals curso.CursoId
      select new Inscricao { InscricaoId = inscricao.InscricaoId, CandidatoId = inscricao.CandidatoId, Candidato = candidato, CursoId = inscricao.CursoId, Curso = curso };
    return inscricoes.ToList();
  }
}