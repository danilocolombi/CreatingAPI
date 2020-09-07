using CreatingAPI.Application.Activities.ViewModels;
using CreatingAPI.Application.Core;
using CreatingAPI.Application.Users.ViewModels;
using CreatingAPI.Domain.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreatingAPI.Application.Users.Interfaces
{
    public interface IUserAppService
    {
        Task<ResultResponse> CreateAsync(UserCreationViewModel userCreationViewModel);
        Task<ResultResponse> ChangePasswordAsync(int idUser, string newPassword);
        Task<ResultResponse> DeleteAsync(int idUser);
        Task<IEnumerable<MyActivitiyViewModel>> GetUserActivitiesAsync(int idUser);
    }
}
