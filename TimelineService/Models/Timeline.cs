using PostService.Models;

namespace TimelineService.Models;

public class Timeline
{
    /// <summary>
    /// The id of the timeline
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The list of the posts on the timeline
    /// </summary>
    /// <returns></returns>
    public List<Post> Posts { get; set; } = new List<Post>();
    
    public int UserId { get; set; }
}