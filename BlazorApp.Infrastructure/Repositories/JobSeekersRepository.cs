using BlazorApp.Application.Interfaces;
using BlazorApp.Bll.Models;
using BlazorApp.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Infrastructure.Repositories;

public class JobSeekersRepository : IRepository<JobSeeker>
{
    private readonly ApplicationDbContext _dbContext;

    public JobSeekersRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<JobSeeker>> GetList()
    {
        var jobSeekers = await _dbContext.JobSeekers.ToListAsync();
        return jobSeekers.AsReadOnly();
    }

    public async Task<JobSeeker> GetOne(int id)
    {
        var jobSeeker = await _dbContext.JobSeekers.FirstOrDefaultAsync(it => it.Id == id);

        if (jobSeeker is null)
        {
            throw new InfrastructureException("Пользователь с заданным идентификатором не найден");
        }

        return jobSeeker;
    }

    public async Task Add(JobSeeker jobSeeker)
    {
        await _dbContext.JobSeekers.AddAsync(jobSeeker);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(JobSeeker jobSeeker)
    {
        _dbContext.Entry(jobSeeker).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var jobSeeker = await _dbContext.JobSeekers.FirstOrDefaultAsync(it => it.Id == id);

        if (jobSeeker is null)
        {
            throw new InfrastructureException("Пользователь с заданным идентификатором не найден");
        }
            
        _dbContext.JobSeekers.Remove(jobSeeker);
        await _dbContext.SaveChangesAsync();
    }
}