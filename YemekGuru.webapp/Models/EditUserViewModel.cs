using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YemekGuru.webapp.Models;

public class EditUserViewModel
{
    public string? Id { get; set; }

    [DisplayName("Kullanıcı Adı")]
    public string? UserName { get; set; }
    public string? ImageUrl { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [StringLength(30,ErrorMessage ="En az 3 en fazla 30 karakter olmalıdır!",MinimumLength =3)]
    [DisplayName("Ad")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [StringLength(30,ErrorMessage ="En az 2 en fazla 30 karakter olmalıdır!",MinimumLength =2)]
    [DisplayName("Soyad")]
    public string? LastName { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [DataType(DataType.EmailAddress,ErrorMessage ="Email formatında olmalıdır!")]
    [DisplayName("Email")]
    public string? EmailAddress { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [DataType(DataType.PhoneNumber,ErrorMessage ="+90 ***-***-**-** 10 haneli olamalıdır!")]
    [DisplayName(" Telefon Numarası")]
    public string? PhoneNumber { get; set; }

    [DisplayName("Dağum Tarihi")]
    public DateTime? BirthDay { get; set; }


}