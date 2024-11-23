namespace YemekGuru.webapp.Models;

public class CartModel
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public int? RestaurantId { get; set; }
    public int? Amount { get; set; }
    public decimal? TotalPrice { get; set; }
}