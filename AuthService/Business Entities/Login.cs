using Domain;

namespace TwitterCloneCompulsory.Business_Entities;

public class Login
{
    // Primary Key, foreign key reference to the User entity.
    /// <summary>
    /// Primary key
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// The valid JWT token for the logged in user
    /// </summary>
    public string Token { get; set; }
    
    /// <summary>
    /// The expiry time of the JWT Token
    /// </summary>
    public DateTime TokenExpiryTime { get; set; }
    
    public User User { get; set; }
}