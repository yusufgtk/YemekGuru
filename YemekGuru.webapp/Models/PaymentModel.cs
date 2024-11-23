using System.ComponentModel.DataAnnotations;

namespace YemekGuru.webapp.Models;

public class PaymentModel
{
    [Required(ErrorMessage ="Zorunlu alan!")]
    [StringLength(16,ErrorMessage ="16 karakterden oluşacak boşluk bırakmayın!",MinimumLength =16)]
    public string? CardNumber { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    public string? CardName { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [Range(2024,2075,ErrorMessage ="En az 1 en fazla 31 girilebilir!")]
    public int? Year { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [Range(1,31,ErrorMessage ="En az 1 en fazla 31 girilebilir!")]
    public int? Month { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    public int? CVV { get; set; }
}