namespace YemekGuru.webapp.Models;

public class ApplySellerModel
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? TCKN { get; set; }
    public string? ImageUrl { get; set; }
    public string? BirthDay { get; set; }
    public string? RestaurantName { get; set; }
    public string? EmailAddress { get; set; }
    public string? PhoneNumber { get; set; }
    public string? RestaurantPhoneNumber { get; set; }
    public int? MinimumOrderPrice { get; set; }
    public string? Country { get; set; }
    public int? CityId { get; set; }
    public int? DistrictId { get; set; }
    public string? Address { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsApproved { get; set; }
    public int? ApplyState { get; set; } //Null: Başvuru yok / 1 : Başvuru yapıldı İnceleniyor / 2 : Onaylanmadı / 3 onaylandı artık satıcısın
}
