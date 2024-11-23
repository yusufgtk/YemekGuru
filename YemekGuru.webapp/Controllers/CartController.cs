using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YemekGuru.business.Abstract;
using YemekGuru.entity;
using YemekGuru.webapp.EmailSErvice;
using YemekGuru.webapp.Identity;
using YemekGuru.webapp.Models;
using YemekGuru.webapp.Services.IyzicoServices;

namespace YemekGuru.webapp.Controllers;


public class CartController:Controller
{
    private bool? _resultPayment;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signManager;
    private readonly ICartService _cartService;
    private readonly ICartItemService _cartItemService;
    private readonly IProductService _productService;
    private readonly IRestaurantService _restaurantService;
    private readonly IAddressService _addressService;
    private readonly ICityService _cityService;
    private readonly IDistrictService _districtService;
    private readonly IOrderService _orderService;
    private readonly IEmailService _emailService;
    private readonly IIyzicoService _iyzicoService;

    public CartController(UserManager<User> userManager, SignInManager<User> signInManager, ICartService cartService, ICartItemService cartItemService, IProductService productService, IRestaurantService restaurantService, IAddressService addressService, ICityService cityService, IDistrictService districtService, IOrderService orderService, IEmailService emailService, IIyzicoService iyzicoService)
    {
        _userManager=userManager;
        _signManager=signInManager;
        _cartService=cartService;
        _cartItemService=cartItemService;
        _productService=productService;
        _restaurantService=restaurantService;
        _addressService=addressService;
        _cityService=cityService;
        _districtService=districtService;
        _orderService=orderService;
        _emailService=emailService;
        _iyzicoService=iyzicoService;
    }

