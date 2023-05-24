using BlazorApp.Application.Interfaces;
using BlazorApp.Bll.Models;
using BlazorApp.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Infrastructure.Repositories;

public class InterviewRepository : IRepository<Interview>
{
    private readonly ApplicationDbContext _dbContext;

    public InterviewRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Interview>> GetList()
    {
        var interviews = await _dbContext.Interviews.ToListAsync();
        return interviews.AsReadOnly();
    }

    public async Task<Interview> GetOne(int id)
    {
        var interview = await _dbContext.Interviews.FirstOrDefaultAsync(it => it.Id == id);

        if (interview is null)
        {
            throw new InfrastructureException("Интервью с заданным идентификатором не найдено");
        }

        return interview;
    }

    public async Task Add(Interview interview)
    {
        await _dbContext.Interviews.AddAsync(interview);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Interview interview)
    {
        _dbContext.Entry(interview).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var interview = await _dbContext.Interviews.FirstOrDefaultAsync(it => it.Id == id);

        if (interview is null)
        {
            throw new InfrastructureException("Интервью с заданным идентификатором не найдено");
        }

        _dbContext.Interviews.Remove(interview);
        await _dbContext.SaveChangesAsync();
    }
}