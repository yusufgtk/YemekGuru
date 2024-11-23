using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace YemekGuru.webapp.Identity;

public class IdentityContext:IdentityDbContext<User>
{
    public IdentityContext(DbContextOptions<IdentityContext> options):base(options)
    {

    }
}