    public async Task<IActionResult> Detail()
    {
        var cart = await _cartService.GetCartByUserIdAsync(_userManager.GetUserId(User));
        CartModel cartModel = new CartModel(){
            Id=cart.Id,
            UserId=cart.UserId,
            RestaurantId=cart.RestaurantId,
            Amount=cart.Amount,
            TotalPrice=cart.TotalPrice

        };
        var cartItems = await _cartItemService.GetCartItemsByCartIdAsync(cart.Id);
        if(cartItems==null)
        {
            return NotFound();
        }
        List<CartItemModel> cartItemModels = new List<CartItemModel>(){};
        decimal totalPrice = 0;
        foreach(var cartItem in cartItems)
        {
            cartItemModels.Add(new CartItemModel(){
                Id=cartItem.Id,
                CartId=cartItem.CartId,
                ProductId=cartItem.ProductId,
                Product=cartItem.Product,
                Amount=cartItem.Amount,
            });
            // cartItem.Product.Price ve cartItem.Amount nullable türler ise
            if (cartItem.Product?.Price != null && cartItem.Amount != null)
            {
                totalPrice += (decimal)cartItem.Product.Price * (int)cartItem.Amount;
            }
        }
        
        cartModel.TotalPrice=totalPrice;
        CartDetailViewModel cartDetailViewModel = new CartDetailViewModel();
        cartDetailViewModel.CartModel=cartModel;
        cartDetailViewModel.CartItemModels=cartItemModels;
        
        var restaurant = await _restaurantService.GetRestaurantByIdAsync(cart.RestaurantId);
        if(restaurant != null)
        {
            RestaurantModel restaurantModel = new RestaurantModel(){
                Id=restaurant.Id,
                Name=restaurant.Name,
                MinimumOrderPrice=restaurant.MinimumOrderPrice
            };
            cartDetailViewModel.RestaurantModel=restaurantModel;
        };
        

        
        return View(cartDetailViewModel);
    }
    public async Task<IActionResult> AddCartItem(int ProductId, string ReturnUrl)
    {
        var userId = _userManager.GetUserId(User);
        var user = await _userManager.FindByIdAsync(userId);
        if(user==null)
        {
            TempData["Message"]="Giriş yapınız!";
            TempData["Alert"]="alert-danger";
            return Redirect(ReturnUrl);
        }
        if (!await _userManager.IsInRoleAsync(user,"User"))
        {
            TempData["Message"]="Yetkiniz Yok!";
            TempData["Alert"]="alert-danger";
            return Redirect(ReturnUrl);
        }
        var address = await _addressService.GetAddressByIdAsync(user.AddressId);
        var cart = await _cartService.GetCartByUserIdAsync(userId);
        var product = await _productService.GetProductByIdAsync(ProductId);
        var cartItems = await _cartItemService.GetCartItemsByCartIdAsync(cart.Id);
        var restaurant = await _restaurantService.GetRestaurantByIdAsync(product.RestaurantId);
        decimal? price = 0;
        if(user.RestaurantId==restaurant.Id)
        {
            TempData["Message"]="Kendi retorantınızdan ürün sipariş veremezsiniz!";
            TempData["Alert"]="alert-danger";
            return Redirect(ReturnUrl);
        }
        if(address==null)
        {
            TempData["Message"]="Adres ekleyin!";
            TempData["Alert"]="alert-danger";
            return Redirect(ReturnUrl);
        }
        if(restaurant.CityId!=address.City||restaurant.DistrictId!=address.District)
        {
            TempData["Message"]="Restaurant sizin bulunduğunuz bölgeye hizmet vermiyor!";
            TempData["Alert"]="alert-danger";
            return Redirect(ReturnUrl);
        }
        if(restaurant!=null&&restaurant.IsActive==false)
        {
            TempData["Message"]="Restaurant kapalı olduğundan dolayı ürünü sepetinize ekleyemezsiniz!";
            TempData["Alert"]="alert-danger";
            return Redirect(ReturnUrl);
        }
        if(cart.RestaurantId==product.RestaurantId)
        {
            var entity =  cartItems.FirstOrDefault(i=>i.ProductId==ProductId);
            if(entity!=null)
            {
                price=entity.Price;
                entity.Amount++;
                var result = await _cartItemService.UpdateAsync(entity);
                if(!result)
                    return NotFound();
            }
            else
            {
                CartItem cartItem = new CartItem(){
                    ProductId=ProductId,
                    CartId=cart.Id,
                    Amount=1,
                };
                var result = await _cartItemService.CreateAsync(cartItem);
                if(!result)
                    return NotFound();
            }
                
        }
        else if(cart.RestaurantId==-1)
        {
            cart.RestaurantId=product.RestaurantId;
            var result = await _cartService.UpdateAsync(cart);

            CartItem cartItem = new CartItem(){
                ProductId=ProductId,
                CartId=cart.Id,
                Amount=1,
            };

            var result2 = await _cartItemService.CreateAsync(cartItem);
            if(!result2)
                return NotFound();

        }
        else
        {
            foreach(var item in cartItems)
            {
                var result3 = await _cartItemService.DeleteAsync(item);
            }
            cart.RestaurantId=product.RestaurantId;
            var result = await _cartService.UpdateAsync(cart);
            if(!result)
                return NotFound();

            CartItem cartItem = new CartItem(){
                ProductId=product.Id,
                CartId=cart.Id,
                Amount=1,
                Price=null,
                TotalPrice=null
            };

            var result2 = await _cartItemService.CreateAsync(cartItem);
            if(!result2)
                return NotFound();

        }

        Console.WriteLine($"AddCartItem method called with ProductId: {ProductId}, ReturnUrl: {ReturnUrl}");

        return Redirect(ReturnUrl);
        //return Json(new {ProductId,ReturnUrl,Price=price});
    }
    public async Task<IActionResult> DeleteCartItem([FromForm] int ProductId, [FromForm] string ReturnUrl)
    {
        var userId = _userManager.GetUserId(User);
        if(User==null)
            return NotFound();
        var cart = await _cartService.GetCartByUserIdAsync(userId);
        var product = await _productService.GetProductByIdAsync(ProductId);
        var cartItems = await _cartItemService.GetCartItemsByCartIdAsync(cart.Id);
        decimal? price = 0.0m;
        if(cart==null||product==null)
        {
            return NotFound();
        }
        if(cart.RestaurantId==product.RestaurantId)
        {
            var entity =  cartItems.FirstOrDefault(i=>i.ProductId==ProductId);
            price = entity.Price;
            if(entity!=null)
            {
                entity.Amount--;
                if(entity.Amount<=0)
                {
                    var result = await _cartItemService.DeleteAsync(entity);
                    
                }
                else
                {
                    var result = await _cartItemService.UpdateAsync(entity);
                }
                
            }
            
            var items = await _cartItemService.GetCartItemsByCartIdAsync(cart.Id);
            if(items.Count<=0)
            {
                cart.RestaurantId=-1;
                var result2 = await _cartService.UpdateAsync(cart);
            }
            
        }
        return Redirect("/cart/detail");
    }

    [HttpGet]
    public async Task<IActionResult> OrderSummary()
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();
        var cart = await _cartService.GetCartByUserIdAsync(user.Id);
        if(cart==null)
            return NotFound();

