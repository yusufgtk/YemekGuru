using Microsoft.EntityFrameworkCore;
using YemekGuru.entity;
using YemekGuru.repository.Abstract;

namespace YemekGuru.repository.Concrete;

public class CommentRepository : Repository<Comment, DataContext>, ICommentRepository
{
    public async Task<List<Comment>> Admin_GetComments30Async()
    {
        using(var context = new DataContext())
        {
            return await context.Comments.Include(r=>r.Restaurant).Where(i=>i.IsApproved==false).OrderByDescending(i=>i.CreatedAt).Take(30).ToListAsync();
        }
    }

    public async Task<int> Admin_TotalCommentsCountAsync()
    {
        using(var context = new DataContext())
        {
            return await context.Comments.CountAsync();
        }
    }

    public async Task<int> Admin_TotalWaitapprovedCommentsCountAsync()
    {
        using(var context = new DataContext())
        {
            return await context.Comments.Where(i=>i.IsApproved==false).CountAsync();
        }
    }

    public async Task<Comment> GetCommentByIdAsync(int id)
    {
        using(var context = new DataContext())
        {
            return await context.Comments.FirstOrDefaultAsync(i=>i.Id==id);
        }
    }

    public async Task<Comment> GetCommentByUserNameAsync(string userName)
    {
        using(var context = new DataContext())
        {
            return await context.Comments.Include(r=>r.Restaurant).FirstOrDefaultAsync(i=>i.UserName==userName);
        }
    }

    public async Task<List<Comment>> GetCommentsByRestaurantIdAsync(int restaurantId)
    {
        using(var context = new DataContext())
        {
            return await context.Comments.Where(i=>i.RestaurantId==restaurantId&&i.IsApproved==true).OrderByDescending(i=>i.CreatedAt).Take(30).ToListAsync();
        }
    }

    public async Task<int> TotalCommentsCountByRestaurantIdAsync(int restaurantId)
    {
        using(var context = new DataContext())
        {
            return await context.Comments.Where(i=>i.RestaurantId==restaurantId&&i.IsApproved==true).CountAsync();
        }
    }
}
