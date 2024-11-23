using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YemekGuru.business.Abstract;
using YemekGuru.entity;
using YemekGuru.webapp.Identity;
using YemekGuru.webapp.Models;

namespace YemekGuru.webapp.Controllers;

public class RestaurantController:Controller
{
    private readonly IRestaurantService _restaurantService;
    private readonly IProductService _productService;
    private readonly UserManager<User> _userManager;
    private readonly ICommentService _commentService;
    private readonly IOrderService _orderService;
    public RestaurantController(IRestaurantService restaurantService, IProductService productService, UserManager<User> userManager, ICommentService commentService, IOrderService orderService)
    {
        _restaurantService=restaurantService;
        _productService=productService;
        _userManager=userManager;
        _commentService=commentService;
        _orderService=orderService;
    }
    public async Task<IActionResult> Detail(int restaurantId)
    {
        var restaurant = await _restaurantService.GetRestaurantByIdAsync(restaurantId);
        if(restaurant==null)
        {
            return NotFound();
        }
        var products = await _productService.GetProductsByRestaurantIdAsync(restaurant.Id);

        RestaurantModel restaurantModel = new RestaurantModel(){
            Id=restaurant.Id,
            Name=restaurant.Name,
            Description=restaurant.Description,
            ImageUrl=restaurant.ImageUrl,
            Address=restaurant.Address,
            PhoneNumber=restaurant.PhoneNumber,
            OpeningTime=restaurant.OpeningTime,
            ClosingTime=restaurant.ClosingTime,
            DeliveryTime=restaurant.DeliveryTime,
            MinimumOrderPrice=restaurant.MinimumOrderPrice,
            Rating=restaurant.Rating,
            IsActive=restaurant.IsActive,
            IsApproved=restaurant.IsApproved
        };

        List<ProductModel> productModels = new List<ProductModel>(){};
        foreach(var product in products)
        {
            productModels.Add(
                new ProductModel(){
                    Id=product.Id,
                    RestaurantId=product.RestaurantId,
                    CategoryId=product.CategoryId,
                    Name=product.Name,
                    Description=product.Description,
                    Content=product.Content,
                    Calorie=product.Calorie,
                    ImageUrl=product.ImageUrl,
                    PreviousPrice=product.PreviousPrice,
                    Price=product.Price,
                    IsActive=product.IsActive
                }
            );
        }

        var comments = await _commentService.GetCommentsByRestaurantIdAsync(restaurant.Id);
        List<CommentModel> commentModels = new List<CommentModel>();
        foreach(var c in comments)
        {
            commentModels.Add(new CommentModel(){
                Id=c.Id,
                RestaurantId=c.RestaurantId,
                UserName=c.UserName,
                Description=c.Description,
                CreatedAt=c.CreatedAt.Date
            });
        }
        RestaurantDetailViewModel restaurantDetailViewModel = new RestaurantDetailViewModel();
        restaurantDetailViewModel.RestaurantModel=restaurantModel;
        restaurantDetailViewModel.ProductModels=productModels;
        restaurantDetailViewModel.CommentModels=commentModels;
        restaurantDetailViewModel.TotalCommentCount=commentModels.Count();
        
        return View(restaurantDetailViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> AddRestaurantComment(int orderId)
    {
        var order = await _orderService.GetOrderByIdAsync(orderId);
        if(order==null)
            return NotFound();
        
        if(order.CommentId!=null)
        {   
            TempData["Message"]="Bu siparişe zaten yorum yaptınız.";
            TempData["alert"]="alert-success";
            return Redirect("/user/listOrders");
        }
        
        var restaurant = await _restaurantService.GetRestaurantByIdAsync(order.RestaurantId);
        if(restaurant!=null)
        {
            AddCommnetModel addCommnetModel = new AddCommnetModel(){
                RestaurantId=restaurant.Id,
                OrderId=orderId
            };
            ViewBag.RestaurantName=restaurant.Name;
            return View(addCommnetModel);
        }
        return Redirect("/user/listOrders");
    }

    [HttpPost]
    public async Task<IActionResult> AddRestaurantComment([FromForm] int orderId, [FromForm] AddCommnetModel model)
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();

        var order = await _orderService.GetOrderByIdAsync(orderId);
        if(order==null)
            return NotFound();
        
        if(order.CommentId!=null)
        {   
            TempData["Message"]=order.Id.ToString()+" id'li siparişe zaten yorum yaptınız.";
            TempData["alert"]="alert-success";
            return Redirect("/user/listOrders");
        }
        if(ModelState.IsValid)
        {
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(order.RestaurantId);
            if(restaurant!=null)
            {
                Comment comment = new Comment(){
                    RestaurantId=restaurant.Id,
                    UserName=user.UserName,
                    Description=model.Description,
                    OrderId=order.Id,
                    CreatedAt=DateTime.Now,
                    UpdateAt=DateTime.Now,
                    IsApproved=false
                };
                await _commentService.CreateAsync(comment);
                order.CommentId=1;
                await _orderService.UpdateAsync(order);
                TempData["Message"]="Yorumunuz yapıldı.";
                TempData["Alert"]="alert-success";
                return Redirect("/user/listOrders");
            }
            
        }
        return View(model);
    }

}