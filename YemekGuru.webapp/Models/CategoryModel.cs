using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YemekGuru.webapp.Models;

public class CategoryModel
{
    public int Id { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [StringLength(40,ErrorMessage ="En az 2 en fazla 40 karakter olabilir!",MinimumLength =2)]
    [DisplayName("Kategori Adı")]
    public string? Name { get; set; }

    [StringLength(100,ErrorMessage ="En az 2 en fazla 100 karakter olabilir!",MinimumLength =2)]
    [DisplayName("Kategori Açıklaması")]
    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}