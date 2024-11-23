namespace YemekGuru.webapp.Models;

public class ProductModel
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public RestaurantModel? RestaurantModel { get; set; }
    public int CategoryId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Content { get; set; }
    public float? Calorie { get; set; }
    public string? ImageUrl { get; set; }
    public decimal? PreviousPrice { get; set; }
    public decimal? Price { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsApproved { get; set; }
    public DateTime? CreatedTime { get; set; }
    public DateTime? UpdateTime { get; set; }

}