using BlazorApp.Application.Interfaces;
using BlazorApp.Bll.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VacancyController : ControllerBase
{
    private readonly IRepository<Vacancy> _repository;

    public VacancyController(IRepository<Vacancy> repository)
    {
        _repository = repository;
    }

    [HttpGet("GetVacancies")]
    public async Task<IReadOnlyList<Vacancy>> GetVacancies() => await _repository.GetList();

    [HttpGet("GetVacancy{id}")]
    public async Task<Vacancy> GetVacancy(int id) => await _repository.GetOne(id);

    [HttpPost("AddVacancy")]
    public async Task AddVacancy(Vacancy vacancy) => await _repository.Add(vacancy);

    [HttpPut("UpdateVacancy")]
    public async Task UpdateVacancy(Vacancy vacancy) => await _repository.Update(vacancy);

    [HttpDelete("DeleteVacancy{id}")]
    public async Task DeleteVacancy(int id) => await _repository.Delete(id);
}