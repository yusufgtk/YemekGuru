using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using YemekGuru.business.Abstract;
using YemekGuru.entity;
using YemekGuru.webapp.Identity;
using YemekGuru.webapp.Models;

namespace YemekGuru.webapp.Controllers;

[Authorize(Roles ="User")]
public class UserController:Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IAddressService _addressService;
    private readonly ICityService _cityService;
    private readonly IDistrictService _districtService;
    private readonly IRestaurantService _restaurantService;
    private readonly ICartService _cartService;
    private readonly ICartItemService _cartItemService;
    private readonly IApplySellerService _applySellerService;
    private readonly IOrderService _OrderService;
    private readonly IFeedbackService _feedbackService;
    private readonly IComplaintService _complaintService;

    public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IAddressService addressService, ICityService cityService, IDistrictService districtService, IRestaurantService restaurantService, ICartService cartService, ICartItemService cartItemService, IApplySellerService applySellerService, IOrderService OrderService, IFeedbackService feedbackService, IComplaintService complaintService)
    {
        _userManager=userManager;
        _signInManager=signInManager;
        _addressService=addressService;
        _cityService=cityService;
        _districtService=districtService;
        _restaurantService=restaurantService;
        _cartItemService=cartItemService;
        _cartService=cartService;
        _applySellerService=applySellerService;
        _OrderService=OrderService;
        _feedbackService=feedbackService;
        _complaintService=complaintService;
    }

    public async Task<IActionResult> Profile()
    {
        var userId = _userManager.GetUserId(User);
        var user = await _userManager.FindByIdAsync(userId);
        if(user==null)
        {
            return NotFound();
        }
        UserModel userModel = new UserModel(){
            Id=user.Id,
            UserName=user.UserName,
            FirstName=user.FirstName,
            LastName=user.LastName,
            EmailAddress=user.Email,
            ImageUrl=user.ImageUrl,
            PhoneNumber=user.PhoneNumber,
            GuruPuan=user.GuruPuan,
            AddressId=user.AddressId,
            IsActive=user.IsActive
        };

        var addresses = await _addressService.GetAddressesByUserIdAsync(userId);
        List<AddressModel> addressModels = new List<AddressModel>(){};
        foreach(var address in addresses)
        {
            addressModels.Add(new AddressModel(){
                Id=address.Id,
                UserId=address.UserId,
                Title=address.Title,
                Country=address.Country,
                City=address.City,
                District=address.District,
                FullAddress=address.FullAddress,
                Description=address.Description
            });
        }
        ProfilePageViewModel profilePageViewModel = new ProfilePageViewModel();
        profilePageViewModel.UserModel=userModel;
        profilePageViewModel.AddressModels=addressModels;
        ViewBag.Cities = await _cityService.GetCitiesAsync();
        ViewBag.Districts = await _districtService.GetDistrictsAsync();
        return View(profilePageViewModel);
    }

    public async Task<IActionResult> AddAddress()
    {
        ViewBag.Cities = await _cityService.GetCitiesAsync();
        ViewBag.Districts = await _districtService.GetDistrictsAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddAddress([FromForm] AddressModel addressModel)
    {
        var userId = _userManager.GetUserId(User);
        if(userId==null)
            return NotFound();

        var addresses = await _addressService.GetAddressesByUserIdAsync(userId);
        if(addresses.Count()>=5)
        {
            TempData["Message"]="En fazla 5 adet adres ekleyebilirsiniz!";
            TempData["Alert"]="alert-danger";
            return Redirect("/user/profile");
        }

        addressModel.UserId=userId;
        if(ModelState.IsValid)
        {
            Address address = new Address(){
                UserId=userId,
                Title=addressModel.Title,
                Country="Türkiye",
                City=addressModel.City,
                District=addressModel.District,
                FullAddress=addressModel.FullAddress,
                Description=addressModel.Description
            };
            var result = await _addressService.CreateAsync(address);
            if(!result)
                NotFound();
            
            return Redirect("/user/profile");
        }
        
        ViewBag.Cities = await _cityService.GetCitiesAsync();
        ViewBag.Districts = await _districtService.GetDistrictsAsync();
        return View(addressModel);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAddress([FromForm] int addressId)
    {
        var address = await _addressService.GetAddressByIdAsync(addressId);
        if(address!=null)
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            if(user.AddressId==addressId)
                user.AddressId=null;
            
            var result = await _userManager.UpdateAsync(user);
            if(result.Succeeded)
            {
                var result2 = await _addressService.DeleteAsync(address);
                if(!result2)
                    return NotFound();
            }
        }
        return Redirect("/user/profile");
    }

    [HttpPost]
    public async Task<IActionResult> SelectAddress([FromForm] int addressId)
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
                    foreach(var cartItem in cartItems)
                    {
                        await _cartItemService.DeleteAsync(cartItem);
                    }
                    
                }
            }
            user.AddressId=addressId;
            await _userManager.UpdateAsync(user);
        }
        return Redirect("/user/profile");
    }

    [HttpGet]
    public async Task<IActionResult> EditUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if(user!=null)
        {
            EditUserViewModel editUserModelView = new EditUserViewModel(){
                Id=userId,
                FirstName=user.FirstName,
                LastName=user.LastName,
                EmailAddress=user.Email,
                PhoneNumber=user.PhoneNumber,
                BirthDay=user.BirthDay,
                ImageUrl=user.ImageUrl
            };
            return View(editUserModelView);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> EditUser([FromForm] string userId, [FromForm] EditUserViewModel editUserViewModel, [FromForm] IFormFile? file)
    {   
        var user=await _userManager.FindByIdAsync(userId);
        if(user!=null)
        {
            
            if(await _userManager.IsInRoleAsync(user,"Seller"))
            {
                TempData["Message"]="Satıcı olduğunuzdan dolayı bilgilerinizi güncelleyemezsiniz!";
                TempData["Alert"]="alert-danger";
                return Redirect("/user/profile");
            }
        }

        editUserViewModel.Id=userId;
        if(ModelState.IsValid)
        {
            if(user!=null)
            {
                user.FirstName=editUserViewModel.FirstName;
                user.LastName=editUserViewModel.LastName;
                user.Email=editUserViewModel.EmailAddress;
                user.PhoneNumber=editUserViewModel.PhoneNumber;
                user.BirthDay=editUserViewModel.BirthDay;
                
                if(file!=null&&file.Length>0)
                {
                    var fileName = user.Id+".jpeg";
                    var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","UserImages",fileName);
                    using(var stream = new FileStream(path,FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    user.ImageUrl=fileName;
                }
                else
                {
                    user.ImageUrl=null;
                }


                var result = await _userManager.UpdateAsync(user);
                if(result.Succeeded)
                {
                    TempData["Message"]="Bilgileriniz Güncellendi.";
                    TempData["Alert"]="alert-success";
                }
                else
                {
                    TempData["Message"]="Hata Oluştu!";
                    TempData["Alert"]="alert-danger";
                }
                return Redirect("/user/profile");
            }
            return NotFound();
        }
        
        return View(editUserViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> UpdatePassword(string? userId)
    {
        if(userId==null)
            return NotFound();

        var user = await _userManager.FindByIdAsync(userId);
        if(user==null)
            return NotFound();
        
        UpdatePassword updatePassword = new UpdatePassword();
        updatePassword.UserId=user.Id;
        return View(updatePassword);
    }

    [HttpPost]
    public async Task<IActionResult> UpdatePassword([FromForm] string? userId, [FromForm] UpdatePassword updatePassword)
    {
        if(userId==null)
            return NotFound();

        if(ModelState.IsValid)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user==null)
                return NotFound();
            var result = await _userManager.ChangePasswordAsync(user,updatePassword.CurrentPassword,updatePassword.NewPassword);
            if(result.Succeeded)
            {
                TempData["Message"]="Şifreniz üncellendi!";
                TempData["Alert"]="alert-success";
                return Redirect("/user/profile");
                
            }
            else
            {
                TempData["Message"]="Şuanki şifreniz yanlış!";
                TempData["Alert"]="alert-danger";
                updatePassword.CurrentPassword=null;
            }
        }
        return View(updatePassword);
        
    }

    public async Task<IActionResult> ApplySeller()
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();
        
        var applySeller = await _applySellerService.GetApplySellerByUserIdAsync(user.Id);
        if(applySeller!=null)
        {   
            ViewBag.ApplySellerState="Başvurunuz Yemek Guru yetkilileri tarafından inceleniyor.";
            return View();
        }
        if(user.RestaurantId!=null)
        {
            var result = await _userManager.IsInRoleAsync(user,"Seller");
            ViewBag.IsSeller=result?true:false;
            return View();
        }
        ApplySellerViewModel applySellerViewModel = new ApplySellerViewModel(){
            UserId=user.Id,
            FirstName=user.FirstName,
            LastName=user.LastName,
            TCKN= user.TCKN,
            BirthDay=user.BirthDay.ToString(),
            PhoneNumber=user.PhoneNumber,
        };
        ViewBag.Cities=await _cityService.GetCitiesAsync();
        ViewBag.Districts=await _districtService.GetDistrictsAsync();
        return View(applySellerViewModel);
    }


    [HttpPost]
    public async Task<IActionResult> ApplySeller([FromForm] string? userId, [FromForm] ApplySellerViewModel model, [FromForm] IFormFile? file)
    {
        ViewBag.Cities = await _cityService.GetCitiesAsync();
        ViewBag.Districts = await _districtService.GetDistrictsAsync();
        
        if(userId==null)
            return NotFound();
        
        var user = await _userManager.FindByIdAsync(userId);
        if(user==null)
            return NotFound();

        
        model.UserId=userId;
        
        
        if(ModelState.IsValid)
        {
            ApplySeller applySeller = new ApplySeller(){
                UserId=user.Id,
                RestaurantName=model.RestaurantName,
                RestaurantPhoneNumber=model.RestaurantPhoneNumber,
                PhoneNumber=user.PhoneNumber,
                FirstName=model.FirstName,
                LastName=model.LastName,
                TCKN=model.TCKN,
                Country="Turkey",
                CityId=model.CityId,
                DistrictId=model.DistrictId,
                Address=model.Address,
                EmailAddress=user.Email,
                MinimumOrderPrice=model.MinOrderPrice,
                CreatedAt=DateTime.Now,
                UpdatedAt=DateTime.Now,
                IsActive=false,
                IsApproved=false,
                ApplyState=1
            };
            if(file!=null&&file.Length>0)
            {
                var fileName = user.Id+".jpeg";
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","ApplySellerImages",fileName);

                using(FileStream stream = new FileStream(path,FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                applySeller.ImageUrl=fileName;
            }
            else
            {
                applySeller.ImageUrl=null;
            }

            var result = await _applySellerService.CreateAsync(applySeller);
            if(result)
            {
                TempData["Message"]="Başvurunuz yapıldı.";
                TempData["Alert"]="alert-success";
                return Redirect("/user/profile");
            }
        }
        
        return View(model);
    }

    public async Task<IActionResult> Purse()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> ListOrders()
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        List<OrderModel> orderModels = new List<OrderModel>(){};
        if(user!=null)
        {
            var orders = await _OrderService.GetOrdersByUserIdAsync(user.Id);
            
            foreach(var order in orders)
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
                    DeliveryDate=order.DeliveryDate,
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
                            Name=orderItem.Product?.Name
                        },
                        Amount=orderItem.Amount,
                        Price=orderItem.Price,
                        TotalPrice=orderItem.TotalPrice,
                    }).ToList(),
                    
                });
                

            }
        }
        return View(orderModels);
    }

    [HttpGet]
    public async Task<IActionResult> AddComplaint(int orderId)
    {
        var order = await _OrderService.GetOrderByIdAsync(orderId);
        if(order!=null)
        {
            if(await _complaintService.IsThereComplaintByOrderIdAsync(orderId))
            {
                TempData["Message"]=orderId.ToString()+" no'lu siparişe zaten şikayet talebinde bulundun!";
                TempData["Alert"]="alert-danger";
                return Redirect("/user/listorders");
            }
            TimeSpan? time = DateTime.Now-order.DeliveryDate;

            if(time.HasValue&&time.Value.TotalHours>12)
            {
                TempData["Message"]="Sipariş teslim edildikten sonraki ilk 12 saat içinde şikayet talebi oluşturabilirsiniz."+order.Id.ToString()+" nolu siparişe şikayet talebi oluşturulamaz!";
                TempData["Alert"]="alert-danger";
                return Redirect("/user/listorders");
            }
            ComplaintModel complaintModel = new ComplaintModel(){
                OrderId=orderId,
                OrderModel = new OrderModel(){
                    Id=order.Id,
                    RestaurantId=order.RestaurantId,
                    RestaurantName=order.RestaurantName,
                    TotalPrice=order.TotalPrice,
                    CreatedAt=order.CreatedAt,
                    DeliveryDate=order.DeliveryDate,
                    Status=order.Status
                }
            };
            return View(complaintModel);
        }
        return Redirect("/user/listfeedbacks");
    }

    [HttpPost]
    public async Task<IActionResult> AddComplaint([FromForm] int orderId, [FromForm] ComplaintModel model)
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();

        if(await _complaintService.IsThereComplaintByOrderIdAsync(orderId))
        {
            TempData["Message"]=orderId.ToString()+" no'lu siparişe zaten şikayet talebinde bulundun!";
            TempData["Alert"]="alert-danger";
            return Redirect("/user/listorders");
        }
        var order = await _OrderService.GetOrderByIdAsync(orderId);
        if(order!=null)
        {
            TimeSpan? time = DateTime.Now-order.DeliveryDate;
            if(time.HasValue&&time.Value.TotalHours>12)
            {
                TempData["Message"]="Sipariş teslim edildikten sonraki ilk 24 saat içinde şikayet talebi oluşturabilirsiniz."+order.Id.ToString()+" nolu siparişe şikayet talebi oluşturulamaz!";
                TempData["Alert"]="alert-danger";
                return Redirect("/user/listorders");
            }
        }
        
        if(ModelState.IsValid)
        {
            Complaint complaint = new Complaint(){
                OrderId=orderId,
                UserName=user.UserName,
                Subject=model.Subject,
                Description=model.Description,
                CreatedAt=DateTime.Now,
                Status=false
            };
            var result = await _complaintService.CreateAsync(complaint);
            if(result)
            {
                TempData["Message"]="Şikayet talebiniz alındı!";
                TempData["Alert"]="alert-success";
                return Redirect("/user/listorders");
            }
        }
        
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ListFeedbacks()
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();

        var feedbacks=await _feedbackService.GetFeedbacksByUserNameAsync(user.UserName);
        List<FeedbackModel> feedbackModels = new List<FeedbackModel>();
        foreach(var f in feedbacks)
        {
            feedbackModels.Add(new FeedbackModel(){
                Id=f.Id,
                Subject=f.Subject,
                Description=f.Subject,
                Status=f.Status
            });
        }
        return View(feedbackModels);
    }

    [HttpPost]
    public async Task<IActionResult> AddFeedback([FromForm] string subject, [FromForm] string description)
    {
        var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
        if(user==null)
            return NotFound();
        
        if(await _feedbackService.IsThereActiveFeedbackAsync(user.UserName))
        {
            TempData["Message"]="İncelenmesi devam eden geri bildiriminiz olduğundan dolayı yeni geri bildirim oluşturamazsınız!";
            TempData["Alert"]="alert-danger";
            return Redirect("/user/listfeedbacks");
        }
        
        Feedback feedback = new Feedback(){
            Subject=subject,
            Description=description,
            UserName=user.UserName??"user",
            CreatedAt=DateTime.Now
        };
        var result = await _feedbackService.CreateAsync(feedback);
        if(result)
        {
            TempData["Message"]="Geri bildiriminiz oluşturuldu.";
            TempData["Alert"]="alert-success";
        }

        return Redirect("/user/listfeedbacks");
    }

}