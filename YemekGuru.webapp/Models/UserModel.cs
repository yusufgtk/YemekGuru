using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YemekGuru.webapp.Models;

public class UserModel
{
    public string? Id { get; set; }
    public int? RestaurantId { get; set; }

    [DisplayName("Kullanıcı Adı")]
    public string? UserName { get; set; }
    public string? ImageUrl { get; set; }

    [DisplayName("Ad")]
    public string? FirstName { get; set; }

    [DisplayName("Soyad")]
    public string? LastName { get; set; }

    [DisplayName("TCKN")]
    public string? TCKN { get; set; }

    [DisplayName("Email")]
    public string? EmailAddress { get; set; }

    [DisplayName(" Telefon Numarası")]
    public string? PhoneNumber { get; set; }

    [DataType(DataType.DateTime,ErrorMessage ="Gün/Ay/Yıl")]
    [DisplayName("Dağum Tarihi")]
    public DateTime? BirthDay { get; set; }

    [DisplayName("GP (GuruPuan)")]
    public int? GuruPuan { get; set; }


    public int? AddressId { get; set; }

    [DisplayName("Aktif mi?")]
    public bool? IsActive { get; set; }
}