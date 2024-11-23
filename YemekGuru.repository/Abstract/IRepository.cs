namespace YemekGuru.repository.Abstract;

public interface IRepository<T>
{
    public Task<bool> CreateAsync(T entity);
    public Task<bool> DeleteAsync(T entity);
    public Task<bool> UpdateAsync(T entity);
    public Task<List<T>> GetAllAsync();

}