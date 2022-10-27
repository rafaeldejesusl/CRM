using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Text;
using CRM.Repository;
using CRM.Models;
using Microsoft.Extensions.DependencyInjection;
namespace CRM.Test;

public class CandidatoTest : IClassFixture<WebApplicationFactory<program>>
{
    public HttpClient client;

    public CandidatoTest(WebApplicationFactory<program> factory)
    {
        client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<MyContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }
                services.AddDbContext<MyContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryTest");
                });
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                using (var appContext = scope.ServiceProvider.GetRequiredService<MyContext>())
                {
                    appContext.Database.EnsureCreated();
                    appContext.Database.EnsureDeleted();
                    appContext.Database.EnsureCreated();
                    appContext.Candidatos.Add(new Candidato { CandidatoId = 1, CandidatoName = "Test", CandidatoEmail="Test", CandidatoCpf="Test" });
                    appContext.SaveChanges();
                }
            });
        }).CreateClient();
    }

    [Theory(DisplayName = "POST para criar candidatos")]
    [MemberData(nameof(ShouldCreateCandidatoData))]
    public async Task ShouldCreateCandidato(Candidato candidatoExpected)
    {
        var json = JsonConvert.SerializeObject(candidatoExpected);
        var body = new StringContent(json, Encoding.UTF8, "application/json"); 
        var response =  await client.PostAsync("api/candidatos", body);
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Candidato>(content);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        result.CandidatoName.Should().Be(candidatoExpected.CandidatoName);
        result.CandidatoEmail.Should().Be(candidatoExpected.CandidatoEmail);
        result.CandidatoCpf.Should().Be(candidatoExpected.CandidatoCpf);
        result.CandidatoId.Should().Be(2);
    }

    public static readonly TheoryData<Candidato> ShouldCreateCandidatoData = new()
    {
        new Candidato()
        {
            CandidatoName = "name",
            CandidatoEmail = "email@email.com",
            CandidatoCpf = "00000000000"
        }
    };

    [Theory(DisplayName = "GET para listar candidatos")]
    [MemberData(nameof(ShouldGetAllCandidatosData))]
    public async Task ShouldGetAllCandidatos(List<Candidato> usersExpected)
    {
        var response =  await client.GetAsync("api/candidatos");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<List<Candidato>>(content);
        result.Should().BeEquivalentTo(usersExpected);
    }

    public static readonly TheoryData<List<Candidato>> ShouldGetAllCandidatosData = new()
    {
        new()
        {
            new Candidato 
            { 
              CandidatoId = 1,
              CandidatoName = "Test",
              CandidatoEmail="Test",
              CandidatoCpf="Test"
            }
        }
    };

    [Theory(DisplayName = "GET para listar candidato pelo id")]
    [MemberData(nameof(ShouldGetCandidatoData))]
    public async Task ShouldGetCandidato(Candidato userExpected)
    {
        var response =  await client.GetAsync("api/candidatos/1");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Candidato>(content);
        result.Should().BeEquivalentTo(userExpected);
    }

    public static readonly TheoryData<Candidato> ShouldGetCandidatoData = new()
    {
        new Candidato 
        { 
            CandidatoId = 1,
            CandidatoName = "Test",
            CandidatoEmail="Test",
            CandidatoCpf="Test"
        }
    };
}