using YemekGuru.business.Abstract;
using YemekGuru.entity;
using YemekGuru.repository.Abstract;

namespace YemekGuru.business.Concrete;

public class OrderManager : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    public OrderManager(IOrderRepository orderRepository)
    {
        _orderRepository=orderRepository;
    }
    public async Task<bool> CreateAsync(Order entity)
    {
        return await _orderRepository.CreateAsync(entity);
    }

    public async Task<bool> DeleteAsync(Order entity)
    {
        return await _orderRepository.DeleteAsync(entity);
    }

    public async Task<List<Order>> GetActiveOrderByRestaurantIdAsync(int? restaurantId)
    {
        return await _orderRepository.GetActiveOrderByRestaurantIdAsync(restaurantId);
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _orderRepository.GetAllAsync();
    }

    public async Task<List<Order>> GetCanceledOrderByRestaurantIdAsync(int? restaurantId)
    {
        return await _orderRepository.GetCanceledOrderByRestaurantIdAsync(restaurantId);
    }

    public async Task<List<Order>> GetComplatedOrderByRestaurantIdAsync(int? restaurantId)
    {
        return await _orderRepository.GetComplatedOrderByRestaurantIdAsync(restaurantId);
    }

    public async Task<Order> GetOrderByIdAsync(int? id)
    {
        return await _orderRepository.GetOrderByIdAsync(id);
    }

    public async Task<List<Order>> GetOrderByRestaurantIdAsync(int? restaurantId)
    {
        return await _orderRepository.GetOrderByRestaurantIdAsync(restaurantId);
    }

    public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
    {
        return await _orderRepository.GetOrdersByUserIdAsync(userId);
    }

    public async Task<List<Order>> GetSetOutOrderByRestaurantIdAsync(int? restaurantId)
    {
        return await _orderRepository.GetSetOutOrderByRestaurantIdAsync(restaurantId);
    }

    public async Task<int> GetTotalActiveOrderByRestaurantIdAsync(int? restaurantId)
    {
        return await _orderRepository.GetTotalActiveOrderByRestaurantIdAsync(restaurantId);
    }

    public async Task<int> GetTotalCanceledOrderByRestaurantIdAsync(int? restaurantId)
    {
        return await _orderRepository.GetTotalCanceledOrderByRestaurantIdAsync(restaurantId);
    }

    public async Task<int> GetTotalComplatedOrderByRestaurantIdAsync(int? restaurantId)
    {
        return await _orderRepository.GetTotalComplatedOrderByRestaurantIdAsync(restaurantId);
    }

    public async Task<List<Order>> GetCompletedOrderByRestaurantIdAndOrderIdAsync(int? restaurantId, int? orderId)
    {
        return await _orderRepository.GetCompletedOrderByRestaurantIdAndOrderIdAsync(restaurantId,orderId);
    }

    public async Task<int> GetTotalOrderByRestaurantIdAsync(int? restaurantId)
    {
        return await _orderRepository.GetTotalOrderByRestaurantIdAsync(restaurantId);
    }

    public async Task<int> GetTotalSetOutOrderByRestaurantIdAsync(int? restaurantId)
    {
        return await _orderRepository.GetTotalSetOutOrderByRestaurantIdAsync(restaurantId);
    }

    public async Task<bool> UpdateAsync(Order entity)
    {
        return await _orderRepository.UpdateAsync(entity);
    }

    public async Task<int> Admin_TotalOrdersCountAsync()
    {
        return await _orderRepository.Admin_TotalOrdersCountAsync();
    }

    public async Task<int> Admin_TotalActiveOrdersCountAsync()
    {
        return await _orderRepository.Admin_TotalActiveOrdersCountAsync();
    }

    public async Task<int> Admin_TotalCompletedOrdersCountAsync()
    {
        return await _orderRepository.Admin_TotalCompletedOrdersCountAsync();
    }

    public async Task<int> Admin_TotalCanceledOrdersCountAsync()
    {
        return await _orderRepository.Admin_TotalCanceledOrdersCountAsync();
    }

    public async Task<int> Admin_TotalSetOutOrdersCountAsync()
    {
        return await _orderRepository.Admin_TotalSetOutOrdersCountAsync();
    }

    public async Task<List<Order>> Admin_GetOrders30Async()
    {
        return await _orderRepository.Admin_GetOrders30Async();
    }

    public async Task<List<Order>> Admin_GetOrdersByOrderIdAsync(int? orderId)
    {
        return await _orderRepository.Admin_GetOrdersByOrderIdAsync(orderId);
    }

    public async Task<Dictionary<int, int>> Admin_ShortOrdersByCategoryAsync()
    {
        return await _orderRepository.Admin_ShortOrdersByCategoryAsync();
    }
}
