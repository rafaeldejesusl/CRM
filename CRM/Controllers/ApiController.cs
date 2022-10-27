using Microsoft.AspNetCore.Mvc;
using CRM.Models;
using CRM.Repository;

namespace CRM.Controllers;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase
{
    private readonly CandidatoRepository _candidatos;
    private readonly CursoRepository _cursos;

    public ApiController(CandidatoRepository candidatoRepository, CursoRepository cursoRepository, InscricaoRepository inscricaoRepository)
    {
        _candidatos = candidatoRepository;
        _cursos = cursoRepository;
    }

    [HttpPost("candidatos")]
    public Candidato CreateCandidatos([FromBody] Candidato candidato)
    {
        var created = _candidatos.Create(candidato);
        return created;
    }
    
    [HttpGet("candidatos")]
    public List<Candidato> GetAllCandidatos()
    {
        var candidatos = _candidatos.GetAll();
        return candidatos;
    }

    [HttpDelete("candidatos/{id}")]
    public Candidato DeleteCandidatos([FromRoute] string id)
    {
        var candidatoId = Convert.ToInt32(id);
        var deleted = _candidatos.Delete(candidatoId);
        return deleted;
    }

    [HttpGet("candidatos/{id}")]
    public Candidato? GetCandidatoById([FromRoute] string id)
    {
        var candidatoId = Convert.ToInt32(id);
        var candidato = _candidatos.GetById(candidatoId);
        return candidato;
    }

    [HttpPost("cursos")]
    public Curso CreateCursos([FromBody] Curso curso)
    {
        var created = _cursos.Create(curso);
        return created;
    }
    
    [HttpGet("cursos")]
    public List<Curso> GetAllCursos()
    {
        var cursos = _cursos.GetAll();
        return cursos;
    }
}
