using BlazorApp.Bll.Models;
using BlazorApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobSeekerController : ControllerBase
{
    private readonly IJobSeekerRepository _repository;

    public JobSeekerController(IJobSeekerRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("GetJobSeekers")]
    public async Task<IReadOnlyList<JobSeeker>> GetJobSeekers() => await _repository.GetList();

    [HttpGet("GetJobSeeker{id}")]
    public async Task<JobSeeker> GetJobSeeker(int id) => await _repository.GetOne(id);

    [HttpGet("GetPhoto{id}")]
    public async Task<IActionResult> GetPhoto(int id)
    {
        var jobSeeker = await _repository.GetOne(id);

        var stream = new MemoryStream(jobSeeker.Photo);

        return new FileStreamResult(stream, "image/jpeg");
    }

    [HttpPut("AddPhoto")]
    public async Task AddPhoto(int id, [FromForm] IFormFile photo)
    {
        var jobSeeker = await _repository.GetOne(id);
        using (var target = new MemoryStream())
        {
            await photo.CopyToAsync(target);
            jobSeeker.Photo = target.ToArray();
        }

        await _repository.Update(jobSeeker);
    }

    [HttpGet("GetResumes")]
    public async Task<IActionResult> GetResumes(int id)
    {
        var resumes = await _repository.GetResumes(id);
        var resume = resumes.FirstOrDefault();

        var stream = new MemoryStream(resume?.File);

        return new FileStreamResult(stream, "application/pdf");
    }

    [HttpPost("AddJobSeeker")]
    public async Task AddJobSeeker(JobSeeker jobSeeker) => await _repository.Add(jobSeeker);

    [HttpPut("UpdateJobSeeker")]
    public async Task UpdateJobSeeker(JobSeeker jobSeeker) => await _repository.Update(jobSeeker);

    [HttpDelete("DeleteJobSeeker{id}")]
    public async Task DeleteJobSeeker(int id) => await _repository.Delete(id);
}