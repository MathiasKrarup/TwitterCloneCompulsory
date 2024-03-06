using Domain;

namespace TwitterCloneCompulsory.Business_Entities;

public class Token
{
        /// <summary>
        /// The id of the token
        /// </summary>
        public int TokenId { get; set; }
        

        /// <summary>
        /// The expiry time of the token
        /// </summary>
        public DateTime TokenExpiryTime { get; set; }

        /// <summary>
        /// Boolean that checks if the token is active
        /// </summary>
        public bool IsActive { get; set; }

}
