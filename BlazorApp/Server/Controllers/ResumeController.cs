using BlazorApp.Application.Interfaces;
using BlazorApp.Bll.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Server.Controllers;

public class ResumeController : ControllerBase
{
    private readonly IRepository<Resume> _repository;

    public ResumeController(IRepository<Resume> repository)
    {
        _repository = repository;
    }

    [HttpGet("GetResumes")]
    public async Task<IReadOnlyList<Resume>> GetResumes() => await _repository.GetList();

    [HttpGet("GetResume{id}")]
    public async Task<Resume> GetResume(int id) => await _repository.GetOne(id);

    [HttpPost("AddResume")]
    public async Task AddResume(Resume resume) => await _repository.Add(resume);

    [HttpPut("UpdateResume")]
    public async Task UpdateResume(Resume resume) => await _repository.Update(resume);

    [HttpDelete("DeleteResume{id}")]
    public async Task DeleteResume(int id) => await _repository.Delete(id);
}