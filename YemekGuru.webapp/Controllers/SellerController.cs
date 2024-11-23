using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using YemekGuru.business.Abstract;
using YemekGuru.entity;
using YemekGuru.webapp.Identity;
using YemekGuru.webapp.Models;


namespace YemekGuru.webapp.Controllers;

[Authorize(Roles ="Seller")]
public class SellerController:Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IRestaurantService _restaurantService;
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly ICityService _cityService;
    private readonly IDistrictService _districtService;
    private readonly ICartService _cartService;
    private readonly ICartItemService _cartItemService;
    private readonly IOrderService _orderService;
    private readonly ICommentService _commentService;

    public SellerController(UserManager<User> userManager, SignInManager<User> signInManager, IRestaurantService restaurantService, IProductService productService, ICategoryService categoryService, ICityService cityService, IDistrictService districtService, ICartService cartService, ICartItemService cartItemService, IOrderService orderService, ICommentService commentService)
    {
        _userManager=userManager;
        _signInManager=signInManager;
        _restaurantService=restaurantService;
        _productService=productService;
        _categoryService=categoryService;
        _cityService=cityService;
        _districtService=districtService;
        _cartService=cartService;
        _cartItemService=cartItemService;
        _orderService=orderService;
        _commentService=commentService;
    }
    
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();
        if(user.RestaurantId==null)
            return NotFound();
        
        var restaurant = await _restaurantService.GetRestaurantByUserIdAsync(user.Id);
        if(restaurant==null)
            return NotFound();
        
        RestaurantModel restaurantModel = new RestaurantModel{
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
                OpeningTime=restaurant.OpeningTime,
                ClosingTime=restaurant.ClosingTime,
                DeliveryTime=restaurant.DeliveryTime,
                MinimumOrderPrice=restaurant.MinimumOrderPrice,
                Rating=restaurant.Rating,
                CreatedAt=restaurant.CreatedAt,
                UpdatedAt=restaurant.UpdatedAt,
                IsActive=restaurant.IsActive,
                IsApproved=restaurant.IsApproved
            };
        var products = await _productService.GetTotalProductsByRestaurantIdAsync(restaurant.Id);
        SellerIndexViewModel sellerIndexViewModel = new SellerIndexViewModel();
        sellerIndexViewModel.RestaurantModel=restaurantModel;
        sellerIndexViewModel.ProductsCount=products.Count();
        sellerIndexViewModel.ActiveProductsCount=products.Where(i=>i.IsActive==true).Count();
        sellerIndexViewModel.TotalActiveOrdersCount=await _orderService.GetTotalActiveOrderByRestaurantIdAsync(restaurant.Id);
        sellerIndexViewModel.TotalSetOutOrdersCount=await _orderService.GetTotalSetOutOrderByRestaurantIdAsync(restaurant.Id);
        sellerIndexViewModel.TotalCanceledOrdersOrdersCount=await _orderService.GetTotalCanceledOrderByRestaurantIdAsync(restaurant.Id);
        sellerIndexViewModel.TotalComplatedOrdersCount=await _orderService.GetTotalComplatedOrderByRestaurantIdAsync(restaurant.Id);
        sellerIndexViewModel.TotalOrdersCount=await _orderService.GetTotalOrderByRestaurantIdAsync(restaurant.Id);
        sellerIndexViewModel.CommentsCount=await _commentService.TotalCommentsCountByRestaurantIdAsync(restaurant.Id);
        
        return View(sellerIndexViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> RestaurantOpenClose([FromForm] int restaurantId)
    {
        if(restaurantId==null)
            return NotFound();
        
        var restaurant = await _restaurantService.GetRestaurantByIdAsync(restaurantId);
        if(restaurant.IsApproved==false)
        {
            TempData["MessageWarning"]="Restorantınız incelemeye alındı!";
            TempData["Alert"] = "alert-warning";
            return Redirect("/seller/index");
        }
        if(restaurant!=null)
        {   
            if(restaurant.IsActive==true)
            {
                restaurant.IsActive=false;
                TempData["Message"]="Restorant Kapatıldı!";
                TempData["Alert"] = "alert-danger";
            }
            else
            {
                if(restaurant.OpeningTime==null||restaurant.ClosingTime==null||restaurant.MinimumOrderPrice==null||restaurant.Description==null)
                {
                    TempData["MessageWarning"]="Eksik bilgileri doldurunuz! Bilgiler eksik olduğundan restorantı açamazsınız!";
                    TempData["Alert"] = "alert-warning";
                    return Redirect("/seller/index");
                }
                restaurant.IsActive=true;
                TempData["Message"]="restorant Açıldı.";
                TempData["Alert"] = "alert-success";
            }
            var result = await _restaurantService.UpdateAsync(restaurant);
            if(!result)
                return NotFound();
        }
        else
        {
            TempData["Message"]="Hata : Restorant Kapatılamadı!";
            TempData["Alert"] = "alert-warning";
        }
        return Redirect("/seller/index");
    }

    
    public async Task<IActionResult> ListProducts()
    {
        ViewBag.Categories=await _categoryService.GetCategoriesAsync();
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));

        if(user!=null)
        {
            var restaurant = await _restaurantService.GetRestaurantByUserIdAsync(user.Id);

            if(restaurant!=null)
            {
                var products = await _productService.GetSellerProductsByRestaurantIdAsync(restaurant.Id);

                List<ProductModel> productModels = new List<ProductModel>(){};
                foreach(var p in products)
                {
                    productModels.Add(new ProductModel(){
                        Id=p.Id,
                        RestaurantId=p.RestaurantId,
                        Name=p.Name,
                        CategoryId=p.CategoryId,
                        Description=p.Description,
                        Content=p.Content,
                        Calorie=p.Calorie,
                        ImageUrl=p.ImageUrl,
                        PreviousPrice=p.PreviousPrice,
                        Price=p.Price,
                        IsActive=p.IsActive,
                        CreatedTime=p.CreatedTime,
                        UpdateTime=p.UpdateTime
                    });
                }
                ViewBag.RestaurantIsActice=restaurant.IsActive;
                return View(productModels);
                
            }
        }
        TempData["Message"]="Hata : Yemekler sekmesine giriş yapılamıyor daha sonra tekrar deneyin!";
        TempData["Alert"] = "alert-warning";
        
        return Redirect("/seller/index");
    }

    [HttpGet]
    public async Task<IActionResult> AddProduct()
    {
        ViewBag.Categories=await _categoryService.GetCategoriesAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromForm] AddProductViewModel model, [FromForm] IFormFile? file)
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();

        var restaurant = await _restaurantService.GetRestaurantByUserIdAsync(user.Id);
        if(restaurant==null)
            return NotFound();
        
        if(ModelState.IsValid)
        {
            Product product = new Product(){
                RestaurantId=restaurant.Id,
                Name=model.Name,
                CategoryId=model.CategoryId,
                Description=model.Description,
                Content=model.Content,
                Calorie=model.Calorie,
                Price=model.Price,
                IsActive=false,
                IsApproved=false
            };
            if(file!=null&&file.Length>0)
            {
                var fileName=Guid.NewGuid()+".jpeg";
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","ProductImages",fileName);

                using(FileStream stream =new FileStream(path,FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                product.ImageUrl=fileName;
            }
            else
            {
                product.ImageUrl=null;
            }

            var result = await _productService.CreateAsync(product);
            TempData["Message"]="Yemek başarıyla eklendi.";
            TempData["Alert"] = "alert-success";
            return Redirect("/seller/listproducts");
            
        }
        
        ViewBag.Categories=await _categoryService.GetCategoriesAsync();
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> EditProduct(int productId)
    {
        
        var product = await _productService.GetProductByIdAsync(productId);
        if(product==null)
            return NotFound();
        
        EditProductViewModel editProductViewModel = new EditProductViewModel(){
            Id=product.Id,
            Name=product.Name,
            ImageUrl=product.ImageUrl,
            Description=product.Description,
            Content=product.Content,
            CategoryId=product.CategoryId,
            Calorie=product.Calorie,
            Price=product.Price,
            IsActive=product.IsActive,
        };  

        ViewBag.Categories=await _categoryService.GetCategoriesAsync();

        return View(editProductViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> EditProduct([FromForm] EditProductViewModel model, [FromForm] IFormFile? file)
    {
        var product = await _productService.GetProductByIdAsync(model.Id);
        if(product==null)
            return NotFound();
        if(ModelState.IsValid)
        {
            product.Name=model.Name;
            product.Description=model.Description;
            product.Content=model.Content;
            product.CategoryId=model.CategoryId;
            product.Calorie=model.Calorie;
            product.IsActive=false;
            product.IsApproved=false;
            if(product.Price==model.Price)
            {
                product.Price=model.Price;
            }
            else{
                product.PreviousPrice=product.Price;
                product.Price=model.Price;
            }


            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductImages", product.ImageUrl);

                // Dosyanın var olduğunu kontrol et
                if (System.IO.File.Exists(imagePath))
                {
                    // Dosyayı sil
                    System.IO.File.Delete(imagePath);
                }
            }
            if(file!=null&&file.Length>0)
            {
                var fileName=Guid.NewGuid()+".jpeg";
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","ProductImages",fileName);

                using(FileStream stream =new FileStream(path,FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                product.ImageUrl=fileName;
            }
            else
            {
                product.ImageUrl=null;
            }
            var result = await _productService.UpdateAsync(product);
            TempData["Message"]=product.Id.ToString()+" Nolu yemek başarıyla güncellendi.";
            TempData["Alert"] = "alert-success";
            return Redirect("/seller/listproducts");

        }
            

        ViewBag.Categories=await _categoryService.GetCategoriesAsync();

        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteProduct([FromForm] int productId)
    {
        var product = await _productService.GetProductByIdAsync(productId);
        if(product==null)
            return NotFound();
        
        if(product.ImageUrl!=null)
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","ProductImages",product.ImageUrl);
            System.IO.File.Delete(imagePath);
        }
        var result = await _productService.DeleteAsync(product);
        TempData["Message"]=product.Id.ToString()+" Nolu yemek başarıyla silindi.";
        TempData["Alert"] = "alert-success";
        return Redirect("/seller/listproducts");
    }
    [HttpPost]
    public async Task<IActionResult> IsActiveProduct([FromForm] int productId)
    {
        var product = await _productService.GetProductByIdAsync(productId);
        if(product==null)
            return NotFound();
        
        if(product.IsApproved==false)
        {
            TempData["Message"]=product.Id.ToString()+" Nolu ürün Yemek Guru tarafından onaylanması gerekiyor! Şuan incelenme aşamasında.";
            TempData["Alert"] = "alert-warning";
            return Redirect("/seller/listproducts");
        }

        if(product.IsActive==true)
        {
            product.IsActive=false;
            var cartItems = await _cartItemService.GetCartItemsByProductIdAsync(product.Id);
            if(cartItems.Count>0)
            {
                foreach(var cartItem in cartItems)
                {
                    Console.WriteLine(cartItem.ProductId);
                    await _cartItemService.DeleteAsync(cartItem);
                }
            }
        }
        else
        {
            product.IsActive=true;
        }
        var result = await _productService.UpdateAsync(product);
        TempData["Message"]=product.Id.ToString()+" Nolu yemek başarıyla yayına alındı.";
        TempData["Alert"] = "alert-success";
        return Redirect("/seller/listproducts");
    }

    [HttpGet]
    public async Task<IActionResult> RestaurantDetail()
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();
        
        var restaurant = await _restaurantService.GetRestaurantByUserIdAsync(user.Id);

        if(restaurant==null)
            return NotFound();

        RestaurantModel RestaurantModel = new RestaurantModel(){
            Id=restaurant.Id,
            UserId=restaurant.UserId,
            Name=restaurant.Name,
            Description=restaurant.Description,
            CityId=restaurant.CityId,
            DistrictId=restaurant.DistrictId,
            IsActive=restaurant.IsActive,
            ImageUrl=restaurant.ImageUrl,
            Address=restaurant.Address,
            PhoneNumber=restaurant.PhoneNumber,
            EmailAdress=restaurant.EmailAdress,
            OpeningTime=restaurant.OpeningTime,
            ClosingTime=restaurant.ClosingTime,
            DeliveryTime=restaurant.DeliveryTime,
            MinimumOrderPrice=restaurant.MinimumOrderPrice,
            Rating=restaurant.Rating,
            CreatedAt=restaurant.CreatedAt
        };

        ViewBag.City=await _cityService.GetCityByIdAsync(restaurant.CityId);
        ViewBag.District=await _districtService.GetDistrictByIdAsync(restaurant.DistrictId);
        return View(RestaurantModel);
    }
    [HttpGet]
    public async Task<IActionResult> EditRestaurant()
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();
        
        var restaurant = await _restaurantService.GetRestaurantByUserIdAsync(user.Id);

        if(restaurant==null)
            return NotFound();

        EditRestaurantViewModel editRestaurantViewModel = new EditRestaurantViewModel(){
            Name=restaurant.Name,
            Description=restaurant.Description,
            CityId=restaurant.CityId,
            DistrictId=restaurant.DistrictId,
            ImageUrl=restaurant.ImageUrl,
            Address=restaurant.Address,
            PhoneNumber=restaurant.PhoneNumber,
            EmailAdress=restaurant.EmailAdress,
            OpeningTime=restaurant.OpeningTime,
            ClosingTime=restaurant.ClosingTime,
            DeliveryTime=Convert.ToInt32(restaurant.DeliveryTime),
            MinimumOrderPrice=restaurant.MinimumOrderPrice,
        };

        ViewBag.Cities=await _cityService.GetCitiesAsync();
        ViewBag.Districts=await _districtService.GetDistrictsAsync();
        return View(editRestaurantViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> EditRestaurant([FromForm] EditRestaurantViewModel model, [FromForm] IFormFile? file)
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();

        var restaurant = await _restaurantService.GetRestaurantByUserIdAsync(user.Id);
        if(restaurant==null)
            return NotFound();
        
        model.ImageUrl=restaurant.ImageUrl;
        if(ModelState.IsValid)
        {
            restaurant.Name=model.Name;
            restaurant.Description=model.Description;
            restaurant.EmailAdress=model.EmailAdress;
            restaurant.PhoneNumber=model.PhoneNumber;
            restaurant.CityId=model.CityId;
            restaurant.DistrictId=model.DistrictId;
            restaurant.Address=model.Address;
            restaurant.DeliveryTime=model.DeliveryTime.ToString();
            restaurant.OpeningTime=model.OpeningTime;
            restaurant.ClosingTime=model.ClosingTime;
            restaurant.MinimumOrderPrice=model.MinimumOrderPrice;
            restaurant.IsActive=false;
            restaurant.IsApproved=false;
            
            if(restaurant.ImageUrl!=null)
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","RestaurantImages",restaurant.ImageUrl);
                System.IO.File.Delete(imagePath);
            }

            if(file!=null&&file.Length>0)
            {
                var fileName = Guid.NewGuid()+".jpeg";
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","RestaurantImages",fileName);
                using(FileStream stream = new FileStream(path,FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                restaurant.ImageUrl=fileName;
            }
            else
            {
                restaurant.ImageUrl=null;
            }
            var result = await _restaurantService.UpdateAsync(restaurant);
            if(result)
            {
                user.Email=model.EmailAdress;
                var result2 = await _userManager.UpdateAsync(user);
                if(result2.Succeeded)
                {
                    TempData["Message"]= "Bilgileriniz güncellendi.";
                    TempData["Alert"] = "alert-success";
                    return Redirect("/seller/restaurantdetail");
                }
            }
        }
        ViewBag.Cities=await _cityService.GetCitiesAsync();
        ViewBag.Districts=await _districtService.GetDistrictsAsync();
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ActiveOrders()
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();
        
        var restaurant = await _restaurantService.GetRestaurantByUserIdAsync(user.Id);
        var activeOrders = await _orderService.GetActiveOrderByRestaurantIdAsync(restaurant.Id);
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
    public async Task<IActionResult> SetOutOrder([FromForm] int orderId)
    {
        var order = await _orderService.GetOrderByIdAsync(orderId);
        if(order!=null)
        {
            order.Status=2;
            var result = await _orderService.UpdateAsync(order);
            if(result)
            {
                TempData["Message"]= order.Id.ToString()+" Nolu siparişi dağıtıma çıkardınız. ";
                TempData["Alert"] = "alert-success";
                return Redirect("/seller/activeorders");
            }
            else
            {
                TempData["Message"]= order.Id.ToString()+" Nolu sipariş dağıtıma çıkarılamadı! ";
                TempData["Alert"] = "alert-danger";
            }
        }

        return Redirect("/seller/activeorders");
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
                TempData["Message"]= order.Id.ToString()+" Nolu sipariş iptal edildi ";
                TempData["Alert"] = "alert-danger";
                return Redirect("/seller/activeorders");
            }
            else
            {
                TempData["Message"]= order.Id.ToString()+" Nolu sipariş iptal edilemedi! ";
                TempData["Alert"] = "alert-danger";
            }
        }

        return Redirect("/seller/activeorders");
    }

    [HttpGet]
    public async Task<IActionResult> SetOutOrders()
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();

        var restaurant = await _restaurantService.GetRestaurantByUserIdAsync(user.Id);
        var activeOrders = await _orderService.GetSetOutOrderByRestaurantIdAsync(restaurant.Id);
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
    public async Task<IActionResult> ActiveOrder([FromForm] int orderId)
    {
        var order = await _orderService.GetOrderByIdAsync(orderId);
        if(order!=null)
        {
            order.Status=1;
            var result = await _orderService.UpdateAsync(order);
            if(result)
            {
                TempData["Message"]= order.Id.ToString()+" Nolu sipariş hazırlanıyor olarak değiştirildi. ";
                TempData["Alert"] = "alert-success";
                return Redirect("/seller/setoutorders");
            }
            else
            {
                TempData["Message"]= order.Id.ToString()+" Nolu sipariş durumu değiştirilemedi! ";
                TempData["Alert"] = "alert-danger";
            }
        }

        return Redirect("/seller/setoutorders");
    }

    [HttpPost]
    public async Task<IActionResult> CompledOrder([FromForm] int orderId)
    {
        var order = await _orderService.GetOrderByIdAsync(orderId);
        if(order!=null)
        {
            order.Status=3;
            order.DeliveryDate=DateTime.Now;
            var result = await _orderService.UpdateAsync(order);
            if(result)
            {
                TempData["Message"]= order.Id.ToString()+" Nolu sipariş teslim edildi ";
                TempData["Alert"] = "alert-primary";
                return Redirect("/seller/setoutorders");
            }
            else
            {
                TempData["Message"]= order.Id.ToString()+" Nolu sipariş teslim edilemedi! ";
                TempData["Alert"] = "alert-danger";
            }
        }

        return Redirect("/seller/setoutorders");
    }

    [HttpGet]
    public async Task<IActionResult> CompletedOrders()
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();
        
        var restaurant = await _restaurantService.GetRestaurantByUserIdAsync(user.Id);
        var activeOrders = await _orderService.GetComplatedOrderByRestaurantIdAsync(restaurant.Id);
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
    public async Task<IActionResult> CompletedOrders([FromForm] int? search)
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();
        var restaurant = await _restaurantService.GetRestaurantByUserIdAsync(user.Id);
        if(restaurant==null)
            return NotFound();
        List<Order> activeOrders;
        if(search!=null)
        {
            activeOrders = await _orderService.GetCompletedOrderByRestaurantIdAndOrderIdAsync(restaurant.Id,search);
        }
        else
        {
            activeOrders = await _orderService.GetComplatedOrderByRestaurantIdAsync(restaurant.Id);
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

    [HttpGet]
    public async Task<IActionResult> CanceledOrders()
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();
        
        var restaurant = await _restaurantService.GetRestaurantByUserIdAsync(user.Id);
        var activeOrders = await _orderService.GetCanceledOrderByRestaurantIdAsync(restaurant.Id);
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
}