using YemekGuru.entity;

namespace YemekGuru.business.Abstract;

public interface ICartService
{
    public Task<bool> CreateAsync(Cart entity);
    public Task<bool> DeleteAsync(Cart entity);
    public Task<bool> UpdateAsync(Cart entity);
    public Task<List<Cart>> GetAllAsync();
    public Task<Cart> GetCartByUserIdAsync(string userId);
}