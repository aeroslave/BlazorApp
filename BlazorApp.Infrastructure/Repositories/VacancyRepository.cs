using BlazorApp.Application.Interfaces;
using BlazorApp.Bll.Models;
using BlazorApp.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Infrastructure.Repositories;

public class VacancyRepository : IRepository<Vacancy>
{
    private readonly ApplicationDbContext _dbContext;

    public VacancyRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Vacancy>> GetList()
    {
        var vacancies = await _dbContext.Vacancies.ToListAsync();
        return vacancies.AsReadOnly();
    }

    public async Task<Vacancy> GetOne(int id)
    {
        var vacancy = await _dbContext.Vacancies.FirstOrDefaultAsync(it => it.Id == id);

        if (vacancy is null)
        {
            throw new InfrastructureException("Вакансия с заданным идентификатором не найдена");
        }

        return vacancy;
    }

    public async Task Add(Vacancy vacancy)
    {
        await _dbContext.Vacancies.AddAsync(vacancy);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Vacancy vacancy)
    {
        _dbContext.Entry(vacancy).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var vacancy = await _dbContext.Vacancies.FirstOrDefaultAsync(it => it.Id == id);

        if (vacancy is null)
        {
            throw new InfrastructureException("Вакансия с заданным идентификатором не найден");
        }

        _dbContext.Vacancies.Remove(vacancy);
        await _dbContext.SaveChangesAsync();
    }
}