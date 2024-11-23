using System.ComponentModel;

namespace YemekGuru.webapp.Models;

public class RestaurantModel
{
    public int Id { get; set; }
    public string? UserId { get; set; }

    [DisplayName("Restaurant Adı")]
    public string? Name { get; set; }

    [DisplayName("Restaurant Tanımı")]
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }

    [DisplayName("Ülke")]
    public string? Country { get; set; }

    [DisplayName("Şehir")]
    public int? CityId { get; set; }

    [DisplayName("İlçe")]
    public int? DistrictId { get; set; }

    [DisplayName("Adres")]
    public string? Address { get; set; }

    [DisplayName("Telefon Numarası")]
    public string? PhoneNumber { get; set; }
    [DisplayName("Email Adresi")]
    public string? EmailAdress { get; set; }

    [DisplayName("Açılış Saati")]
    public string? OpeningTime { get; set; }

    [DisplayName("Kapanış Saati")]
    public string? ClosingTime { get; set; }
    
    [DisplayName("Teslimat Süresi")]
    public string? DeliveryTime { get; set; }

    [DisplayName("Minimum Sipariş Tutarı")]
    public float? MinimumOrderPrice { get; set; }
    
    [DisplayName("Değerlendime Puanı")]
    public float? Rating { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsApproved { get; set; }
    
}
