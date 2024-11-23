
using Microsoft.AspNetCore.Identity;
using YemekGuru.webapp.Identity;

namespace morshop.app.Identity
{
    public static class SeedIdentity
    {
        //IConfiguration ile appsetting üzerindeki bilgileri almamızı sağlar
        public static async Task Seed(UserManager<User> userManager,RoleManager<IdentityRole> roleManager,IConfiguration configuration)
        {
            var username=configuration["Data:AdminUser:username"];
            var password=configuration["Data:AdminUser:password"];
            var email=configuration["Data:AdminUser:email"];
            var role=configuration["Data:AdminUser:role"];

            if(await userManager.FindByNameAsync(username)==null)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
                var user = new User{
                    FirstName="admin",
                    LastName="admin",
                    UserName=username,
                    Email=email,
                    EmailConfirmed=true,
                };
                var result = await userManager.CreateAsync(user,password);
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user,role);
                    
                    var role2 = new IdentityRole(){
                        Name="User",
                        NormalizedName="USER"
                    };
                    await roleManager.CreateAsync(role2);

                    var role3 = new IdentityRole(){
                        Name="Seller",
                        NormalizedName="SELLER"
                    };
                    await roleManager.CreateAsync(role3);
                }
            }
        }
    }


}