

using YemekGuru.business.Abstract;
using YemekGuru.entity;
using YemekGuru.repository.Abstract;

namespace YemekGuru.business.Concrete;

public class CommentManager : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    public CommentManager(ICommentRepository commentRepository)
    {
        _commentRepository=commentRepository;
    }

    public async Task<List<Comment>> Admin_GetComments30Async()
    {
        return await _commentRepository.Admin_GetComments30Async();
    }

    public async Task<int> Admin_TotalCommentsCountAsync()
    {
        return await _commentRepository.Admin_TotalCommentsCountAsync();
    }

    public async Task<int> Admin_TotalWaitapprovedCommentsCountAsync()
    {
        return await _commentRepository.Admin_TotalWaitapprovedCommentsCountAsync();
    }

    public async Task<bool> CreateAsync(Comment entity)
    {
        return await _commentRepository.CreateAsync(entity);
    }

    public async Task<bool> DeleteAsync(Comment entity)
    {
        return await _commentRepository.DeleteAsync(entity);
    }

    public async Task<List<Comment>> GetAllAsync()
    {
        return await _commentRepository.GetAllAsync();
    }

    public async Task<Comment> GetCommentByIdAsync(int id)
    {
        return await _commentRepository.GetCommentByIdAsync(id);
    }

    public async Task<Comment> GetCommentByUserNameAsync(string userName)
    {
        return await _commentRepository.GetCommentByUserNameAsync(userName);
    }

    public async Task<List<Comment>> GetCommentsByRestaurantIdAsync(int restaurantId)
    {
        return await _commentRepository.GetCommentsByRestaurantIdAsync(restaurantId);
    }

    public async Task<int> TotalCommentsCountByRestaurantIdAsync(int restaurantId)
    {
        return await _commentRepository.TotalCommentsCountByRestaurantIdAsync(restaurantId);
    }

    public async Task<bool> UpdateAsync(Comment entity)
    {
        return await _commentRepository.UpdateAsync(entity);
    }
}