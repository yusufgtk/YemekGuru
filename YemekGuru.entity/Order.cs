namespace YemekGuru.entity;

public class Order
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public string? Name { get; set; }
    public string? RestaurantName { get; set; }
    public string? Note { get; set; }
    public int ProductTypesNumber { get; set; }
    public decimal? TotalPrice { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public int Status { get; set; } //1: hazırlanıyor 2: yola çıktı 3: teslim edildi -1: iptal edildi
    public string? Country { get; set; }
    public int? CityId { get; set; }
    public int? DistrictId { get; set; }
    public string? Address { get; set; }
    public string? FullName { get; set; }
    public string? UserId { get; set; }
    public string? PhoneNumber { get; set; }
    public int? CommentId { get; set; }
    public List<OrderItem>? OrderItems { get; set; }

}