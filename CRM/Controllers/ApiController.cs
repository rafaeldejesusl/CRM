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
    private readonly InscricaoRepository _inscricoes;

    public ApiController(CandidatoRepository candidatoRepository, CursoRepository cursoRepository, InscricaoRepository inscricaoRepository)
    {
        _candidatos = candidatoRepository;
        _cursos = cursoRepository;
        _inscricoes = inscricaoRepository;
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

    [HttpDelete("cursos/{id}")]
    public Curso DeleteCursos([FromRoute] string id)
    {
        var cursoId = Convert.ToInt32(id);
        var deleted = _cursos.Delete(cursoId);
        return deleted;
    }

    [HttpGet("cursos/{id}")]
    public Curso? GetCursoById([FromRoute] string id)
    {
        var cursoId = Convert.ToInt32(id);
        var curso = _cursos.GetById(cursoId);
        return curso;
    }

    [HttpPost("inscricoes")]
    public Inscricao CreateInscricoes([FromBody] Inscricao inscricao)
    {
        Console.WriteLine(inscricao.CandidatoId);
        var created = _inscricoes.Create(inscricao);
        return created;
    }
    
}
