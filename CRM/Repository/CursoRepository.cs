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

  public Curso Delete(int id)
  {
    var curso = _context.Cursos.Where(x => x.CursoId == id).First();
    _context.Remove(curso);
    _context.SaveChanges();
    return curso;
  }

  public Curso? GetById(int id)
  {
    var curso = _context.Cursos.Where(x => x.CursoId == id).FirstOrDefault();
    return curso;
  }
}