using TwitterCloneCompulsory.Business_Entities;
using TwitterCloneCompulsory.Interfaces;

namespace TwitterCloneCompulsory.Repo;

public class AuthRepo : IAuthenticationRepo
{
    public Login GetUserByToken(string token)
    {
        throw new NotImplementedException();
    }

    public Login GetUserByUsername(string username)
    {
        throw new NotImplementedException();
    }


}