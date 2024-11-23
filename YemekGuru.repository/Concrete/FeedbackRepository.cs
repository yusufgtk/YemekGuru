using Microsoft.EntityFrameworkCore;
using YemekGuru.entity;
using YemekGuru.repository.Abstract;

namespace YemekGuru.repository.Concrete;

public class FeedbackRepository : Repository<Feedback, DataContext>, IFeedbackRepository
{
    public async Task<Feedback> GetFeedbackByIdAsync(int id)
    {
        using(var context = new DataContext())
        {
            return await context.Feedbacks.FirstOrDefaultAsync(i=>i.Id==id);
        }
    }

    public async Task<List<Feedback>> GetFeedbacksAsync()
    {
        using(var context = new DataContext())
        {
            return await context.Feedbacks.ToListAsync();
        }
    }

    public async Task<List<Feedback>> GetFeedbacksByUserNameAsync(string userName)
    {
        using(var context = new DataContext())
        {
            return await context.Feedbacks.Where(i=>i.UserName==userName).OrderByDescending(i=>i.CreatedAt).Take(30).ToListAsync();
        }
    }

    public async Task<List<Feedback>> GetWaitFeedbacksAsync()
    {
        using(var context = new DataContext())
        {
            return await context.Feedbacks.Where(i=>i.Status==false).OrderBy(i=>i.CreatedAt).Take(30).ToListAsync();
        }
    }

    public async Task<bool> IsThereActiveFeedbackAsync(string userName)
    {
        using(var context = new DataContext())
        {
            var result = await context.Feedbacks.Where(i=>i.UserName==userName&&i.Status==false).ToListAsync();
            if(result.Count()>0)
            {
                return true;
            }
            return false;
        }
    }

    public async Task<int> WaitFeedbacksCountAsync()
    {
        using(var context = new DataContext())
        {
            return await context.Feedbacks.Where(i=>i.Status==false).CountAsync();
        }
    }
}
