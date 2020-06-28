using CreatingAPI.Application.Core;
using CreatingAPI.Application.Users.ViewModels;
using System.Threading.Tasks;

namespace CreatingAPI.Application.Users.Interfaces
{
    public interface IUserAppService
    {
        Task<ResultResponse> CreateUser(UserCreationViewModel userCreationViewModel);
        Task<ResultResponse> ChangePassword(int idUser, string newPassword);
        Task<ResultResponse> DeleteUser(int idUser);
    }
}
