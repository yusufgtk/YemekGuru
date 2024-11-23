
namespace YemekGuru.webapp.Models;

public class OrderManagerViewModel
{
    public int TotalOrdersCount { get; set; }
    public int TotalCompletedOrdersCount { get; set; }
    public int TotalCanceledOrdersCount { get; set; }
    public int ActiveOrdersCount { get; set; }
    public int SetOutOrdersCount { get; set; }
    public CategoryModel? TopSellingCategory { get; set; }
    public int TopSellingCategoryCount { get; set; }
    public List<KeyValuePair<int,int>>? TopSellingCategoryList { get; set; }
    public OrderModel? OrderModel { get; set; }
}