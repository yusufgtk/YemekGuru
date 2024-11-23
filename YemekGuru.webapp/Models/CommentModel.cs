namespace YemekGuru.webapp.Models;

public class CommentModel
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int RestaurantId { get; set; }
    public RestaurantModel? RestaurantModel { get; set; }
    public string UserName{ get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
}