using Domain;
using TwitterCloneCompulsory.Business_Entities;

namespace TwitterCloneCompulsory.Interfaces;

    public interface IAuthRepo
    {
        Task<Login> GetUsersByUsernameAsync(string username);

        Task<Login> RegisterUserAsync(Login login);

        Task<Login> GetUsersByUserId(int userId);

        public void Rebuild();

        Task DeleteLoginAsync(int userId);

}
