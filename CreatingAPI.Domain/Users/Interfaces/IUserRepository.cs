using System.Threading.Tasks;

namespace CreatingAPI.Domain.Users.Interfaces
{
    public interface IUserRepository
    {
        Task<int> CreateAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(User user);
        Task<User> GetAsync(int id);
        Task<bool> IsEmailAlreadyRegistered(string email);
        Task<User> GetUserByIdAsync(int id);
    }
}
