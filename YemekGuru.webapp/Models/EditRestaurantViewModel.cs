using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YemekGuru.webapp.Models;

public class EditRestaurantViewModel
{
    [Required(ErrorMessage ="Zorunlu alan!")]
    [StringLength(maximumLength:40,ErrorMessage ="En az 2 en fazla 40 karakter olabilir!",MinimumLength =2)]
    [DisplayName("Restaurant Adı")]
    public string? Name { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [StringLength(maximumLength:100,ErrorMessage ="En az 10 en fazla 40 karakter olabilir!",MinimumLength =10)]
    [DisplayName("Restaurant Tanımı 10-100")]
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [DisplayName("Şehir")]
    public int? CityId { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [DisplayName("İlçe")]
    public int? DistrictId { get; set; }


    [Required(ErrorMessage ="Zorunlu alan!")]
    [StringLength(maximumLength:100,ErrorMessage ="En az 10 en fazla 40 karakter olabilir!",MinimumLength =5)]
    [DisplayName("Adres")]
    public string? Address { get; set; }


    [Required(ErrorMessage ="Zorunlu alan!")]
    [DataType(DataType.PhoneNumber,ErrorMessage ="+90 ***-****-**-**")]
    [DisplayName("Telefon Numarası")]
    public string? PhoneNumber { get; set; }


    [Required(ErrorMessage ="Zorunlu alan!")]
    [DataType(DataType.EmailAddress,ErrorMessage ="Mail edresi olmalıdır!")]
    [DisplayName("Email Adresi")]
    public string? EmailAdress { get; set; }


    [Required(ErrorMessage ="Zorunlu alan!")]
    [DisplayName("Açılış Saati")]
    public string? OpeningTime { get; set; }


    [Required(ErrorMessage ="Zorunlu alan!")]
    [DisplayName("Kapanış Saati")]
    public string? ClosingTime { get; set; }
    

    [Required(ErrorMessage ="Zorunlu alan!")]
    [Range(minimum:1,maximum:120,ErrorMessage ="En az 1 en fazla 120 dk olabilir.")]
    [DisplayName("Tahmini Teslimat Süresi")]
    public int? DeliveryTime { get; set; }


    [Required(ErrorMessage ="Zorunlu alan!")]
    [Range(minimum:0,maximum:10000,ErrorMessage ="En az 0 enfazla 10000 TL olabilir")]
    [DisplayName("Minimum Sipariş Tutarı")]
    public float? MinimumOrderPrice { get; set; }
    

}
