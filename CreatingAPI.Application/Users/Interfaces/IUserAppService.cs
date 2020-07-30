using CreatingAPI.Application.Core;
using CreatingAPI.Application.Users.ViewModels;
using System.Threading.Tasks;

namespace CreatingAPI.Application.Users.Interfaces
{
    public interface IUserAppService
    {
        Task<ResultResponse> CreateAsync(UserCreationViewModel userCreationViewModel);
        Task<ResultResponse> ChangePasswordAsync(int idUser, string newPassword);
        Task<ResultResponse> DeleteAsync(int idUser);
    }
}
