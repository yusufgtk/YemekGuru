using YemekGuru.entity;

namespace YemekGuru.business.Abstract;

public interface IFeedbackService
{
    public Task<bool> CreateAsync(Feedback entity);
    public Task<bool> DeleteAsync(Feedback entity);
    public Task<bool> UpdateAsync(Feedback entity);
    public Task<List<Feedback>> GetAllAsync();
    public Task<Feedback> GetFeedbackByIdAsync(int id);
    public Task<bool> IsThereActiveFeedbackAsync(string userName);
    public Task<List<Feedback>> GetFeedbacksByUserNameAsync(string userName);
    public Task<List<Feedback>> GetFeedbacksAsync();
    public Task<List<Feedback>> GetWaitFeedbacksAsync();
    public Task<int> WaitFeedbacksCountAsync();
}

