using TimelineApplication.Interfaces;
using TimelineInfrastructure.Interfaces;

namespace TimelineApplication;

public class TimelineService : ITimelineService
{
    private readonly ITimelineRepo _timelineRepo;

    public TimelineService(ITimelineRepo timelineRepo)
    {
        _timelineRepo = timelineRepo;
    }

    public async Task AddPostToTimelineAsync(int postId)
    {
        await _timelineRepo.AddPostToTimelineAsync(postId);
    }

    public async Task<IEnumerable<int>> GetTimelinePostIdsAsync()
    {
        return await _timelineRepo.GetTimelinePostIdsAsync();
    }

    public void Rebuild()
    {
        _timelineRepo.Rebuild();
    }

    public async Task RemovePostFromTimelineAsync(int postId)
    {
        await _timelineRepo.RemovePostFromTimelineAsync(postId);
    }
}