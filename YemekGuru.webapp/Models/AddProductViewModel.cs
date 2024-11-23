using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YemekGuru.webapp.Models;


public class AddProductViewModel
{
    
    // public int RestaurantId { get; set; }

    [Required(ErrorMessage ="Zorunlu alan")]
    [DisplayName("Kategoriler")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage ="Zorunlu alan")]
    [StringLength(maximumLength:50,ErrorMessage ="En az 2 en fazla 50 karaterden oluşabilir!",MinimumLength =2)]
    [DisplayName("Yemeğin Adı")]
    public string? Name { get; set; }

    [StringLength(maximumLength:100,ErrorMessage ="En az 2 en fazla 50 karaterden oluşabilir!",MinimumLength =0)]
    [DisplayName("Açıklama")]
    public string? Description { get; set; }

    [Required(ErrorMessage ="Zorunlu alan")]
    [StringLength(maximumLength:100,ErrorMessage ="En az 2 en fazla 100 karaterden oluşabilir!",MinimumLength =2)]
    [DisplayName("Yemeğin İçeriği")]
    public string? Content { get; set; }

    [Required(ErrorMessage ="Zorunlu alan")]
    [Range(0,99999,ErrorMessage ="Geçerli bir değer giriniz!")]
    [DisplayName("Kalori Miktarı")]
    public float? Calorie { get; set; }

    [Required(ErrorMessage ="Zorunlu alan")]
    [Range(0.1,99999.9,ErrorMessage ="Geçerli bir değer giriniz!")]
    [DisplayName("Fiyat")]
    public decimal? Price { get; set; }
}