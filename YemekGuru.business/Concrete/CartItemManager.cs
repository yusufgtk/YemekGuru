using YemekGuru.business.Abstract;
using YemekGuru.entity;
using YemekGuru.repository.Abstract;

namespace YemekGuru.business.Concrete;

public class CartItemManager : ICartItemService
{
    private readonly ICartItemRepository _cartItemRepository;
    public CartItemManager(ICartItemRepository cartItemRepository)
    {
        _cartItemRepository=cartItemRepository;
    }
    public Task<bool> CreateAsync(entity.CartItem entity)
    {
        return _cartItemRepository.CreateAsync(entity);
    }

    public Task<bool> DeleteAsync(entity.CartItem entity)
    {
        return _cartItemRepository.DeleteAsync(entity);
    }

    public Task<List<entity.CartItem>> GetAllAsync()
    {
        return _cartItemRepository.GetAllAsync();
    }

    public Task<List<entity.CartItem>> GetCartItemsByCartIdAsync(int cartId)
    {
        return _cartItemRepository.GetCartItemsByCartIdAsync(cartId);
    }

    public Task<List<CartItem>> GetCartItemsByProductIdAsync(int productId)
    {
        return _cartItemRepository.GetCartItemsByProductIdAsync(productId);
    }

    public Task<int> HaveCartItemUsersNumber()
    {
        return _cartItemRepository.HaveCartItemUsersNumber();
    }

    public Task<bool> IsThereAnyProductAsync(int productId)
    {
        return _cartItemRepository.IsThereAnyProductAsync(productId);
    }

    public Task<bool> UpdateAsync(entity.CartItem entity)
    {
        return _cartItemRepository.UpdateAsync(entity);
    }
}
