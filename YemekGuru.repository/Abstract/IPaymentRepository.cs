using YemekGuru.entity;

namespace YemekGuru.repository.Abstract;

public interface IPaymentRepository:IRepository<Payment>
{
    // public Task<Payment> GetPaymentByIdAsync(int Id);
    // public Task<Payment> GetPaymentByOrderIdAsync(int orderId);
    // public Task<List<Payment>> GetPaymentsAsync();

    // public Task<Payment> GetPaymentByRestaurantIdAndOrderIdAsync(int restaurantId, int orderId);
    // public Task<List<Payment>> GetPaymentsByRestaurantIdAsync(int restaurantId);
    // public Task<List<Payment>> GetPaymentsPoolByRestaurantIdAsync(int restaurantId);
    // public Task<List<Payment>> GetPaymentsSafeByRestaurantIdAsync(int restaurantId);
    // public Task<List<Payment>> GetPaymentsCompletedByRestaurantIdAsync(int restaurantId);
    // public Task<List<Payment>> GetPaymentsByUserIdAsync(string userName);
    // public Task<Payment> GetPaymentByUserNameAndOrderIdAsync(int orderId);
    // public Task<List<Payment>> Admin_GetPaymentsByAsync();
    // public Task<List<Payment>> Admin_GetPaymentsPoolAsync();
    // public Task<List<Payment>> Admin_GetPaymentsSafeByRestaurantIdAsync(int restaurantId);
    // public Task<List<Payment>> Admin_GetPaymentsCompletedByRestaurantIdAsync(int restaurantId);
}