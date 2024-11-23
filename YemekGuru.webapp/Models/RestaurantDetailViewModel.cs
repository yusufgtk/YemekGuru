namespace YemekGuru.webapp.Models;

public class RestaurantDetailViewModel
{
    public RestaurantModel? RestaurantModel { get; set; }
    public List<ProductModel>? ProductModels { get; set; }
    public List<CommentModel>? CommentModels { get; set; }
    public int TotalCommentCount { get; set; }
}