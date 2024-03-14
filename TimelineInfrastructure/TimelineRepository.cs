using Domain;
using Microsoft.EntityFrameworkCore;
using TimelineInfrastructure.Interfaces;

namespace TimelineInfrastructure;

public class TimelineRepository : ITimelineRepo
{
    private readonly TimelineDbContext _context;

    public TimelineRepository(TimelineDbContext context)
    {
        _context = context;
    }

    public async Task AddPostToTimelineAsync(int postId)
    {
        var timelineEntry = new Timeline { PostId = postId };
        _context.Timelines.Add(timelineEntry);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<int>> GetTimelinePostIdsAsync()
    {
        return await _context.Timelines
            .Select(t => t.PostId)
            .ToListAsync();
    }

    public void Rebuild()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    public async Task RemovePostFromTimelineAsync(int postId)
    {
        var timelineEntry = await _context.Timelines
            .FirstOrDefaultAsync(t => t.PostId == postId);
        if (timelineEntry != null)
        {
            _context.Timelines.Remove(timelineEntry);
            await _context.SaveChangesAsync();
        }
    }
}