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

    public string Firstname { get; set; }
    public string Lastname { get; set; }

    public int Age { get; set; }
    

    /// <summary>
    /// The data the user is created
    /// </summary>
    public DateTime DateCreated { get; set; }
    
}