using Domain;

namespace TwitterCloneCompulsory.Business_Entities;

public class Token
{
        /// <summary>
        /// The id of the token
        /// </summary>
        public int TokenId { get; set; }
        /// <summary>
        /// Foreign key to the authUser table
        /// </summary>
        public int AuthUserId { get; set; } 
        /// <summary>
        /// The expiry time of the token
        /// </summary>
        public DateTime TokenExpiryTime { get; set; }
        
        /// <summary>
        /// Boolean that checks if the token is active
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Navigation property for the associated AuthUser
        /// </summary>
        public AuthUser AuthUser { get; set; } // Navigation property for the associated AuthUser
}