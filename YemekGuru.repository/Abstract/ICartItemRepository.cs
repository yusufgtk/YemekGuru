using YemekGuru.entity;

namespace YemekGuru.repository.Abstract;

public interface ICartItemRepository:IRepository<CartItem>
{
    public Task<List<CartItem>> GetCartItemsByCartIdAsync(int cartId);
    public Task<List<CartItem>> GetCartItemsByProductIdAsync(int productId);
    public Task<bool> IsThereAnyProductAsync(int ProductId);

    public Task<int> HaveCartItemUsersNumber();
}