        UserModel userModel = new UserModel(){
            Id=user.Id,
            FirstName=user.FirstName,
            LastName=user.LastName,
            PhoneNumber=user.PhoneNumber,
            AddressId=user.AddressId
        };
        var addresses = await _addressService.GetAddressesByUserIdAsync(user.Id);
        List<AddressModel> addressModels = new List<AddressModel>(){};
        foreach(var address in addresses)
        {
            addressModels.Add(new AddressModel(){
                Id=address.Id,
                Title=address.Title,
                Country=address.Country,
                City=address.City,
                District=address.District,
                FullAddress=address.FullAddress,
                Description=address.Description
            });
        }
        var cartItems = await _cartItemService.GetCartItemsByCartIdAsync(cart.Id);
        var restaurant = await _restaurantService.GetRestaurantByIdAsync(cart.RestaurantId);
        decimal? totalPrice=0;
        foreach(var cartItem in cartItems)
        {
            totalPrice += (decimal)cartItem.Product.Price * (int)cartItem.Amount;
        }
        CartModel cartModel = new CartModel(){
            Id=cart.Id,
            UserId=cart.UserId,
            RestaurantId=cart.RestaurantId,
            TotalPrice=totalPrice
        };
        if(restaurant==null||cartItems.Count()<=0||restaurant.MinimumOrderPrice>(float)totalPrice)
        {
            return Redirect("/cart/detail");
        }
        List<CartItemModel> cartItemModels =new List<CartItemModel>(){};
        foreach(var cartItem in cartItems)
        {
            cartItemModels.Add(new CartItemModel(){
                Id=cartItem.Id,
                ProductId=cartItem.ProductId,
                Product=cartItem.Product,
                Amount=cartItem.Amount,
                Price=cartItem.Price,
                TotalPrice=cartItem.TotalPrice
            });
        }
        
        
        
        OrderSummaryViewModel orderSummaryViewModel = new OrderSummaryViewModel();
        orderSummaryViewModel.UserModel=userModel;
        orderSummaryViewModel.AddressModels=addressModels;
        orderSummaryViewModel.CartItemModels=cartItemModels;
        orderSummaryViewModel.CartModel=cartModel;
        ViewBag.Cities=await _cityService.GetCitiesAsync();
        ViewBag.Districts=await _districtService.GetDistrictsAsync();

        return View(orderSummaryViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> ChangeAddress([FromForm] string userId, [FromForm] int addressId)
    {

        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user!=null)
        {
            if(user.AddressId!=addressId)
            {
                var cart = await _cartService.GetCartByUserIdAsync(user.Id);
                var cartItems = await _cartItemService.GetCartItemsByCartIdAsync(cart.Id);
                if(cartItems.Count()>0)
                {
                    cart.RestaurantId=-1;
                    await _cartService.UpdateAsync(cart);
                    foreach(var cartItem in cartItems)
                    {
                        await _cartItemService.DeleteAsync(cartItem);
                    }
                    
                }
            }
            user.AddressId=addressId;
            var result = await _userManager.UpdateAsync(user);
            if(result.Succeeded)
            {
                TempData["Message"]="Seçili adres değiştirildi!";
                TempData["Alert"]="alert-success";
            }
            else
            {
                TempData["Message"]="Adres değiştirilemedi!";
                TempData["Alert"]="alert-danger";
            }
        }
        return Redirect("/cart/ordersummary");
    }

    [HttpGet]
    public async Task<IActionResult> Payment()
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();
        
        var addresses = await _addressService.GetAddressesByUserIdAsync(user.Id);
        var cart = await _cartService.GetCartByUserIdAsync(user.Id);
        var cartItems = await _cartItemService.GetCartItemsByCartIdAsync(cart.Id);
        var restaurant = await _restaurantService.GetRestaurantByIdAsync(cart.RestaurantId);
        if(restaurant==null)
            return Redirect("/cart/detail");
        decimal? totalPrice = 0;
        foreach(var cartItem in cartItems)
        {
            totalPrice += (decimal)cartItem.Product.Price * (int)cartItem.Amount;
        }
        
        if(user.FirstName==null||user.LastName==null||user.PhoneNumber==null||user.AddressId==null||addresses.Count()<=0||totalPrice<=0)
        {
            return Redirect("/cart/ordersummary");
        }
        var address = await _addressService.GetAddressByIdAsync(user.AddressId);
        if(cart==null||cartItems.Count()<=0||restaurant.MinimumOrderPrice>=(float)totalPrice)
        {
            return Redirect("/cart/detail");
        }
        if(address.City!=restaurant.CityId||address.District!=restaurant.DistrictId)
        {   
            TempData["Message"]="Restorant seçili adrese hizmet vermiyor!";
            TempData["Alert"]="alert-danger";
            return Redirect("/cart/detail");
        }
        ViewBag.TotalPrice=totalPrice;
        return View();
        
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Payment([FromForm] PaymentModel model)
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();
        var addresses = await _addressService.GetAddressesByUserIdAsync(user.Id);
        var cart = await _cartService.GetCartByUserIdAsync(user.Id);
        var cartItems = await _cartItemService.GetCartItemsByCartIdAsync(cart.Id);
        var restaurant = await _restaurantService.GetRestaurantByIdAsync(cart.RestaurantId);
        if(restaurant==null)
            return Redirect("/cart/detail");
        decimal? totalPrice = 0;
        foreach(var cartItem in cartItems)
        {
            totalPrice+=(decimal)cartItem.Product.Price*(int)cartItem.Amount;
        }
        
