using BlazorApp.Application.Interfaces;
using BlazorApp.Bll.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobSeekerController : ControllerBase
{
    private readonly IRepository<JobSeeker> _repository;

    public JobSeekerController(IRepository<JobSeeker> repository)
    {
        _repository = repository;
    }

    [HttpGet("GetJobSeekers")]
    public async Task<IReadOnlyList<JobSeeker>> GetJobSeekers() => await _repository.GetList();

    [HttpGet("GetJobSeeker{id}")]
    public async Task<JobSeeker> GetJobSeeker(int id) => await _repository.GetOne(id);

    [HttpPost("AddJobSeeker")]
    public async Task AddJobSeeker(JobSeeker jobSeeker) => await _repository.Add(jobSeeker);

    [HttpPut("UpdateJobSeeker")]
    public async Task UpdateJobSeeker(JobSeeker jobSeeker) => await _repository.Update(jobSeeker);

    [HttpDelete("DeleteJobSeeker{id}")]
    public async Task DeleteJobSeeker(int id) => await _repository.Delete(id);
}