using BlazorApp.Application.Interfaces;
using BlazorApp.Bll.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Server.Controllers;

public class InterviewController : ControllerBase
{
    private readonly IRepository<Interview> _repository;

    public InterviewController(IRepository<Interview> repository)
    {
        _repository = repository;
    }

    [HttpGet("GetInterviews")]
    public async Task<IReadOnlyList<Interview>> GetInterviews() => await _repository.GetList();

    [HttpGet("GetInterview{id}")]
    public async Task<Interview> GetInterview(int id) => await _repository.GetOne(id);

    [HttpPost("AddInterview")]
    public async Task AddInterview(Interview interview) => await _repository.Add(interview);

    [HttpPut("UpdateInterview")]
    public async Task UpdateInterview(Interview interview) => await _repository.Update(interview);

    [HttpDelete("DeleteInterview{id}")]
    public async Task DeleteInterview(int id) => await _repository.Delete(id);
}