using Domain;
using TwitterCloneCompulsory.Business_Entities;

namespace TwitterCloneCompulsory.Interfaces;

    public interface IAuthRepo
    {
        Task<Login> GetUsersByUsernameAsync(string username);

        Task<Login> RegisterUserAsync(Login login);

        Task<Login> GetUsersByUserId(int userId);

        public void Rebuild();

        Task SaveTokenAsync(Token token);

        Task<bool> IsTokenActiveAsync(int UserId);

        Task DeleteLoginAsync(int userId);

        Task DeleteTokenAsync(int userId);
}
