using YemekGuru.entity;

namespace YemekGuru.repository.Abstract;

public interface ICommentRepository:IRepository<Comment>
{
    public Task<Comment> GetCommentByIdAsync(int id);
    public Task<Comment> GetCommentByUserNameAsync(string userName);
    public Task<List<Comment>> GetCommentsByRestaurantIdAsync(int restaurantId);
    public Task<int> Admin_TotalCommentsCountAsync();
    public Task<int> TotalCommentsCountByRestaurantIdAsync(int restaurantId);
    public Task<int> Admin_TotalWaitapprovedCommentsCountAsync();
    public Task<List<Comment>> Admin_GetComments30Async();
    
    
}