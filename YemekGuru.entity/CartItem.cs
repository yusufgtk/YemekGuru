namespace YemekGuru.entity;

public class CartItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int CartId { get; set; }
    public int? Amount { get; set; }
    public decimal? Price { get; set; }
    public decimal? TotalPrice { get; set; }
    
}