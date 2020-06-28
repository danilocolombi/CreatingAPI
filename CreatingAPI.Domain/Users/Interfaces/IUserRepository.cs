using System.Threading.Tasks;

namespace CreatingAPI.Domain.Users.Interfaces
{
    public interface IUserRepository
    {
        Task<int> CreateUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(User user);
        Task<User> GetUser(int id);
        Task<bool> IsEmailAlreadyRegistered(string email);
    }
}
