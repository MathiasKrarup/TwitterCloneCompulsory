namespace UserService.Models;

public class User
{
    /// <summary>
    /// Id of the user
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// The firstname of the user 
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// The lastname of the user
    /// </summary>
    public string LastName { get; set; }
    
    /// <summary>
    /// The e-mail of the user
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// The username of the user
    /// </summary>
    public string Username { get; set; }
    
    /// <summary>
    /// The data the user is created
    /// </summary>
    public DateTime DateCreated { get; set; }
}