using Microsoft.AspNetCore.Identity;

namespace YemekGuru.webapp.Identity;

public class User:IdentityUser
{
    public int? RestaurantId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ImageUrl { get; set; }
    public string? TCKN { get; set; }
    public float? Purse { get; set; }
    public int? GuruPuan { get; set; }
    public int? AddressId { get; set; }
    public DateTime? BirthDay { get; set; }
    
    public bool? IsApproved { get; set; }
    public bool? IsActive { get; set; }
    public int? ApplySeller { get; set; }

    
    public DateTime? CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
}