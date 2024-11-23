using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using morshop.app.Identity;
using YemekGuru.business.Abstract;
using YemekGuru.business.Concrete;
using YemekGuru.entity;
using YemekGuru.repository.Abstract;
using YemekGuru.repository.Concrete;
using YemekGuru.webapp.EmailSErvice;
using YemekGuru.webapp.Hubs;
using YemekGuru.webapp.Identity;
using YemekGuru.webapp.Services;
using YemekGuru.webapp.Services.IyzicoServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IEmailService,EmailService>(i=>
    new EmailService(
        builder.Configuration["EmailService:Host"],
        builder.Configuration.GetValue<int>("EmailService:Port"),
        builder.Configuration.GetValue<bool>("EmailService:EnableSSL"),
        builder.Configuration["EmailService:EmailAddress"],
        builder.Configuration["EmailService:Password"])
);

var connectionString = builder.Configuration.GetConnectionString("SqliteConnectionString");
builder.Services.AddDbContext<IdentityContext>(options=>options.UseSqlite(connectionString));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSignalR();

//Other Services
builder.Services.AddScoped<IIyzicoService,IyzicoService>();

//Repositories
builder.Services.AddScoped<IRestaurantRepository,RestaurantRepository>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<ICartRepository,CartRepository>();
builder.Services.AddScoped<ICartItemRepository,CartItemRepository>();
builder.Services.AddScoped<IAddressRepository,AddressRepository>();
builder.Services.AddScoped<ICityRepository,CityRepository>();
builder.Services.AddScoped<IDistrictRepository,DistrictRepository>();
builder.Services.AddScoped<IApplySellerRepository,ApplySellerRepository>();
builder.Services.AddScoped<IOrderRepository,OrderRepository>();
builder.Services.AddScoped<ICommentRepository,CommentRepository>();
builder.Services.AddScoped<IFeedbackRepository,FeedbackRepository>();
builder.Services.AddScoped<IComplaintRepository,ComplaintRepository>();

//Business
builder.Services.AddScoped<IRestaurantService,RestaurantManager>();
builder.Services.AddScoped<IProductService,ProductManager>();
builder.Services.AddScoped<ICategoryService,CategoryManager>();
builder.Services.AddScoped<ICartService,CartManager>();
builder.Services.AddScoped<ICartItemService,CartItemManager>();
builder.Services.AddScoped<IAddressService,AddressManager>();
builder.Services.AddScoped<ICityService,CityManager>();
builder.Services.AddScoped<IDistrictService,DistrictManager>();
builder.Services.AddScoped<IApplySellerService,ApplySellerManager>();
builder.Services.AddScoped<IOrderService,OrderManager>();
builder.Services.AddScoped<ICommentService,CommentManager>();
builder.Services.AddScoped<IFeedbackService,FeedbackManager>();
builder.Services.AddScoped<IComplaintService,ComplaintManager>();

//Identity Option
builder.Services.Configure<IdentityOptions>(options=>{
        //Password
    options.Password.RequireDigit=true;//Parolanun içerisinde sayı olsun mu?
    options.Password.RequireLowercase=true;//Parolanun içerisinde küçük harf olsun mu?
    options.Password.RequireUppercase=false;//Parolanun içerisinde büyük harf olsun mu?
    options.Password.RequiredLength=6;//Parola Minimum kaç karakter olsun?
    options.Password.RequireNonAlphanumeric=false;//Parolanın içerisinde özel karakter olsun mu?

    //Lockout
    options.Lockout.AllowedForNewUsers=true;//Şifreyi yanlış girme sayısını doldurunca kilitlensin mi?
    options.Lockout.MaxFailedAccessAttempts=5;//Şifreyi en fazla kaçkere yanlış girebilsin?
    options.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(5);//Kilitlenen hesap ne kadar süre sonra açılsın?
    
    //User
    //options.User.AllowedUserNameCharacters="";//User adında İstediğini karakterleri yazın?
    options.User.RequireUniqueEmail=true;//user ların e-mail adresleri benzersiz mi olsun?
    options.SignIn.RequireConfirmedEmail=false;//user mailini onaylatması zorunlu olsun mu?
    options.SignIn.RequireConfirmedPhoneNumber=false;//user telefon no onaylatması zorunlu olsun mu?

});

//Cookie Ayarları
builder.Services.ConfigureExternalCookie(options=>{
    options.LoginPath="/account/login";//Kullanıcı login değilse nereye yonlendirilsin?
    options.LogoutPath="/account/logout";//Kullanıcı çıkış yaptığında nereye yönlendirilsin?
    options.AccessDeniedPath="/account/accessdenied";//Yekilendirme Admin,seller,costumer vb... Yani yetkisi hangi sayfaya yönlendirilsin?
    options.SlidingExpiration=false;//İstek yaptığında cookie süresi tazelensin mi?
    options.ExpireTimeSpan=TimeSpan.FromDays(1);//Cookie nekadar süre sonra silinsin? 
    //Güvenlik
    options.Cookie=new CookieBuilder
    {
        HttpOnly=true,//Sadece Http isteklerini kabul et
        Name=".YemekGuru.Security.Cookie",//Cookie nin Adı
        SameSite=SameSiteMode.Strict //bu özellik Cookie çalındığında başka bir cihazdan kullanılmasını önler
    };

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapHub<AdminHub>("/adminhub");

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var userManager = services.GetRequiredService<UserManager<User>>();
var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
var configuration = services.GetRequiredService<IConfiguration>();
SeedIdentity.Seed(userManager, roleManager, configuration).Wait();

app.Run();
