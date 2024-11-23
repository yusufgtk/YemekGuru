using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YemekGuru.business.Abstract;
using YemekGuru.entity;
using YemekGuru.webapp.Identity;
using YemekGuru.webapp.Models;


namespace YemekGuru.webapp.Controllers;

public class HomeController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IRestaurantService _restaurantService;
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IAddressService _addressService;
    private readonly ICityService _cityService;
    private readonly IDistrictService _districtService;

    public HomeController(UserManager<User> userManager, SignInManager<User> signInManager, IRestaurantService restaurantService, IProductService productService, ICategoryService categoryService, IAddressService addressService, ICityService cityService, IDistrictService districtService)
    {
        _userManager=userManager;
        _signInManager=signInManager;
        _restaurantService=restaurantService;
        _productService=productService;
        _categoryService=categoryService;
        _addressService=addressService;
        _cityService=cityService;
        _districtService=districtService;
    }
    public async Task<IActionResult> Index()
    {
        string country="Turkey";
        int? cityId=34;
        int? districtId=2004;
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user!=null)
        {
            var cities=await _cityService.GetCitiesAsync();
            var districts=await _districtService.GetDistrictsAsync();
            
            
            if(user.AddressId!=null)
            {
                var address = await _addressService.GetAddressByIdAsync(user.AddressId);
                foreach(var city in cities)
                {
                    if(city.Id==address.City)
                        ViewBag.City=city.Name;
                }
                foreach(var district in districts)
                {
                    if(district.Id==address.District)
                        ViewBag.District=district.Name;
                }
                ViewBag.AddressTitle=address.Title;
                ViewBag.FullAddress=address.FullAddress;
                cityId=address.City;
                districtId=address.District;
            }
        }
        List<RestaurantModel> restaurantModels = new List<RestaurantModel>(){};
        var restaurants=await _restaurantService.GetRestaurantsByLocationAsync(country,cityId,districtId);
        if(restaurants!=null)
        {
            foreach(var r in restaurants)
            {
                restaurantModels.Add(
                    new RestaurantModel(){
                        Id=r.Id,
                        Name=r.Name,
                        ImageUrl=r.ImageUrl,
                        Country=r.Country,
                        CityId=r.CityId,
                        DistrictId=r.DistrictId,
                        DeliveryTime=r.DeliveryTime,
                        MinimumOrderPrice=r.MinimumOrderPrice,
                        Rating=r.Rating,
                        IsActive=r.IsActive
                    }
                );
            }

        }
        var categories = await _categoryService.GetCategoriesAsync();
        List<CategoryModel> categoryModels = new List<CategoryModel>(){};
        foreach(var c in categories)
        {
            categoryModels.Add(
                new CategoryModel(){
                    Id=c.Id,
                    Name=c.Name,
                    Description=c.Description,
                    ImageUrl=c.ImageUrl
                }
            );
        }
        HomePageViewModel homePageViewModel = new HomePageViewModel();
        homePageViewModel.RestaurantModels=restaurantModels;
        homePageViewModel.CategoryModels=categoryModels;
        
        ViewBag.City=await _cityService.GetCityByIdAsync(cityId);
        ViewBag.District=await _districtService.GetDistrictByIdAsync(districtId);

        return View(homePageViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Index([FromForm] string? searchName)
    {
        string country="Turkey";
        int? cityId=34;
        int? districtId=2004;
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user!=null)
        {
            var cities=await _cityService.GetCitiesAsync();
            var districts=await _districtService.GetDistrictsAsync();
            
            
            if(user.AddressId!=null)
            {
                var address = await _addressService.GetAddressByIdAsync(user.AddressId);
                foreach(var city in cities)
                {
                    if(city.Id==address.City)
                        ViewBag.City=city.Name;
                }
                foreach(var district in districts)
                {
                    if(district.Id==address.District)
                        ViewBag.District=district.Name;
                }
                ViewBag.AddressTitle=address.Title;
                ViewBag.FullAddress=address.FullAddress;
                cityId=address.City;
                districtId=address.District;
            }
        }
        List<RestaurantModel> restaurantModels = new List<RestaurantModel>(){};
        List<Restaurant> restaurants;
        if(searchName!=null)
        {
            restaurants = await _restaurantService.GetRestaurantsByLocationSeachNameAsync(searchName,country,cityId,districtId);
        }
        else
        {
            restaurants = await _restaurantService.GetRestaurantsByLocationAsync(country,cityId,districtId);
        }
        if(restaurants!=null)
        {
            foreach(var r in restaurants)
            {
                restaurantModels.Add(
                    new RestaurantModel(){
                        Id=r.Id,
                        Name=r.Name,
                        ImageUrl=r.ImageUrl,
                        Country=r.Country,
                        CityId=r.CityId,
                        DistrictId=r.DistrictId,
                        DeliveryTime=r.DeliveryTime,
                        MinimumOrderPrice=r.MinimumOrderPrice,
                        Rating=r.Rating,
                        IsActive=r.IsActive
                    }
                );
            }

        }
        var categories = await _categoryService.GetCategoriesAsync();
        List<CategoryModel> categoryModels = new List<CategoryModel>(){};
        foreach(var c in categories)
        {
            categoryModels.Add(
                new CategoryModel(){
                    Id=c.Id,
                    Name=c.Name,
                    Description=c.Description,
                    ImageUrl=c.ImageUrl
                }
            );
        }
        HomePageViewModel homePageViewModel = new HomePageViewModel();
        homePageViewModel.RestaurantModels=restaurantModels;
        homePageViewModel.CategoryModels=categoryModels;
        ViewBag.City=await _cityService.GetCityByIdAsync(cityId);
        ViewBag.District=await _districtService.GetDistrictByIdAsync(districtId);
        return View(homePageViewModel);
        

    }



    public async Task<IActionResult> ListProducts(int? categoryId)
    {
        string country="Turkey";
        int? cityId=34;
        int? districtId=2004;
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user!=null)
        {
            var cities=await _cityService.GetCitiesAsync();
            var districts=await _districtService.GetDistrictsAsync();
            
            
            if(user.AddressId!=null)
            {
                var address = await _addressService.GetAddressByIdAsync(user.AddressId);
                foreach(var city in cities)
                {
                    if(city.Id==address.City)
                        ViewBag.City=city.Name;
                }
                foreach(var district in districts)
                {
                    if(district.Id==address.District)
                        ViewBag.District=district.Name;
                }
                ViewBag.AddressTitle=address.Title;
                ViewBag.FullAddress=address.FullAddress;
                cityId=address.City;
                districtId=address.District;
            }
        }
        List<Product> products = new List<Product>(){};
        List<ProductModel> productModels = new List<ProductModel>(){};

        if(categoryId!=null)
        {
            products = await _productService.GetProductsByLocationCategoryAsync(categoryId,country,cityId,districtId);
        }
        else
        {
            products = await _productService.GetProductsByLocationAsync(country,cityId,districtId); 
        }

        foreach(var p in products)
        {
            productModels.Add(new ProductModel(){
                Id=p.Id,
                Name=p.Name,
                Content=p.Content,
                RestaurantId=p.RestaurantId,
                CategoryId=p.CategoryId,
                ImageUrl=p.ImageUrl,
                Calorie=p.Calorie,
                Price=p.Price,
                PreviousPrice=p.PreviousPrice
            });
        }
        List<CategoryModel> categoryModels = new List<CategoryModel>(){};
        var categories = await _categoryService.GetCategoriesAsync();
        foreach(var c in categories)
        {
            categoryModels.Add(new CategoryModel(){
                Id=c.Id,
                Name=c.Name,
                ImageUrl=c.ImageUrl,
                Description=c.Description
            });
        }
        ListProductsViewModel listProductsViewModel = new ListProductsViewModel();
        listProductsViewModel.ProductModels=productModels;
        listProductsViewModel.CategoryModels=categoryModels;
        
        ViewBag.City=await _cityService.GetCityByIdAsync(cityId);
        ViewBag.District=await _districtService.GetDistrictByIdAsync(districtId);
        return View(listProductsViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> ListProducts([FromForm] string? searchName)
    {
        string country="Turkey";
        int? cityId=34;
        int? districtId=2004;
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user!=null)
        {
            var cities=await _cityService.GetCitiesAsync();
            var districts=await _districtService.GetDistrictsAsync();
            
            
            if(user.AddressId!=null)
            {
                var address = await _addressService.GetAddressByIdAsync(user.AddressId);
                foreach(var city in cities)
                {
                    if(city.Id==address.City)
                        ViewBag.City=city.Name;
                }
                foreach(var district in districts)
                {
                    if(district.Id==address.District)
                        ViewBag.District=district.Name;
                }
                ViewBag.AddressTitle=address.Title;
                ViewBag.FullAddress=address.FullAddress;
                cityId=address.City;
                districtId=address.District;
            }
        }


        List<Product> products = new List<Product>(){};
        List<ProductModel> productModels = new List<ProductModel>(){};
        if(searchName!=null)
        {
            products = await _productService.GetProductsByLocationSearchNameAsync(searchName,country,cityId,districtId);
        }
        else
        {
            products = await _productService.GetProductsByLocationAsync(country,cityId,districtId);
        }

        foreach(var p in products)
        {
            productModels.Add(new ProductModel(){
                Id=p.Id,
                Name=p.Name,
                Content=p.Content,
                RestaurantId=p.RestaurantId,
                CategoryId=p.CategoryId,
                ImageUrl=p.ImageUrl,
                Calorie=p.Calorie,
                Price=p.Price,
                PreviousPrice=p.PreviousPrice
            });
        }
        List<CategoryModel> categoryModels = new List<CategoryModel>(){};
        var categories = await _categoryService.GetCategoriesAsync();
        foreach(var c in categories)
        {
            categoryModels.Add(new CategoryModel(){
                Id=c.Id,
                Name=c.Name,
                ImageUrl=c.ImageUrl,
                Description=c.Description
            });
        }
        ListProductsViewModel listProductsViewModel = new ListProductsViewModel();
        listProductsViewModel.ProductModels=productModels;
        listProductsViewModel.CategoryModels=categoryModels;
        ViewBag.City=await _cityService.GetCityByIdAsync(cityId);
        ViewBag.District=await _districtService.GetDistrictByIdAsync(districtId);
        return View(listProductsViewModel);
    }


}
