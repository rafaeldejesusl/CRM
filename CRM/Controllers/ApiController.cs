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
}
