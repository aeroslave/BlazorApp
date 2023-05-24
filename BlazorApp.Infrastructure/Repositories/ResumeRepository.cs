using BlazorApp.Application.Interfaces;
using BlazorApp.Bll.Models;
using BlazorApp.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Infrastructure.Repositories;

public class ResumeRepository : IRepository<Resume>
{
    private readonly ApplicationDbContext _dbContext;

    public ResumeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Resume>> GetList()
    {
        var resumes = await _dbContext.Resumes.ToListAsync();
        return resumes.AsReadOnly();
    }

    public async Task<Resume> GetOne(int id)
    {
        var resume = await _dbContext.Resumes.FirstOrDefaultAsync(it => it.Id == id);

        if (resume is null)
        {
            throw new InfrastructureException("Резюме с заданным идентификатором не найдено");
        }

        return resume;
    }

    public async Task Add(Resume resume)
    {
        await _dbContext.Resumes.AddAsync(resume);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Resume resume)
    {
        _dbContext.Entry(resume).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var resume = await _dbContext.Resumes.FirstOrDefaultAsync(it => it.Id == id);

        if (resume is null)
        {
            throw new InfrastructureException("Резюме с заданным идентификатором не найдено");
        }

        _dbContext.Resumes.Remove(resume);
        await _dbContext.SaveChangesAsync();
    }
}