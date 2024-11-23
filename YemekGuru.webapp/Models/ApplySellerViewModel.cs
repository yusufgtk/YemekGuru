using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YemekGuru.webapp.Models;

public class ApplySellerViewModel
{
    public int Id { get; set; }
    public string? UserId { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [StringLength(maximumLength:40,ErrorMessage ="en az 2 en fazla 40 karakter olabilir!",MinimumLength =2)]
    [DisplayName("Ad")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [StringLength(maximumLength:40,ErrorMessage ="en az 2 en fazla 40 karakter olabilir!",MinimumLength =2)]
    [DisplayName("Soyad")]
    public string? LastName { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "Geçersiz TCKN")] 
    [DisplayName("TCKN")]
    public string? TCKN { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [DisplayName("Doğum Tarihi")]
    public string? BirthDay { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [StringLength(maximumLength:50,ErrorMessage ="en az 3 en fazla 50 karakter olabilir!",MinimumLength =3)]
    [DisplayName("Restaurant Adı")]
    public string? RestaurantName { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [DataType(DataType.PhoneNumber,ErrorMessage ="11 haneli olmalıdır!")]
    [DisplayName("Telefon Numarası")]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [DataType(DataType.PhoneNumber,ErrorMessage ="11 haneli olmalıdır!")]
    [DisplayName("Restaurant Telefon Numarası")]
    public string? RestaurantPhoneNumber { get; set; }

    

    [Required(ErrorMessage ="Zorunlu alan!")]
    [Range(0,10000,ErrorMessage ="Minimum sipariş tutarı en az 0 TL en fazla 10.000 TL olabilir!")]
    [DisplayName("Minimum Sipariş Tutarı")]
    public int? MinOrderPrice { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [DisplayName("Restaurantın Bulunduğu Şehir")]
    public int? CityId { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [DisplayName("Restaurantın Bulunduğu İlçe")]
    public int? DistrictId { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [DisplayName("Restaurantın Bulunduğu Adres")]
    public string? Address { get; set; }

}