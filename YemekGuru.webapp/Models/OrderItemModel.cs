using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YemekGuru.webapp.Models;

public class OrderItemModel
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public OrderModel? OrderModel { get; set; }
    public int ProductId { get; set; }
    public ProductModel? ProductModel { get; set; }

    [DisplayName("Adet")]
    public int? Amount { get; set; }

    [DisplayName("Fiyat")]
    public decimal? Price { get; set; }

    [DisplayName("Ürünün Toplam Fiyatı")]
    public decimal? TotalPrice { get; set; }

}