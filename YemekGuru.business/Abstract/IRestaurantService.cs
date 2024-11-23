using YemekGuru.entity;

namespace YemekGuru.business.Abstract;

public interface IRestaurantService
{
    public Task<bool> CreateAsync(Restaurant entity);
    public Task<bool> DeleteAsync(Restaurant entity);
    public Task<bool> UpdateAsync(Restaurant entity);
    public Task<List<Restaurant>> GetAllAsync();
    public Task<Restaurant> GetRestaurantByIdAsync(int id);
    public Task<Restaurant> GetRestaurantByUserIdAsync(string userId);
    public Task<int> TotalRestaurantsNumberAsync();
    public Task<int> OpenRestaurantsNumberAsync();
    public Task<int> CloseRestaurantsNumberAsync();
    public Task<int> NotApprovedRestaurantNumberAsync();
    public Task<List<Restaurant>> GetRestaurantsByLocationAsync(string country, int? cityId, int? districtId);
    public Task<List<Restaurant>> GetRestaurantsByLocationSeachNameAsync(string? searchName, string country, int? cityId, int? districtId);
    public Task<List<Restaurant>> Admin_GetRestaurants30Async();
    public Task<List<Restaurant>> Admin_GetRestaurantsBySearchAsync(string search);
    public Task<List<Restaurant>> Admin_GetNotApprovedRestaurants30Async();
    public Task<List<Restaurant>> Admin_GetNotApprovedRestaurantsBySearchAsync(string search);
}