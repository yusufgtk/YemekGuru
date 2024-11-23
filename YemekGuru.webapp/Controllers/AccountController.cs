using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YemekGuru.business.Abstract;
using YemekGuru.entity;
using YemekGuru.webapp.EmailSErvice;
using YemekGuru.webapp.Identity;
using YemekGuru.webapp.Models;

namespace YemekGuru.webapp.Controllers;

public class AccountController:Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ICartService _cartService;
    private readonly IEmailService _emailService;
    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ICartService cartService, IEmailService emailService)
    {
        _userManager=userManager;
        _signInManager=signInManager;
        _cartService=cartService;
        _emailService=emailService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login([FromForm]LoginModel loginModel)
    {
        if(ModelState.IsValid&&loginModel.UserName!=null&&loginModel.Password!=null)
        {
            var user = await _userManager.FindByNameAsync(loginModel.UserName);
            if(user==null)
            {
                TempData["Message"] = loginModel.UserName+" adlı kullanıcı adı sistemde kayıtlı değil!";
                TempData["Alert"] = "alert-Danger";
                return View(loginModel);
            }
            var result = await _signInManager.PasswordSignInAsync(user,loginModel.Password,true,false);
            if(result.Succeeded)
            {
                if(await _userManager.IsInRoleAsync(user,"Admin"))
                {
                    return Redirect("/Admin/Dashboard");
                }
                return Redirect("/home/index");
            }
            else{
                TempData["Message"] = "Girilen şifre yanlış!";
                TempData["Alert"] = "alert-Danger";
            }

        }
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index","Home");
    }
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> RegisterAsync([FromForm]RegisterModel registerModel)
    {
        if(ModelState.IsValid)
        {
            User user = new User()
            {
                UserName=registerModel.UserName,
                Email=registerModel.EmailAddress,
                GuruPuan=0,
                CreatedTime=DateTime.Now,
                UpdatedTime=DateTime.Now,
            };
            if(registerModel.Password!=null&&user.UserName!=null)
            {
                var result = await _userManager.CreateAsync(user, registerModel.Password);
                if(result.Succeeded)
                {
                    var user2 = await _userManager.FindByNameAsync(user.UserName);
                    if(user2!=null)
                    {
                        await _userManager.AddToRoleAsync(user2,"User");
                        var cart = new Cart(){
                            UserId=user2.Id,
                            RestaurantId=-1,
                            Amount=0,
                            TotalPrice=0
                        };
                        var result2= await _cartService.CreateAsync(cart);
                    }
                    
                    TempData["Message"] = user.UserName+" kaydınız başarıyla oluşturuldu!";
                    TempData["Alert"] = "alert-success";
                    return Redirect("/account/login");
                }
                
            }
            else
            {
                TempData["Message"] = "Kaydınız oluşturulurken bir hata ile karşılaşıldı!";
                TempData["Alert"] = "alert-danger";
            }
        }
        
        return View("Register",registerModel);
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword([FromForm] string? userName)
    {
        if(userName!=null)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if(user!=null)
            {
                var userEmailAddress = user.Email;

                Random rnd = new Random();
                var newPassword=userName+rnd.Next(100000,1000000).ToString();
                var message = $"Yeni şifreniz: {newPassword}";
                var subject = "Yeni Şifre";
                await _emailService.SendEmail(userEmailAddress,subject,message);
                var result = await _userManager.RemovePasswordAsync(user);
                if(result.Succeeded)
                {
                    var result2 = await _userManager.AddPasswordAsync(user,newPassword);
                    if(result2.Succeeded)
                    {
                        TempData["Message"] = "Yeni şifreniz mail adresinize gönderildi.";
                        TempData["Alert"] = "alert-success";
                    }
                }
            }
            else
            {
                TempData["Message"] = "Böyle bir kullanıcı bulunamadı!";
                TempData["Alert"] = "alert-danger";
            }
            
        }
        else
        {
            ModelState.AddModelError("","Zorunlu alan!");
        }
        return View();
    }

    public IActionResult AccessDenied()
    {
        return View();
    }


}