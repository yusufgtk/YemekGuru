using YemekGuru.business.Abstract;
using YemekGuru.entity;
using YemekGuru.repository.Abstract;

namespace YemekGuru.business.Concrete;

public class CartManager : ICartService
{
    private readonly ICartRepository _cartRepository;
    public CartManager(ICartRepository cartRepository)
    {
        _cartRepository=cartRepository;
    }
    public Task<bool> CreateAsync(Cart entity)
    {
        Console.WriteLine("Çalıştı-----------------");
        return _cartRepository.CreateAsync(entity);
    }

    public Task<bool> DeleteAsync(Cart entity)
    {
        return _cartRepository.DeleteAsync(entity);
    }

    public Task<List<Cart>> GetAllAsync()
    {
        return _cartRepository.GetAllAsync();
    }

    public Task<Cart> GetCartByUserIdAsync(string userId)
    {
        return _cartRepository.GetCartByUserIdAsync(userId);
    }

    public Task<bool> UpdateAsync(Cart entity)
    {
        return _cartRepository.UpdateAsync(entity);
    }
}
