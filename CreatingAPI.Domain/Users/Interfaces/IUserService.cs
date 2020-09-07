using CreatingAPI.Domain.Core;
using System.Threading.Tasks;

namespace CreatingAPI.Domain.Users.Interfaces
{
    public interface IUserService
    {
        Task<ValidationResult> CreateAsync(User user);
        Task<ValidationResult> ChangePasswordAsync(int id, string newPassword);
        Task<ValidationResult> DeleteAsync(int id);
        Task<User> GetAsync(int id);
        Task<User> GetUserByIdAsync(int userId);
    }
}
