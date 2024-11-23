using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YemekGuru.webapp.Models;

public class OrderModel
{   
    [DisplayName("Sipariş Numarası")]
    public int Id { get; set; }

    [Required(ErrorMessage ="Restauran seçmek zorundasın!")]
    public int RestaurantId { get; set; }


    [DisplayName("Sipariş Adı")]
    public string? Name { get; set; }

    [DisplayName("Restaurant Adı")]
    public string? RestaurantName { get; set; }

    [StringLength(100,ErrorMessage ="En fazla 100 karakter olabilir!")]
    [DisplayName("Sipariş Notu")]
    public string? Note { get; set; }

    [DisplayName("Ürün Çeşidi Sayısı")]
    public int ProductTypesNumber { get; set; }

    [DisplayName("Sipariş Toplam Tutarı")]
    public decimal? TotalPrice { get; set; }

    [DisplayName("Sipariş Tarihi")]
    public DateTime? CreatedAt { get; set; }

    [DisplayName("Sipariş Tarihi")]
    public DateTime? DeliveryDate { get; set; }

    [DisplayName("Sipariş Durumu")]
    public int Status { get; set; }

    [DisplayName("Ülke")]
    public string? Country { get; set; }

    [DisplayName("Şehir")]
    public int? CityId { get; set; }
    public string? CityName  { get; set; }

    [DisplayName("İlçe")]
    public int? DistrictId { get; set; }
    public string? DistrictName { get; set; }

    [DisplayName("Address")]
    public string? Address { get; set; }

    [DisplayName("Ad Soyad")]
    public string? FullName { get; set; }

    public string? UserId { get; set; }

    [DisplayName("Telefon Numarası")]
    public string? PhoneNumber { get; set; }


    public List<OrderItemModel>? OrderItemModels { get; set; }
}