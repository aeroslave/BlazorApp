using BlazorApp.Application.Interfaces;
using BlazorApp.Bll.Models;
using BlazorApp.Bll.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Server.Controllers;

public class ResumeController : ControllerBase
{
    private readonly IRepository<Resume> _repository;

    public ResumeController(IRepository<Resume> repository)
    {
        _repository = repository;
    }

    [HttpGet("GetResumes")]
    public async Task<IReadOnlyList<Resume>> GetResumes()
    {
        return await _repository.GetList();
    }

    [HttpGet("GetResume{id}")]
    public async Task<Resume> GetResume(int id) => await _repository.GetOne(id);

    [HttpPost("AddResume")]
    public async Task AddResume(ResumeDto resumeDto, [FromForm]IFormFile file)
    {
        if (file.Length is <= 0 or >= 1000000) 
            throw new Exception("Файл не должен быть пустым или не превышать 10 Мб");

        var resume = new Resume
        {
            Description = resumeDto.Description,
            UserId = resumeDto.UserId
        };

        using (var target = new MemoryStream())
        {
            await file.CopyToAsync(target);
            resume.File = target.ToArray();
        }

        await _repository.Add(resume);
    }

    [HttpPut("UpdateResume")]
    public async Task UpdateResume(ResumeDto resumeDto)
    {
        var file = Request.Form.Files[0];

        if (file.Length is <= 0 or >= 1000000)
            throw new Exception("Файл не должен быть пустым или не превышать 10 Мб");

        var resume = new Resume
        {
            Description = resumeDto.Description,
            UserId = resumeDto.UserId
        };

        using (var target = new MemoryStream())
        {
            await file.CopyToAsync(target);
            resume.File = target.ToArray();
        }

        await _repository.Update(resume);
    }

    [HttpDelete("DeleteResume{id}")]
    public async Task DeleteResume(int id) => await _repository.Delete(id);
}