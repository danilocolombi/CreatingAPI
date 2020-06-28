using CreatingAPI.Domain.Core;
using System.Threading.Tasks;

namespace CreatingAPI.Domain.Users.Interfaces
{
    public interface IUserService
    {
        Task<ResultResponseBusiness> CreateUser(User user);
        Task<ResultResponseBusiness> ChangePassword(int id, string newPassword);
        Task<ResultResponseBusiness> DeleteUser(int id);
        Task<User> GetUser(int id);
    }
}
