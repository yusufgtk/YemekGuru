using System.ComponentModel.DataAnnotations;

namespace YemekGuru.webapp.Models;

public class AddCommnetModel
{
    public int? RestaurantId { get; set; }
    public int? OrderId { get; set; }
    
    [Required(ErrorMessage ="Bu alanı boş bırakamazsınız!")]
    [StringLength(200,ErrorMessage ="En az 5 en fazla 200 karater olabilir!",MinimumLength =5)]
    public string? Description { get; set; }
}