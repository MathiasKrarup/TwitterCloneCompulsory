using System.Net.Http;
using TimelineApplication.Interfaces;
using TimelineInfrastructure.Interfaces;

namespace TimelineApplication;

public class TimelineService : ITimelineService
{
    private readonly ITimelineRepo _timelineRepo;
    private readonly HttpClient _httpClientFactory;
    private readonly string _postServiceUrl;

    public TimelineService(ITimelineRepo timelineRepo, IHttpClientFactory httpClientFactory)
    {
        _timelineRepo = timelineRepo;
        _httpClientFactory = httpClientFactory.CreateClient();
        _postServiceUrl = "https://localhost:7222";
    }

    public async Task AddPostToTimelineAsync(int postId)
    {
        var response = await _httpClientFactory.GetAsync($"{_postServiceUrl}/Post/{postId}");

        if (!response.IsSuccessStatusCode)
        {
            throw new KeyNotFoundException($"Post with the id {postId} was not found.");
        }

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