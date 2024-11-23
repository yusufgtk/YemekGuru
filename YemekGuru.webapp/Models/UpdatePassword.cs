using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YemekGuru.webapp.Models;

public class UpdatePassword
{
    [Required]
    public string? UserId { get; set; }

    [Required(ErrorMessage = "Zorunlu alan!")]
    [DataType(DataType.Password)]
    [DisplayName("Şifre")]
    public string? CurrentPassword { get; set; }

    [Required(ErrorMessage = "Zorunlu alan!")]
    [DataType(DataType.Password,ErrorMessage ="en az 6 karakter olmalı ve içerisinde sayı bulundurmalıdır!")]
    [DisplayName("Yeni Şifre")]
    public string? NewPassword { get; set; }

    [Required(ErrorMessage = "Zorunlu alan!")]
    [Compare(nameof(NewPassword),ErrorMessage ="Şifreler bir biri ile uyuşmuyor!")]
    [DisplayName("Yeni Şifreyi Onayla")]
    public string? ConfirmNewPassword { get; set; }
}