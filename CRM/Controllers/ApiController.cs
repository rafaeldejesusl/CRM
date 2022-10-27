using Microsoft.AspNetCore.Mvc;
using CRM.Models;
using CRM.Repository;

namespace CRM.Controllers;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase
{
    private readonly CandidatoRepository _candidatos;

    public ApiController(CandidatoRepository candidatoRepository, CursoRepository cursoRepository, InscricaoRepository inscricaoRepository)
    {
        _candidatos = candidatoRepository;
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
}
