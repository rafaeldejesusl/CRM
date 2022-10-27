using CRM.Models;

namespace CRM.Repository;

public class CursoRepository
{
  private MyContext _context;
  public CursoRepository(MyContext context)
  {
    _context = context;
  }

  public Curso Create(Curso curso)
  {
    _context.Cursos.Add(curso);
    _context.SaveChanges();
    return curso;
  }

  public List<Curso> GetAll()
  {
    var cursos = _context.Cursos.ToList();
    return cursos;
  }
}