using Domain;
using TwitterCloneCompulsory.Business_Entities;

namespace TwitterCloneCompulsory.Interfaces;

    public interface IAuthRepo
    {
        Task<Login> GetUsersByUsernameAsync(string username);
        public void Rebuild();

    }
