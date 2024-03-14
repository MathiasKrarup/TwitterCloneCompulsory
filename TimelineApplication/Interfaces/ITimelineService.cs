using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimelineApplication.Interfaces
{
    public interface ITimelineService
    {
        /// <summary>
        /// Adds a post to the timeline
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        Task AddPostToTimelineAsync(int postId);
        /// <summary>
        /// Get all post ids on the timeline.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<int>> GetTimelinePostIdsAsync();
        /// <summary>
        /// Removes a post from the timeline
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        Task RemovePostFromTimelineAsync(int postId);
        
        /// <summary>
        /// Rebuilds the database
        /// </summary>
        public void Rebuild();

    }
}
