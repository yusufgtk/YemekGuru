using YemekGuru.entity;

namespace YemekGuru.business.Abstract;

public interface IOrderService
{
    public Task<bool> CreateAsync(Order entity);
    public Task<bool> DeleteAsync(Order entity);
    public Task<bool> UpdateAsync(Order entity);
    public Task<List<Order>> GetAllAsync();
    public Task<Order> GetOrderByIdAsync(int? id);
    public Task<List<Order>> GetOrdersByUserIdAsync(string userId);
    public Task<List<Order>> GetOrderByRestaurantIdAsync(int? restaurantId);
    public Task<List<Order>> GetActiveOrderByRestaurantIdAsync(int? restaurantId);
    public Task<List<Order>> GetSetOutOrderByRestaurantIdAsync(int? restaurantId);
    public Task<List<Order>> GetCanceledOrderByRestaurantIdAsync(int? restaurantId);
    public Task<List<Order>> GetComplatedOrderByRestaurantIdAsync(int? restaurantId);
    public Task<int> GetTotalComplatedOrderByRestaurantIdAsync(int? restaurantId);
    public Task<int> GetTotalCanceledOrderByRestaurantIdAsync(int? restaurantId);
    public Task<int> GetTotalSetOutOrderByRestaurantIdAsync(int? restaurantId);
    public Task<int> GetTotalActiveOrderByRestaurantIdAsync(int? restaurantId);
    public Task<int> GetTotalOrderByRestaurantIdAsync(int? restaurantId);
    public Task<List<Order>> GetCompletedOrderByRestaurantIdAndOrderIdAsync(int? restaurantId, int? orderId);
    //admin
    public Task<int> Admin_TotalOrdersCountAsync();
    public Task<int> Admin_TotalActiveOrdersCountAsync();
    public Task<int> Admin_TotalCompletedOrdersCountAsync();
    public Task<int> Admin_TotalCanceledOrdersCountAsync();
    public Task<int> Admin_TotalSetOutOrdersCountAsync();
    public Task<List<Order>> Admin_GetOrders30Async();
    public Task<List<Order>> Admin_GetOrdersByOrderIdAsync(int? orderId);
    public Task<Dictionary<int,int>> Admin_ShortOrdersByCategoryAsync();
}