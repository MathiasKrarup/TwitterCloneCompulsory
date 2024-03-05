namespace Domain;

public class User
{
    /// <summary>
    /// Id of the user
    /// </summary>
    public int Id { get; set; }
    
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