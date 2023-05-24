using BlazorApp.Bll.Models;

namespace BlazorApp.Application.Interfaces;

public interface IRepository<T>
{
    public Task<IReadOnlyList<T>> GetList();
    public Task<T> GetOne(int id);
    public Task Add(T item);
    public Task Update(T item);
    public Task Delete(int id);
}