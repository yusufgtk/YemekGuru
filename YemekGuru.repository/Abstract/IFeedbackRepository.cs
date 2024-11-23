using YemekGuru.entity;

namespace YemekGuru.repository.Abstract;

public interface IFeedbackRepository:IRepository<Feedback>
{
    public Task<Feedback> GetFeedbackByIdAsync(int id);
    public Task<bool> IsThereActiveFeedbackAsync(string userName);
    public Task<List<Feedback>> GetFeedbacksByUserNameAsync(string userName);
    public Task<List<Feedback>> GetFeedbacksAsync();
    public Task<List<Feedback>> GetWaitFeedbacksAsync();
    public Task<int> WaitFeedbacksCountAsync();
}