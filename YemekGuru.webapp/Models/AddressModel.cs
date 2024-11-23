using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YemekGuru.webapp.Models;
public class AddressModel
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [StringLength(50,ErrorMessage ="En fazla 50 karkter girebilirsin!",MinimumLength =1)]
    [DisplayName("Adres Başlığı")]
    public string? Title { get; set; }

    [DisplayName("Ülke")]
    public string? Country { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [DisplayName("Şehir")]
    public int? City { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [DisplayName("İlçe")]
    public int? District { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [DisplayName("Tam Adres")]
    public string? FullAddress { get; set; }

    [DisplayName("Açıklama")]
    public string? Description { get; set; }
}