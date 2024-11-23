namespace YemekGuru.entity;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order? Order { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int? Amount { get; set; }
    public decimal? Price { get; set; }
    public decimal? TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
}