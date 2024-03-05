namespace TwitterCloneCompulsory.Business_Entities;

/// <summary>
/// Minimal user entity
/// </summary>
public class AuthUser
{
    public int Id { get; set; }
    public string Username { get; set; }
    
    public virtual ICollection<Token> Tokens { get; set; } = new List<Token>();

}