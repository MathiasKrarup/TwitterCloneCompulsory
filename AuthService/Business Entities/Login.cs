using Domain;

namespace TwitterCloneCompulsory.Business_Entities;

public class Login
{
   public int Id { get; set; }
   
   public int UserId { get; set; } 
   
   public string UserName { get; set; }
   
   public string PasswordHash { get; set; }
}