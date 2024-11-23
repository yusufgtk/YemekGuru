using YemekGuru.business.Abstract;
using YemekGuru.entity;
using YemekGuru.repository.Abstract;

namespace YemekGuru.business.Concrete;

public class FeedbackManager : IFeedbackService
{
    private readonly IFeedbackRepository _feedbackRepository;
    public FeedbackManager(IFeedbackRepository feedbackRepository)
    {
        _feedbackRepository=feedbackRepository;
    }
    public async Task<bool> CreateAsync(Feedback entity)
    {
        return await _feedbackRepository.CreateAsync(entity);
    }

    public async Task<bool> DeleteAsync(Feedback entity)
    {
        return await _feedbackRepository.DeleteAsync(entity);
    }

    public async Task<List<Feedback>> GetAllAsync()
    {
        return await _feedbackRepository.GetAllAsync();
    }

    public async Task<Feedback> GetFeedbackByIdAsync(int id)
    {
        return await _feedbackRepository.GetFeedbackByIdAsync(id);
    }

    public async Task<List<Feedback>> GetFeedbacksAsync()
    {
        return await _feedbackRepository.GetFeedbacksAsync();
    }

    public async Task<List<Feedback>> GetFeedbacksByUserNameAsync(string userName)
    {
        return await _feedbackRepository.GetFeedbacksByUserNameAsync(userName);
    }

    public async Task<List<Feedback>> GetWaitFeedbacksAsync()
    {
        return await _feedbackRepository.GetWaitFeedbacksAsync();
    }

    public async Task<bool> IsThereActiveFeedbackAsync(string userName)
    {
        return await _feedbackRepository.IsThereActiveFeedbackAsync(userName);
    }

    public async Task<bool> UpdateAsync(Feedback entity)
    {
        return await _feedbackRepository.UpdateAsync(entity);
    }

    public async Task<int> WaitFeedbacksCountAsync()
    {
        return await _feedbackRepository.WaitFeedbacksCountAsync();
    }
}
