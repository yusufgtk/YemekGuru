namespace YemekGuru.webapp.Models;

public class SellerIndexViewModel
{
    public RestaurantModel? RestaurantModel { get; set; }
    public int TotalActiveOrdersCount  { get; set; }
    public int TotalSetOutOrdersCount  { get; set; }
    public int TotalComplatedOrdersCount  { get; set; }
    public int TotalCanceledOrdersOrdersCount  { get; set; }
    public int TotalOrdersCount  { get; set; }
    public int ProductsCount  { get; set; }
    public int ActiveProductsCount  { get; set; }
    public int CommentsCount  { get; set; }
    
    // public List<CategoryModel>? CategoryModels { get; set; }
    

}