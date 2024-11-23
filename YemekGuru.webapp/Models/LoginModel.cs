using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YemekGuru.webapp.Models;

public class LoginModel
{
    [Required(ErrorMessage ="Zorunlu alan!")]
    [DisplayName("Kullanıcı Adı")]
    public string? UserName { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [DataType(DataType.Password)]
    [DisplayName("Şifre")]
    public string? Password { get; set; }

}