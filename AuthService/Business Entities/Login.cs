using Domain;

namespace TwitterCloneCompulsory.Business_Entities;

public class Login
{
    public int LoginId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime TokenExpiryTime { get; set; }
    public int AuthUserId { get; set; } // Foreign key to the AuthUser
    public AuthUser AuthUser { get; set; } 
}