        if(user.FirstName==null||user.LastName==null||user.PhoneNumber==null||user.AddressId==null||addresses.Count()<=0||totalPrice<=0)
        {
            return Redirect("/cart/ordersummary");
        }
        var address = await _addressService.GetAddressByIdAsync(user.AddressId);
        if(cart==null||cartItems.Count()<=0||restaurant.MinimumOrderPrice>=(float)totalPrice)
        {
            return Redirect("/cart/detail");
        }
        
        if(address.City!=restaurant.CityId||address.District!=restaurant.DistrictId)
        {   
            TempData["Message"]="Restorant seçili adrese hizmet vermiyor!";
            TempData["Alert"]="alert-danger";
            return Redirect("/cart/detail");
        }
        if(ModelState.IsValid)
        {
            List<OrderItem> orderItems = new List<OrderItem>(){};
            if(cartItems.Count>0)
            {
                foreach(var cartItem in cartItems)
                {
                    orderItems.Add(new OrderItem(){
                        ProductId=cartItem.ProductId,
                        Amount=cartItem.Amount,
                        Price=cartItem.Product?.Price,
                        TotalPrice=cartItem.Product?.Price*cartItem.Amount,
                        CreatedAt=DateTime.Now
                    });
                }
            }
            Order order = new Order(){
                RestaurantId=restaurant.Id,
                RestaurantName=restaurant.Name,
                ProductTypesNumber=cartItems.Count(),
                TotalPrice=totalPrice,
                CreatedAt=DateTime.Now,
                Status=1,
                Country="Turkey",
                CityId=address.City,
                DistrictId=address.District,
                Address=address.FullAddress,
                FullName=user.FirstName+" "+user.LastName,
                UserId=user.Id,
                PhoneNumber=user.PhoneNumber,
                OrderItems=orderItems
            };

            var result = await _orderService.CreateAsync(order);
            if(result)
            {
                // TempData["Message"]="Ödeme başarılı! Siparişiniz oluşturuldu.";
                // TempData["Alert"]="alert-success";
                cart.RestaurantId=-1;
                await _cartService.UpdateAsync(cart);
                foreach(var cartItem in cartItems)
                {
                    await _cartItemService.DeleteAsync(cartItem);
                }
                if(totalPrice!=null)
                {
                    var gp = totalPrice/100;
                    decimal temp = gp ?? 0;
                    user.GuruPuan+=Convert.ToInt32(Math.Floor(temp));
                    await _userManager.UpdateAsync(user);
                }
                string? messageContent = "Siparişin Verildi!\n\n"; 

                messageContent += "Restoran: " + order.RestaurantName + "\n\n";

                messageContent += "Ürün Adedi: " + order.ProductTypesNumber.ToString() + "\n\n";  

                messageContent += "Sipariş tarihi: " + order.CreatedAt.ToString() + "\n\n";  
                messageContent += "Toplam Tutar: " + order.TotalPrice + " TL\n"; 

                string? emailAddress = user.Email;
                _emailService.SendEmail(emailAddress, "Yemek Guru", messageContent);
                


                // Payment payment = new Payment(){
                //     Status=1,
                //     Type=1,
                //     CreatedAt=DateTime.Now,
                //     TotalPrice=totalPrice,
                //     End4Number=Convert.ToInt32(model.CardNumber.Substring(model.CardNumber.Length-4)),
                //     OrderId=order.Id,
                //     RestaurantId=restaurant.Id,
                //     UserId=user.Id
                // };

                _resultPayment=true;
                _iyzicoService.Pay(order,model);
                return Redirect("/cart/successpayment");
            }
            else
            {
                TempData["Message"]="Ödeme başarısız! Siparişiniz oluşturulamadı.";
                TempData["Alert"]="alert-danger";
                _resultPayment=false;
                return Redirect("/cart/failpayment");
            }
        }
        return View(model);
        
    }

    [HttpGet]
    public async Task<IActionResult> SuccessPayment()//çalışmıyor
    {

        // if(_resultPayment==true)
        // {
        //     _resultPayment=null;
            
        // }
        return View();
        // return NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> DangerPayment()//çalışmıyor
    {
        // if(_resultPayment==false)
        // {
        //     _resultPayment=null;
            
        // }
        return View();

        // return NotFound();
    }
    
}