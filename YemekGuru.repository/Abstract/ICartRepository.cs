using YemekGuru.entity;

namespace YemekGuru.repository.Abstract;

public interface ICartRepository:IRepository<Cart>
{
    public Task<Cart> GetCartByUserIdAsync(string userId);
}