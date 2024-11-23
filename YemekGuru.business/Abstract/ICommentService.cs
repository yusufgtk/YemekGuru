using YemekGuru.entity;

namespace YemekGuru.business.Abstract;

public interface ICommentService
{
    public Task<bool> CreateAsync(Comment entity);
    public Task<bool> DeleteAsync(Comment entity);
    public Task<bool> UpdateAsync(Comment entity);
    public Task<List<Comment>> GetAllAsync();
    public Task<Comment> GetCommentByIdAsync(int id);
    public Task<Comment> GetCommentByUserNameAsync(string userName);
    public Task<List<Comment>> GetCommentsByRestaurantIdAsync(int restaurantId);
    public Task<int> Admin_TotalCommentsCountAsync();
    public Task<int> TotalCommentsCountByRestaurantIdAsync(int restaurantId);
    public Task<int> Admin_TotalWaitapprovedCommentsCountAsync();
    public Task<List<Comment>> Admin_GetComments30Async();
    
}