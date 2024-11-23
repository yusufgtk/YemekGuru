using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YemekGuru.webapp.Models;

public class ComplaintModel
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public int OrderId { get; set; }
    public OrderModel? OrderModel { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [StringLength(maximumLength:50,ErrorMessage ="En fazla 50 kararkter olabilir!")]
    [DisplayName("Konu")]
    public string? Subject  { get; set; }

    [Required(ErrorMessage ="Zorunlu alan!")]
    [StringLength(maximumLength:150,ErrorMessage ="En fazla 150 kararkter olabilir!")]
    [DisplayName("Açıklama")]
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdateAt { get; set; }
    public bool Status { get; set; } //false: inceleniyor true:Çözüldü!
}