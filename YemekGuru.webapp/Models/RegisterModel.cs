using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YemekGuru.webapp.Models;

public class RegisterModel
{
    [Required(ErrorMessage ="Zorunlu alan!")]
    [StringLength(maximumLength:20,MinimumLength =3,ErrorMessage ="En az 3, en fazla 20 karakter olabilir!")]
    [DisplayName("Kullanıcı Adı")]
    public string? UserName { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [DataType(DataType.EmailAddress,ErrorMessage ="Email olamak zorunda!")]
    [DisplayName("E-posta")]
    public string? EmailAddress { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [DataType(DataType.Password)]
    [DisplayName("Şifre")]
    public string? Password { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [DataType(DataType.Password)]
    [Compare("Password")]
    [DisplayName("Şifre Onay")]
    public string? RePassword { get; set; }
}