using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YemekGuru.business.Abstract;
using YemekGuru.entity;
using YemekGuru.webapp.Identity;
using YemekGuru.webapp.Models;
using YemekGuru.webapp.Services;

namespace YemekGuru.webapp.Controllers;

[Authorize(Roles="Admin")]
public class AdminController:Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IRestaurantService _restaurantService;
    private readonly IProductService _productService;
    private readonly ICartService _cartService;
    private readonly ICartItemService _cartItemService;
    private readonly ICategoryService _categoryService;
    private readonly ICityService _cityService;
    private readonly IDistrictService _districtService;
    private readonly IApplySellerService _applySellerService;
    private readonly IOrderService _orderService;
    private readonly ICommentService _commentService;
    private readonly IFeedbackService _feedbackService;
    private readonly IComplaintService _complaintService;

    public AdminController(UserManager<User> userManager, SignInManager<User> signInManager, IRestaurantService restaurantService, IProductService productService, ICartService cartService, ICartItemService cartItemService, ICategoryService categoryService, ICityService cityService, IDistrictService districtService, IApplySellerService applySellerService, IOrderService orderService, ICommentService commentService, IFeedbackService feedbackService, IComplaintService complaintService)
    {
        _userManager=userManager;
        _signInManager=signInManager;
        _restaurantService=restaurantService;
        _productService=productService;
        _cartItemService=cartItemService;
        _cartService=cartService;
        _categoryService=categoryService;
        _cityService=cityService;
        _districtService=districtService;
        _applySellerService=applySellerService;
        _orderService=orderService;
        _commentService=commentService;
        _feedbackService=feedbackService;
        _complaintService=complaintService;
    }

    
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();
        
        AdminDashboardViewModel adminDashboardViewModel = new AdminDashboardViewModel();

        // adminDashboardViewModel.LiveUsersCount = _userCountService.GetOnlineUsersCount();
        adminDashboardViewModel.RestaurantsNumber = await _restaurantService.TotalRestaurantsNumberAsync();
        adminDashboardViewModel.OpenRestaurantsNumber = await _restaurantService.OpenRestaurantsNumberAsync();
        adminDashboardViewModel.CloseRestaurantsNumber = await _restaurantService.CloseRestaurantsNumberAsync();
        adminDashboardViewModel.UsersNumber = await _userManager.Users.CountAsync();
        adminDashboardViewModel.ProductsNumber = await _productService.TotalProdutsNumberAsync();
        adminDashboardViewModel.CategoriesNumber = await _categoryService.TotalCategoriesNumberAsync();
        adminDashboardViewModel.HaveCartItemsUserNumber = await _cartItemService.HaveCartItemUsersNumber();
        adminDashboardViewModel.ConfirmWaitApplySellerNumber = await _applySellerService.ConfirmWaitApplySellerCountAsync();
        adminDashboardViewModel.OrdersNumber=await _orderService.Admin_TotalOrdersCountAsync();
        adminDashboardViewModel.WaitApprovedProductsCount=await _productService.Admin_GetNotApprovedProductsNumberAsync();
        adminDashboardViewModel.WaitApprovedRestaurantsCount=await _restaurantService.NotApprovedRestaurantNumberAsync();
        adminDashboardViewModel.TotalCommentsCount=await _commentService.Admin_TotalCommentsCountAsync();
        adminDashboardViewModel.WaitApprovedCommentsCount=await _commentService.Admin_TotalWaitapprovedCommentsCountAsync();
        adminDashboardViewModel.WaitApprovedFeedbackCount=await _feedbackService.WaitFeedbacksCountAsync();
        adminDashboardViewModel.ActiveComplaintsCount=await _complaintService.TotalActiveComplaintsCountAsync();
        adminDashboardViewModel.TotalCompletedComplaintCount=await _complaintService.TotalCompletedComplaintsCountAsync();
        return View(adminDashboardViewModel);
    }

    // Restaurant işlemleri ************************************************
    [HttpGet]
    public async Task<IActionResult> ListRestaurants()
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();
        
        var restaurants = await _restaurantService.Admin_GetRestaurants30Async();
        List<RestaurantModel> restaurantModels = new List<RestaurantModel>(){};
        foreach(var restaurant in restaurants)
        {
            restaurantModels.Add(new RestaurantModel(){
                Id=restaurant.Id,
                UserId=restaurant.UserId,
                Name=restaurant.Name,
                Description=restaurant.Description,
                ImageUrl=restaurant.ImageUrl,
                Country=restaurant.Country,
                CityId=restaurant.CityId,
                DistrictId=restaurant.DistrictId,
                Address=restaurant.Address,
                PhoneNumber=restaurant.PhoneNumber,
                EmailAdress=restaurant.EmailAdress,
                IsActive=restaurant.IsActive,
                IsApproved=restaurant.IsApproved,
                MinimumOrderPrice=restaurant.MinimumOrderPrice,
                OpeningTime=restaurant.OpeningTime,
                ClosingTime=restaurant.ClosingTime,
                DeliveryTime=restaurant.DeliveryTime,
                Rating=restaurant.Rating,
                CreatedAt=restaurant.CreatedAt,
                UpdatedAt=restaurant.UpdatedAt
            });
        }
        ViewBag.Cities=await _cityService.GetCitiesAsync();
        ViewBag.Districts=await _districtService.GetDistrictsAsync();
        return View(restaurantModels);
    }

    [HttpPost]
    public async Task<IActionResult> ListRestaurants([FromForm] string? search)
    {
        List<Restaurant> restaurants; 
        List<RestaurantModel> restaurantModels = new List<RestaurantModel>(){};
        if(search!=null)
        {
            restaurants = await _restaurantService.Admin_GetRestaurantsBySearchAsync(search);
        }
        else
        {
            restaurants = await _restaurantService.Admin_GetRestaurants30Async();
            
        }
        foreach(var restaurant in restaurants)
        {
            restaurantModels.Add(new RestaurantModel(){
                Id=restaurant.Id,
                UserId=restaurant.UserId,
                Name=restaurant.Name,
                Description=restaurant.Description,
                ImageUrl=restaurant.ImageUrl,
                Country=restaurant.Country,
                CityId=restaurant.CityId,
                DistrictId=restaurant.DistrictId,
                Address=restaurant.Address,
                PhoneNumber=restaurant.PhoneNumber,
                EmailAdress=restaurant.EmailAdress,
                IsActive=restaurant.IsActive,
                IsApproved=restaurant.IsApproved,
                MinimumOrderPrice=restaurant.MinimumOrderPrice,
                OpeningTime=restaurant.OpeningTime,
                ClosingTime=restaurant.ClosingTime,
                DeliveryTime=restaurant.DeliveryTime,
                Rating=restaurant.Rating,
                CreatedAt=restaurant.CreatedAt,
                UpdatedAt=restaurant.UpdatedAt
            });
        }
        ViewBag.Cities=await _cityService.GetCitiesAsync();
        ViewBag.Districts=await _districtService.GetDistrictsAsync();
        return View(restaurantModels);
    }

    [HttpPost]
    public async Task<IActionResult> BanRestaurant([FromForm] int restaurantId)
    {
        var restaurant = await _restaurantService.GetRestaurantByIdAsync(restaurantId);
        if(restaurant!=null)
        {
            restaurant.IsApproved=false;
            var result = await _restaurantService.UpdateAsync(restaurant);
            if(result)
            {
                TempData["Message"] = restaurantId.ToString()+" Nolu restaurant banlandı!";
                TempData["Alert"] = "alert-success";
            }
        }
        return Redirect("/admin/listrestaurants");
    }

    [HttpPost]
    public async Task<IActionResult> NotBanRestaurant([FromForm] int restaurantId)
    {
        var restaurant = await _restaurantService.GetRestaurantByIdAsync(restaurantId);
        if(restaurant!=null)
        {
            restaurant.IsApproved=true;
            var result = await _restaurantService.UpdateAsync(restaurant);
            if(result)
            {
                TempData["Message"] = restaurantId.ToString()+" Nolu restaurantın banı kaldırıldı!";
                TempData["Alert"] = "alert-success";
            }
        }
        return Redirect("/admin/listrestaurants");
    }

    [HttpGet]
    public async Task<IActionResult> RestaurantDetail(int restaurantId)
    {
        Restaurant restaurant = await _restaurantService.GetRestaurantByIdAsync(restaurantId);
        if(restaurant==null)
            return NotFound();

        var user = await _userManager.FindByIdAsync(restaurant.UserId);
        if(user==null)
            return NotFound();
        ViewBag.UserFullName=user.FirstName+" "+user.LastName;
        RestaurantModel restaurantModel = new RestaurantModel(){
                Id=restaurant.Id,
                UserId=restaurant.UserId,
                Name=restaurant.Name,
                Description=restaurant.Description,
                ImageUrl=restaurant.ImageUrl,
                Country=restaurant.Country,
                CityId=restaurant.CityId,
                DistrictId=restaurant.DistrictId,
                Address=restaurant.Address,
                PhoneNumber=restaurant.PhoneNumber,
                EmailAdress=restaurant.EmailAdress,
                IsActive=restaurant.IsActive,
                IsApproved=restaurant.IsApproved,
                MinimumOrderPrice=restaurant.MinimumOrderPrice,
                OpeningTime=restaurant.OpeningTime,
                ClosingTime=restaurant.ClosingTime,
                DeliveryTime=restaurant.DeliveryTime,
                Rating=restaurant.Rating,
                CreatedAt=restaurant.CreatedAt,
                UpdatedAt=restaurant.UpdatedAt
        };
        ViewBag.City = await _cityService.GetCityByIdAsync(restaurant.CityId);
        ViewBag.District = await _districtService.GetDistrictByIdAsync(restaurant.DistrictId);
        return View(restaurantModel);
    }


    // Kullanıcı işlemleri **************************************************************

    [HttpGet]
    public async Task<IActionResult> UserDetail(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if(user==null)
            return NotFound();
        
        var restaurant = await _restaurantService.GetRestaurantByUserIdAsync(userId);
        if(restaurant==null)
            return NotFound();
        ViewBag.RestaurantName=restaurant.Name;
        UserModel userModel = new UserModel(){
            Id=user.Id,
            UserName=user.UserName,
            TCKN=user.TCKN,
            FirstName=user.FirstName,
            LastName=user.LastName,
            PhoneNumber=user.PhoneNumber,
            EmailAddress=user.Email,
            BirthDay=user.BirthDay,
            GuruPuan=user.GuruPuan
        };
        return View(userModel);
    }

    [HttpPost]
    public async Task<IActionResult> BanUser([FromForm] string userId, [FromForm] string returnUrl)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if(user!=null)
        {
            user.IsActive=false;
            var result = await _userManager.UpdateAsync(user);
            if(result.Succeeded)
            {
                TempData["Message"] = user.UserName+" adlı kullanıcı banlandı!";
                TempData["Alert"] = "alert-danger";
            }
        }
        return Redirect(returnUrl);
    }

    [HttpPost]
    public async Task<IActionResult> NotBanUser([FromForm] string userId, [FromForm] string returnUrl)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if(user!=null)
        {
            user.IsActive=true;
            var result = await _userManager.UpdateAsync(user);
            if(result.Succeeded)
            {
                TempData["Message"] = user.UserName+" adlı kullanıcının banı kaldırıldı!";
                TempData["Alert"] = "alert-success";
            }
        }
        return Redirect(returnUrl);
    }

    [HttpGet]
    public async Task<IActionResult> ListUsers()
    {
        var users = await _userManager.Users.Take(30).ToListAsync();
        List<UserModel> userModels = new List<UserModel>(){};
        foreach(var user in users)
        {
            userModels.Add(new UserModel(){
                Id=user.Id,
                RestaurantId=user.RestaurantId,
                UserName=user.UserName,
                ImageUrl=user.ImageUrl,
                TCKN=user.TCKN,
                FirstName=user.FirstName,
                LastName=user.LastName,
                PhoneNumber=user.PhoneNumber,
                EmailAddress=user.Email,
                BirthDay=user.BirthDay,
                GuruPuan=user.GuruPuan,
                
            });
        }
        return View(userModels);
    }

    [HttpPost]
    public async Task<IActionResult> ListUsers([FromForm] string search)
    {
        List<UserModel> userModels = new List<UserModel>(){};
        List<User> users;
        if(search!=null)
        {
            users = await _userManager.Users.Where(i=>i.Id==search||i.Id.Contains(search)||i.UserName.ToLower().Contains(search.ToLower())).ToListAsync();
        }
        else
        {
            users = await _userManager.Users.Take(30).ToListAsync();
        }
        
        
        foreach(var user in users)
        {
            userModels.Add(new UserModel(){
                Id=user.Id,
                RestaurantId=user.RestaurantId,
                UserName=user.UserName,
                ImageUrl=user.ImageUrl,
                TCKN=user.TCKN,
                FirstName=user.FirstName,
                LastName=user.LastName,
                PhoneNumber=user.PhoneNumber,
                EmailAddress=user.Email,
                BirthDay=user.BirthDay,
                GuruPuan=user.GuruPuan,
                
            });
        }
        return View(userModels);
    }

    //Ürün İşlemleri işlemleri ****************************************************

    [HttpGet]
    public async Task<IActionResult> ListProducts()
    {
        var products = await _productService.Admin_GetProducts30Async();
        List<ProductModel> productModels = new List<ProductModel>(){};
        foreach(var product in products)
        {
            productModels.Add(new ProductModel(){
                Id=product.Id,
                RestaurantId=product.RestaurantId,
                Name=product.Name,
                Description=product.Description,
                Content=product.Content,
                ImageUrl=product.ImageUrl,
                PreviousPrice=product.PreviousPrice,
                Price=product.Price,
                Calorie=product.Calorie,
                IsActive=product.IsActive
            });
        }
        return View(productModels);
    }

    [HttpPost]
    public async Task<IActionResult> ListProducts([FromForm] string search)
    {
        List<Product> products;
        if(search!=null)
        {
            products =await _productService.Admin_GetProductsBySearchAsync(search);
        }
        else
        {
            products = await _productService.Admin_GetProducts30Async();
        }
        List<ProductModel> productModels = new List<ProductModel>(){};
        foreach(var product in products)
        {
            productModels.Add(new ProductModel(){
                Id=product.Id,
                RestaurantId=product.RestaurantId,
                Name=product.Name,
                Description=product.Description,
                Content=product.Content,
                ImageUrl=product.ImageUrl,
                PreviousPrice=product.PreviousPrice,
                Price=product.Price,
                Calorie=product.Calorie,
                IsActive=product.IsActive
            });
        }
        return View(productModels);
    }

    [HttpPost]
    public async Task<IActionResult> IsActiveProduct([FromForm] int productId)
    {
        var product = await _productService.GetProductByIdAsync(productId);
        if(product!=null)
        {
            if(product.IsActive==true)
            {
                product.IsActive=false;
                TempData["Message"]=product.Id.ToString()+" id'li ürün satışa kapandı!";
                TempData["Alert"]="alert-danger";
            }
            else
            {
                product.IsActive=true;
                TempData["Message"]=product.Id.ToString()+" id'li ürün satışa Açıldı!";
                TempData["Alert"]="alert-success";
            }
            bool result = await _productService.UpdateAsync(product);
        }
        return Redirect("/admin/listproducts?search="+productId.ToString());
    }
    [HttpPost]
    public async Task<IActionResult> DeleteProduct([FromForm] int productId)
    {
        var product = await _productService.GetProductByIdAsync(productId);
        if(product!=null)
        {
            bool result = await _productService.DeleteAsync(product);
            if(result)
            {
                product.IsActive=false;
                TempData["Message"]=product.Id.ToString()+" id'li ürün silindi";
                TempData["Alert"]="alert-success";
            }
            else
            {
                product.IsActive=true;
                TempData["Message"]=product.Id.ToString()+" id'li ürün silinemedi!";
                TempData["Alert"]="alert-danger";
            }
            
        }
        return Redirect("/admin/listproducts?search="+productId.ToString());
    }

    //Onay bekleyen başvuru işlemleri ****************************************************

    [HttpGet]
    public async Task<IActionResult> ListCategories()
    {
        var categories = await _categoryService.Admin_GetCategories30Async();
        List<CategoryModel> categoryModels = new List<CategoryModel>(){};
        foreach(var category in categories)
        {
            categoryModels.Add(new CategoryModel(){
                Id=category.Id,
                Name=category.Name,
                Description=category.Description,
                ImageUrl=category.ImageUrl,
                CreatedAt=category.CreatedAt,
                UpdatedAt=category.UpdatedAt,
            });
        }

        return View(categoryModels);
    }
    [HttpPost]
    public async Task<IActionResult> ListCategories([FromForm] string search)
    {
        List<Category> categories;
        if(search!=null)
        {
            categories = await _categoryService.Admin_GetCategoriesBySearchAsync(search);
        }
        else
        {
            categories = await _categoryService.Admin_GetCategories30Async();
        }
        List<CategoryModel> categoryModels = new List<CategoryModel>(){};
        foreach(var category in categories)
        {
            categoryModels.Add(new CategoryModel(){
                Id=category.Id,
                Name=category.Name,
                Description=category.Description,
                ImageUrl=category.ImageUrl,
                CreatedAt=category.CreatedAt,
                UpdatedAt=category.UpdatedAt,
            });
        }

        return View(categoryModels);
    }

    [HttpGet]
    public async Task<IActionResult> AddCategory()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> AddCategory([FromForm] CategoryModel model, [FromForm] IFormFile file)
    {
        
        if(ModelState.IsValid)
        {
            if(file!=null&&file.Length>0)
            {
                var fileExtension = Path.GetExtension(file.FileName);
                if(fileExtension==".jpeg"||fileExtension==".jpg"||fileExtension=="png")
                {
                    var fileName = Guid.NewGuid()+".jpeg";
                    var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","CategoryImages",fileName);
                    using(FileStream stream = new FileStream(path,FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    Category category = new Category(){
                        Name=model.Name,
                        Description=model.Description,
                        ImageUrl=fileName,
                        CreatedAt=DateTime.Now,
                        UpdatedAt=DateTime.Now,
                    };
                    var result = await _categoryService.CreateAsync(category);
                    if(result)
                    {
                        TempData["Message"]="Kategori başarıyla eklendi!";
                        TempData["Alert"]="alert-success";
                        return Redirect("/admin/listcategories");
                    }
                    
                }
                TempData["Message"]="Resim dosyası yükleyin! Resim dosyasının dışındakiler kabul edilmez!";
                TempData["Alert"]="alert-danger";
                ModelState.AddModelError("","Resim dosyası yükleyiniz!");
                return View(model);
            }
        }
        TempData["Message"]="Resim yükleyin!";
        TempData["Alert"]="alert-danger";
        return View(model);
    }
    [HttpGet]
    public async Task<IActionResult> EditCategory(int categoryId)
    {
        var category = await _categoryService.GetCategoryByIdAsync(categoryId);
        ;
        if(category==null)
            return NotFound();
        CategoryModel categoryModel = new CategoryModel(){
            Id=category.Id,
            Name=category.Name,
            Description=category.Description,
            ImageUrl=category.ImageUrl
        };
        ViewBag.CategoryImageUrl=category.ImageUrl;
        return View(categoryModel);
    }

    [HttpPost]
    public async Task<IActionResult> EditCategory([FromForm] int categoryId, [FromForm] CategoryModel model, [FromForm] IFormFile? file)
    {
        var category = await _categoryService.GetCategoryByIdAsync(categoryId);
        if(category==null)
            return NotFound();
        

        if(ModelState.IsValid)
        {
            string? fileName = null;
            if(file!=null&&file.Length>0)
            {
                if(category.ImageUrl!=null)
                {
                    var imgPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","CategoryImages",category.ImageUrl);
                    System.IO.File.Delete(imgPath);
                }

                
                var fileExtension = Path.GetExtension(file.FileName);
                if(fileExtension==".jpeg"||fileExtension==".jpg"||fileExtension=="png")
                {
                    fileName = Guid.NewGuid()+".jpeg";
                    var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","CategoryImages",fileName);
                    using(FileStream stream = new FileStream(path,FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                }
                else
                {
                    TempData["Message"]="Resim dosyası yükleyin! Resim dosyasının dışındakiler kabul edilmez!";
                    TempData["Alert"]="alert-danger";
                    ViewBag.CategoryImageUrl=category.ImageUrl;
                    return View(model);
                }
            }
            else
            {
                if(category.ImageUrl==null)
                {
                    TempData["Message"]="Resim dosyası yükleyin!";
                    TempData["Alert"]="alert-danger";
                    ViewBag.CategoryImageUrl=category.ImageUrl;
                    return View(model);
                }
                fileName=category.ImageUrl;
            }
            category.Name=model.Name;
            category.Description=model.Description;
            category.ImageUrl=fileName;
            category.UpdatedAt=DateTime.Now;

            var result = await _categoryService.UpdateAsync(category);
            if(result)
            {
                TempData["Message"]="Kategori başarıyla eklendi!";
                TempData["Alert"]="alert-success";
                return Redirect("/admin/listcategories");
            }
        }
        ViewBag.CategoryImageUrl=category.ImageUrl;
        return Redirect("/admin/editcategory?categoryId="+categoryId.ToString());
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCategory([FromForm] int categoryId)
    {
        var category = await _categoryService.GetCategoryByIdAsync(categoryId);
        if(category!=null)
        {
            await _productService.Admin_UpdateCategoryIdNullAsync(categoryId);
            var result = await _categoryService.DeleteAsync(category);
            if(result)
            {
                TempData["Message"]="Kategori başarıyla silindi!";
                TempData["Alert"]="alert-success";
                return Redirect("/admin/listcategories");
            }
            TempData["Message"]="Kategori silinemedi!";
            TempData["Alert"]="alert-danger";
        }
        return Redirect("/admin/listcategories");
    }
    

    //Onay bekleyen başvuru işlemleri ****************************************************
    [HttpGet]
    public async Task<IActionResult> ListApplySellers()
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();

        List<ApplySeller> applySellers = await _applySellerService.GetApplySellersAsync(); 
        List<ApplySellerModel> applySellerModels = new List<ApplySellerModel>(){};
        foreach(var applySeller in applySellers)
        {
            applySellerModels.Add(new ApplySellerModel(){
                Id=applySeller.Id,
                UserId=applySeller.UserId,
                ImageUrl=applySeller.ImageUrl,
                RestaurantName=applySeller.RestaurantName,
                FirstName=applySeller.FirstName,
                LastName=applySeller.LastName,
                TCKN=applySeller.TCKN,
                CityId=applySeller.CityId,
                DistrictId=applySeller.DistrictId,
                Address=applySeller.Address,
                PhoneNumber=applySeller.PhoneNumber,
                RestaurantPhoneNumber=applySeller.RestaurantPhoneNumber,
                EmailAddress=applySeller.EmailAddress
            });
        }
        ViewBag.Cities=await _cityService.GetCitiesAsync();
        ViewBag.Districts=await _districtService.GetDistrictsAsync();
        return View(applySellerModels);
    }
    [HttpPost]
    public async Task<IActionResult> ConfirmApplySeller([FromForm] int applySellerId)
    {
        var applySeller = await _applySellerService.GetApplySellerByIdAsync(applySellerId);
        if(applySeller!=null)
        {
            var user = await _userManager.FindByIdAsync(applySeller.UserId);
            if(user!=null)
            {
                Restaurant restaurant = new Restaurant(){
                    UserId=user.Id,
                    Name=applySeller.RestaurantName,
                    ImageUrl=applySeller.ImageUrl,
                    Country=applySeller.Country,
                    CityId=applySeller.CityId,
                    DistrictId=applySeller.DistrictId,
                    Address=applySeller.Address,
                    PhoneNumber=applySeller.RestaurantPhoneNumber,
                    EmailAdress=applySeller.EmailAddress,
                    MinimumOrderPrice=applySeller.MinimumOrderPrice,
                    CreatedAt=DateTime.Now,
                    UpdatedAt=DateTime.Now,
                    IsActive=false
                };
                await _restaurantService.CreateAsync(restaurant);
                var restaurant2 = await _restaurantService.GetRestaurantByUserIdAsync(user.Id);
                if(restaurant2!=null)
                {
                    user.RestaurantId=restaurant2.Id;
                    var result = await _userManager.UpdateAsync(user);
                    if(result.Succeeded)
                    {
                        var result2 = await _userManager.AddToRoleAsync(user,"Seller");
                        if(result2.Succeeded)
                        {
                            TempData["Message"] = applySeller.Id.ToString()+" Nolu başvuru onaylandı!";
                            TempData["Alert"] = "alert-success";
                        }
                    }
                }
            }
            await _applySellerService.DeleteAsync(applySeller);
        }
        return Redirect("/admin/listapplysellers");
    }
    [HttpPost]
    public async Task<IActionResult> RejectApplySeller([FromForm] int applySellerId)
    {
        var applySeller = await _applySellerService.GetApplySellerByIdAsync(applySellerId);
        if(applySeller!=null)
        {
            TempData["Message"] = applySeller.Id.ToString()+" Nolu başvuru reddedildi!";
            TempData["Alert"] = "alert-danger";
            await _applySellerService.DeleteAsync(applySeller);
        }
        return Redirect("/admin/listapplysellers");
    }

    [HttpGet]
    public async Task<IActionResult> ListOrders()
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();
        
        var activeOrders = await _orderService.Admin_GetOrders30Async();
        List<OrderModel> orderModels = new List<OrderModel>(){};
        foreach(var order in activeOrders)
        {
            var city=await _cityService.GetCityByIdAsync(order.CityId);
            var district=await _districtService.GetDistrictByIdAsync(order.DistrictId);
            orderModels.Add(new OrderModel(){
                Id=order.Id,
                RestaurantId=order.RestaurantId,
                RestaurantName=order.RestaurantName,
                Note=order.Note,
                ProductTypesNumber=order.ProductTypesNumber,
                TotalPrice=order.TotalPrice,
                CreatedAt=order.CreatedAt,
                DeliveryDate=order.DeliveryDate,
                Status=order.Status,
                Country=order.Country,
                CityId=order.CityId,
                CityName=city.Name,
                DistrictId=order.DistrictId,
                DistrictName=district.Name,
                Address=order.Address,
                FullName=order.FullName,
                UserId=order.UserId,
                PhoneNumber=order.PhoneNumber,
                OrderItemModels=order.OrderItems?.Select(orderItem=>new OrderItemModel(){
                    Id=orderItem.Id,
                    OrderId=orderItem.OrderId,
                    ProductId=orderItem.ProductId,
                    ProductModel=new ProductModel(){
                        Id=orderItem.Product?.Id ?? -1,
                        ImageUrl=orderItem.Product?.ImageUrl,
                        Name=orderItem.Product?.Name,
                        Content=orderItem.Product?.Content
                    },
                    Amount=orderItem.Amount,
                    Price=orderItem.Price,
                    TotalPrice=orderItem.TotalPrice,
                }).ToList(),
                
            });
        }
        return View(orderModels);
    }
    [HttpPost]
    public async Task<IActionResult> ListOrders([FromForm] int? search)
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();
        List<Order> activeOrders;
        if(search!=null)
        {
            activeOrders = await _orderService.Admin_GetOrdersByOrderIdAsync(search);
        }
        else
        {
            activeOrders = await _orderService.Admin_GetOrders30Async();
        }
        
        List<OrderModel> orderModels = new List<OrderModel>(){};
        foreach(var order in activeOrders)
        {
            var city=await _cityService.GetCityByIdAsync(order.CityId);
            var district=await _districtService.GetDistrictByIdAsync(order.DistrictId);
            orderModels.Add(new OrderModel(){
                Id=order.Id,
                RestaurantId=order.RestaurantId,
                RestaurantName=order.RestaurantName,
                Note=order.Note,
                ProductTypesNumber=order.ProductTypesNumber,
                TotalPrice=order.TotalPrice,
                CreatedAt=order.CreatedAt,
                DeliveryDate=order.DeliveryDate,
                Status=order.Status,
                Country=order.Country,
                CityId=order.CityId,
                CityName=city.Name,
                DistrictId=order.DistrictId,
                DistrictName=district.Name,
                Address=order.Address,
                FullName=order.FullName,
                UserId=order.UserId,
                PhoneNumber=order.PhoneNumber,
                OrderItemModels=order.OrderItems?.Select(orderItem=>new OrderItemModel(){
                    Id=orderItem.Id,
                    OrderId=orderItem.OrderId,
                    ProductId=orderItem.ProductId,
                    ProductModel=new ProductModel(){
                        Id=orderItem.Product?.Id ?? -1,
                        ImageUrl=orderItem.Product?.ImageUrl,
                        Name=orderItem.Product?.Name,
                        Content=orderItem.Product?.Content
                    },
                    Amount=orderItem.Amount,
                    Price=orderItem.Price,
                    TotalPrice=orderItem.TotalPrice,
                }).ToList(),
                
            });
        }
        return View(orderModels);
    }

    
    [HttpPost]
    public async Task<IActionResult> CancelOrder([FromForm] int orderId)
    {
        var order = await _orderService.GetOrderByIdAsync(orderId);
        if(order!=null)
        {
            order.Status=-1;
            var result = await _orderService.UpdateAsync(order);
            if(result)
            {
                TempData["Message"] = order.Id.ToString()+" Nolu sipariş iptal edildi!";
                TempData["Alert"] = "alert-danger";
                return Redirect("/admin/listorders");
            }
        }
        return Redirect("/admin/listorders");
    }
    
    [HttpGet]
    public async Task<IActionResult> OrderManager()
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();

        OrderManagerViewModel orderManagerViewModel = new OrderManagerViewModel();
        orderManagerViewModel.TotalOrdersCount=await _orderService.Admin_TotalOrdersCountAsync();
        orderManagerViewModel.TotalCompletedOrdersCount=await _orderService.Admin_TotalCompletedOrdersCountAsync();
        orderManagerViewModel.TotalCanceledOrdersCount=await _orderService.Admin_TotalCanceledOrdersCountAsync();
        orderManagerViewModel.ActiveOrdersCount=await _orderService.Admin_TotalActiveOrdersCountAsync();
        orderManagerViewModel.SetOutOrdersCount=await _orderService.Admin_TotalSetOutOrdersCountAsync();
        var orderByCategory=await _orderService.Admin_ShortOrdersByCategoryAsync();
        var maxEntry = orderByCategory.OrderByDescending(x => x.Value).FirstOrDefault();
        var category = await _categoryService.GetCategoryByIdAsync(maxEntry.Key);
        var TopSellingCategoryList=orderByCategory.OrderByDescending(x => x.Value).ToList();
        if(category==null)
        {
            orderManagerViewModel.TopSellingCategory=null;
            orderManagerViewModel.TopSellingCategoryCount = 0;
            return View(orderManagerViewModel);
        }
        CategoryModel categoryModel = new CategoryModel(){
            Id=category.Id,
            Name=category.Name,
            ImageUrl=category.ImageUrl
        };
        orderManagerViewModel.TopSellingCategory=categoryModel;
        orderManagerViewModel.TopSellingCategoryCount = maxEntry.Value;
        return View(orderManagerViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> OrderManager([FromForm] int? orderId)
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();

        OrderManagerViewModel orderManagerViewModel = new OrderManagerViewModel();
        orderManagerViewModel.TotalOrdersCount=await _orderService.Admin_TotalOrdersCountAsync();
        orderManagerViewModel.TotalCompletedOrdersCount=await _orderService.Admin_TotalCompletedOrdersCountAsync();
        orderManagerViewModel.TotalCanceledOrdersCount=await _orderService.Admin_TotalCanceledOrdersCountAsync();
        orderManagerViewModel.ActiveOrdersCount=await _orderService.Admin_TotalActiveOrdersCountAsync();
        orderManagerViewModel.SetOutOrdersCount=await _orderService.Admin_TotalSetOutOrdersCountAsync();
        var orderByCategory=await _orderService.Admin_ShortOrdersByCategoryAsync();
        var maxEntry = orderByCategory.OrderByDescending(x => x.Value).FirstOrDefault();
        var category = await _categoryService.GetCategoryByIdAsync(maxEntry.Key);
        var TopSellingCategoryList=orderByCategory.OrderByDescending(x => x.Value).ToList();
        if(category==null)
        {
            orderManagerViewModel.TopSellingCategory=null;
            orderManagerViewModel.TopSellingCategoryCount = 0;
            return View(orderManagerViewModel);
        }
        CategoryModel categoryModel = new CategoryModel(){
            Id=category.Id,
            Name=category.Name,
            ImageUrl=category.ImageUrl
        };
        orderManagerViewModel.TopSellingCategory=categoryModel;
        orderManagerViewModel.TopSellingCategoryCount = maxEntry.Value;

        if(orderId!=null)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if(order==null)
            {
                TempData["Message"] = "Girilen sipariş numarasına ait sipariş bulunamadı!";
                TempData["Alert"] = "alert-danger";
                return View(orderManagerViewModel);
            }
            var city=await _cityService.GetCityByIdAsync(order.CityId);
            var district=await _districtService.GetDistrictByIdAsync(order.DistrictId);
            orderManagerViewModel.OrderModel=new OrderModel(){
                Id=order.Id,
                UserId=order.UserId,
                RestaurantId=order.RestaurantId,
                RestaurantName=order.RestaurantName,
                Status=order.Status,
                CreatedAt=order.CreatedAt,
                DeliveryDate=order.DeliveryDate,
                TotalPrice=order.TotalPrice,
                FullName=order.FullName,
                PhoneNumber=order.PhoneNumber,
                CityId=orderId,
                CityName=city.Name,
                DistrictId=order.DistrictId,
                DistrictName=district.Name,
                Address=order.Address,
                OrderItemModels=order.OrderItems?.Select(orderItem=>new OrderItemModel(){
                    Id=orderItem.Id,
                    ProductId=orderItem.Id,
                    ProductModel=new ProductModel(){
                        Id=orderItem.Product.Id,
                        Name=orderItem.Product.Name,
                        CategoryId=orderItem.Product.CategoryId,
                        ImageUrl=orderItem.Product.ImageUrl
                    }
                }).ToList()
            };
        }
        else
        {
            TempData["Message"] = "Girilen sipariş numarasına ait sipariş bulunamadı!";
            TempData["Alert"] = "alert-danger";
        }

        return View(orderManagerViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> WaitApprovedProducts()
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();

        var notApprovesProducts = await _productService.Admin_GetNotApprovedProducts30Async();
        List<ProductModel> productModels = new List<ProductModel>();
        foreach(var p in notApprovesProducts)
        {
            productModels.Add(new ProductModel(){
                Id=p.Id,
                Name=p.Name,
                CategoryId=p.CategoryId,
                Description=p.Description,
                Content=p.Content,
                ImageUrl=p.ImageUrl,
                Calorie=p.Calorie,
                PreviousPrice=p.PreviousPrice,
                Price=p.Price,
                IsActive=p.IsActive,
                IsApproved=p.IsApproved
            });
        }
        ViewBag.Categories=await _categoryService.GetCategoriesAsync();
        return View(productModels);

    }

    [HttpPost]
    public async Task<IActionResult> WaitApprovedProducts([FromForm] string? search)
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();
        List<Product> notApprovedProducts;
        if(search!=null)
        {
            notApprovedProducts = await _productService.Admin_GetNotApprovedProductsBySearchAsync(search);
            if(notApprovedProducts.Count<=0)
            {
                TempData["Message"] = "Girilen bilgiye göre onay bekleyen ürün bulunamadı";
                TempData["Alert"] = "alert-warning";
            }
        }
        else
        {
            notApprovedProducts = await _productService.Admin_GetNotApprovedProducts30Async();
        }
        
        List<ProductModel> productModels = new List<ProductModel>();
        foreach(var p in notApprovedProducts)
        {
            productModels.Add(new ProductModel(){
                Id=p.Id,
                Name=p.Name,
                CategoryId=p.CategoryId,
                Description=p.Description,
                Content=p.Content,
                ImageUrl=p.ImageUrl,
                Calorie=p.Calorie,
                PreviousPrice=p.PreviousPrice,
                Price=p.Price,
                IsActive=p.IsActive,
                IsApproved=p.IsApproved
            });
        }
        ViewBag.Categories=await _categoryService.GetCategoriesAsync();
        return View(productModels);

    }

    [HttpPost]
    public async Task<IActionResult> ConfirmProduct([FromForm] int productId)
    {
        var product = await _productService.GetProductByIdAsync(productId);
        if(product!=null)
        {
            product.IsApproved=true;
            var result = await _productService.UpdateAsync(product);
            if(result)
            {
                TempData["Message"] = product.Id.ToString()+" Idli ürün onaylandı!";
                TempData["Alert"] = "alert-success";
                return Redirect("/admin/WaitApprovedProducts");
            }
            else
            {
                TempData["Message"] = product.Id.ToString()+" Idli ürün Onaylanamadı!";
                TempData["Alert"] = "alert-danger";
            }
        }
        return Redirect("/admin/WaitApprovedProducts");
    }

    [HttpPost]
    public async Task<IActionResult> RejectProduct([FromForm] int productId)
    {
        var product = await _productService.GetProductByIdAsync(productId);
        if(product!=null)
        {
            if(product.ImageUrl!=null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","ProductImages",product.ImageUrl);
                if(System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            var result = await _productService.DeleteAsync(product);
            if(result)
            {
                TempData["Message"] = product.Id.ToString()+" Idli ürün reddedildi!";
                TempData["Alert"] = "alert-danger";
                return Redirect("/admin/WaitApprovedProducts");
            }
            else
            {
                TempData["Message"] = "Hata oluştu!";
                TempData["Alert"] = "alert-warning";
            }
        }
        return Redirect("/admin/WaitApprovedProducts");
    }

    [HttpGet]
    public async Task<IActionResult> WaitApprovedRestaurants()
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();

        var notApprovedRestaurants = await _restaurantService.Admin_GetNotApprovedRestaurants30Async();
        List<RestaurantModel> restaurantModels = new List<RestaurantModel>();
        foreach(var r in notApprovedRestaurants)
        {
            restaurantModels.Add(new RestaurantModel(){
                Id=r.Id,
                UserId=r.UserId,
                Name=r.Name,
                ImageUrl=r.ImageUrl,
                Description=r.Description,
                Country=r.Country,
                CityId=r.CityId,
                DistrictId=r.DistrictId,
                Address=r.Address,
                PhoneNumber=r.PhoneNumber,
                EmailAdress=r.EmailAdress,
                OpeningTime=r.OpeningTime,
                ClosingTime=r.ClosingTime,
                DeliveryTime=r.DeliveryTime,
                MinimumOrderPrice=r.MinimumOrderPrice,
                Rating=r.Rating,
                CreatedAt=r.CreatedAt,
                UpdatedAt=r.UpdatedAt,

            });
        }
        ViewBag.Cities=await _cityService.GetCitiesAsync();
        ViewBag.Districts=await _districtService.GetDistrictsAsync();
        return View(restaurantModels);
    }

    [HttpPost]
    public async Task<IActionResult> WaitApprovedRestaurants([FromForm] string? search)
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User)??"");
        if(user==null)
            return NotFound();
        List<Restaurant> notApprovedRestaurants;
        if(search!=null)
        {
            notApprovedRestaurants = await _restaurantService.Admin_GetNotApprovedRestaurantsBySearchAsync(search);
            if(notApprovedRestaurants.Count<=0)
            {
                TempData["Message"] = "Girilen bilgiye göre güncelleme onayı bekleyen restorant bulunamadı";
                TempData["Alert"] = "alert-warning";
            }
        }
        else
        {
            notApprovedRestaurants = await _restaurantService.Admin_GetNotApprovedRestaurants30Async();
        }
        List<RestaurantModel> restaurantModels = new List<RestaurantModel>();
        foreach(var r in notApprovedRestaurants)
        {
            restaurantModels.Add(new RestaurantModel(){
                Id=r.Id,
                UserId=r.UserId,
                Name=r.Name,
                ImageUrl=r.ImageUrl,
                Description=r.Description,
                Country=r.Country,
                CityId=r.CityId,
                DistrictId=r.DistrictId,
                Address=r.Address,
                PhoneNumber=r.PhoneNumber,
                EmailAdress=r.EmailAdress,
                OpeningTime=r.OpeningTime,
                ClosingTime=r.ClosingTime,
                DeliveryTime=r.DeliveryTime,
                MinimumOrderPrice=r.MinimumOrderPrice,
                Rating=r.Rating,
                CreatedAt=r.CreatedAt,
                UpdatedAt=r.UpdatedAt,

            });
        }
        ViewBag.Cities=await _cityService.GetCitiesAsync();
        ViewBag.Districts=await _districtService.GetDistrictsAsync();
        return View(restaurantModels);
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmRestaurant([FromForm] int restaurantId)
    {
        var restaurant = await _restaurantService.GetRestaurantByIdAsync(restaurantId);
        if(restaurant!=null)
        {
            restaurant.IsApproved=true;
            var result = await _restaurantService.UpdateAsync(restaurant);
            if(result)
            {
                TempData["Message"] = restaurant.Id.ToString()+" Idli restorant onaylandı!";
                TempData["Alert"] = "alert-success";
                return Redirect("/admin/WaitApprovedProducts");
            }
            else
            {
                TempData["Message"] = restaurant.Id.ToString()+" Idli restorant Onaylanamadı!";
                TempData["Alert"] = "alert-danger";
            }
        }
        return Redirect("/admin/WaitApprovedRestaurants");
    }

    [HttpPost]
    public async Task<IActionResult> RejectRestaurant([FromForm] int restaurantId)
    {
        var restaurant = await _restaurantService.GetRestaurantByIdAsync(restaurantId);
        if(restaurant!=null)
        {
            if(restaurant.ImageUrl!=null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","RestaurantImages",restaurant.ImageUrl);
                if(System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            restaurant.ImageUrl=null;
            restaurant.Description=null;
            restaurant.IsApproved=true;
            var result = await _restaurantService.UpdateAsync(restaurant);
            if(result)
            {
                TempData["Message"] = restaurant.Id.ToString()+" Idli restorant reddedildi!";
                TempData["Alert"] = "alert-success";
                return Redirect("/admin/WaitApprovedProducts");
            }
            else
            {
                TempData["Message"] =" Hata oluştu!";
                TempData["Alert"] = "alert-danger";
            }
        }
        return Redirect("/admin/WaitApprovedRestaurants");
    }

    [HttpGet]
    public async Task<IActionResult> WaitApprovedComments()
    {
        var notApprovedComments = await _commentService.Admin_GetComments30Async();
        List<CommentModel> commentModels = new List<CommentModel>();
        foreach(var c in notApprovedComments)
        {
            commentModels.Add(new CommentModel(){
                Id=c.Id,
                OrderId=c.OrderId,
                RestaurantId=c.RestaurantId,
                UserName=c.UserName??"user",
                Description=c.Description??"",
                CreatedAt=c.CreatedAt
            });
        }


        return View(commentModels);
    }
    [HttpPost]
    public async Task<IActionResult> ConfirmComment([FromForm] int commentId)
    {
        var comment = await _commentService.GetCommentByIdAsync(commentId);
        if(comment!=null)
        {
            comment.IsApproved=true;
            var result = await _commentService.UpdateAsync(comment);
            if(result)
            {
                TempData["Message"] = "Yorum onaylandı!";
                TempData["Alert"] = "alert-success";
                
            }
        }
        return Redirect("/admin/WaitApprovedComments");
    }
    [HttpPost]
    public async Task<IActionResult> RejectComment([FromForm] int commentId)
    {
        var comment = await _commentService.GetCommentByIdAsync(commentId);
        if(comment!=null)
        {
            var result = await _commentService.DeleteAsync(comment);
            if(result)
            {
                TempData["Message"] = "Yorum Red edildi!";
                TempData["Alert"] = "alert-danger";
                
            }
        }
        return Redirect("/admin/WaitApprovedComments");
    }

    [HttpGet]
    public async Task<IActionResult> ListFeedbacks()
    {
        var feedbacks = await _feedbackService.GetWaitFeedbacksAsync();
        List<FeedbackModel> feedbackModels = new List<FeedbackModel>();
        foreach(var f in feedbacks)
        {
            feedbackModels.Add(new FeedbackModel(){
                Id=f.Id,
                UserName=f.UserName,
                Subject=f.Subject,
                Description=f.Description,
                CreatedAt=f.CreatedAt,
                Status=f.Status
            });
        }
        return View(feedbackModels);
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmFeedback([FromForm] int feedbackId)
    {
        var feedback = await _feedbackService.GetFeedbackByIdAsync(feedbackId);
        if(feedback!=null)
        {
            feedback.Status=true;
            var result = await _feedbackService.UpdateAsync(feedback);
            if(result)
            {
                TempData["Message"] = feedback.Id.ToString()+" id'li geri bildirim incelendi olarak işaretlendi.";
                TempData["Alert"] = "alert-success";
                return Redirect("/admin/listfeedbacks");
            }
        }
        return Redirect("/admin/listfeedbacks");
    }

    [HttpGet]
    public async Task<IActionResult> ListComplaints()
    {
        var complaints = await _complaintService.ActiveComplaints30Async();
        List<ComplaintModel> complaintModels = new List<ComplaintModel>();
        foreach(var c in complaints)
        {
            complaintModels.Add(new ComplaintModel(){
                Id=c.Id,
                UserName=c.UserName,
                Subject=c.Subject,
                Description=c.Description,
                CreatedAt=c.CreatedAt,
                Status=c.Status,
                OrderModel=new OrderModel(){
                    Id=c.Order.Id,
                    RestaurantId=c.Order.RestaurantId,
                    RestaurantName=c.Order.RestaurantName,
                    CreatedAt=c.Order.CreatedAt,
                    DeliveryDate=c.Order.DeliveryDate,
                }
            });
        }
        return View(complaintModels);
    }


    [HttpPost]
    public async Task<IActionResult> ConfirmCopmlaint([FromForm] int complaintId)
    {
        var complaint = await _complaintService.GetComplaintByIdAsync(complaintId);
        if(complaint!=null)
        {
            complaint.Status=true;
            var result = await _complaintService.UpdateAsync(complaint);
            if(result)
            {
                TempData["Message"] = complaint.Id.ToString()+" id'li şikayet çözüldü olarak işaretlendi.";
                TempData["Alert"] = "alert-success";
                return Redirect("/admin/listcomplaints");
            }
        }
        return Redirect("/admin/listcomplaints");
    }
}