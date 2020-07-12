using CreatingAPI.Domain.Core;
using System.Threading.Tasks;

namespace CreatingAPI.Domain.Users.Interfaces
{
    public interface IUserService
    {
        Task<ValidationResult> CreateUser(User user);
        Task<ValidationResult> ChangePassword(int id, string newPassword);
        Task<ValidationResult> DeleteUser(int id);
        Task<User> GetUser(int id);
    }
}
