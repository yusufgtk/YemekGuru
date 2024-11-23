using System.Text.RegularExpressions;
using YemekGuru.business.Abstract;
using YemekGuru.entity;
using YemekGuru.repository.Abstract;

namespace YemekGuru.business.Concrete;

public class RestaurantManager : IRestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;
    public RestaurantManager(IRestaurantRepository restaurantRepository)
    {
        _restaurantRepository=restaurantRepository;
    }

    public async Task<List<Restaurant>> Admin_GetNotApprovedRestaurants30Async()
    {
        return await _restaurantRepository.Admin_GetNotApprovedRestaurants30Async();
    }

    public async Task<List<Restaurant>> Admin_GetNotApprovedRestaurantsBySearchAsync(string search)
    {
        return await _restaurantRepository.Admin_GetNotApprovedRestaurantsBySearchAsync(search);
    }

    public async Task<List<Restaurant>> Admin_GetRestaurants30Async()
    {
        return await _restaurantRepository.Admin_GetRestaurants30Async();
    }

    public async Task<List<Restaurant>> Admin_GetRestaurantsBySearchAsync(string search)
    {
        return await _restaurantRepository.Admin_GetRestaurantsBySearchAsync(search);
    }

    public Task<int> CloseRestaurantsNumberAsync()
    {
        return _restaurantRepository.CloseRestaurantsNumberAsync();
    }

    public async Task<bool> CreateAsync(Restaurant entity)
    {
        return await _restaurantRepository.CreateAsync(entity);
    }

    public async Task<bool> DeleteAsync(Restaurant entity)
    {
        return await _restaurantRepository.DeleteAsync(entity);
    }

    public async Task<List<Restaurant>> GetAllAsync()
    {
        return await _restaurantRepository.GetAllAsync();
    }

    public async Task<Restaurant> GetRestaurantByIdAsync(int id)
    {
        return await _restaurantRepository.GetRestaurantByIdAsync(id);
    }

    public Task<Restaurant> GetRestaurantByUserIdAsync(string userId)
    {
        return _restaurantRepository.GetRestaurantByUserIdAsync(userId);
    }

    public async Task<List<Restaurant>> GetRestaurantsByLocationAsync(string country, int? cityId, int? districtId)
    {
        return await _restaurantRepository.GetRestaurantsByLocationAsync(country="Turkey",cityId,districtId);
    }

    public async Task<List<Restaurant>> GetRestaurantsByLocationSeachNameAsync(string? searchName, string country, int? cityId, int? districtId)
    {
        return await _restaurantRepository.GetRestaurantsByLocationSeachNameAsync(searchName,country="Turkey",cityId,districtId);
    }

    public async Task<int> NotApprovedRestaurantNumberAsync()
    {
        return await _restaurantRepository.NotApprovedRestaurantNumberAsync();
    }

    public Task<int> OpenRestaurantsNumberAsync()
    {
        return _restaurantRepository.OpenRestaurantsNumberAsync();
    }

    public Task<int> TotalRestaurantsNumberAsync()
    {
        return _restaurantRepository.TotalRestaurantsNumberAsync();
    }

    public async Task<bool> UpdateAsync(Restaurant entity)
    {
        return await _restaurantRepository.UpdateAsync(entity);
    }
}
