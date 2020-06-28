using AutoMapper;
using CreatingAPI.Application.Core;
using CreatingAPI.Application.Users.Interfaces;
using CreatingAPI.Application.Users.ViewModels;
using CreatingAPI.Domain.Users;
using CreatingAPI.Domain.Users.Interfaces;
using System.Threading.Tasks;

namespace CreatingAPI.Application.Users
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserAppService(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<ResultResponse> CreateUser(UserCreationViewModel userCreationViewModel)
        {
            var user = _mapper.Map<User>(userCreationViewModel);

            var businessResult = await _userService.CreateUser(user);

            return new ResultResponse(businessResult, Operation.CREATE);
        }

        public async Task<ResultResponse> ChangePassword(int idUser, string newPassword)
        {
            var businessResult = await _userService.ChangePassword(idUser, newPassword);

            return new ResultResponse(businessResult, Operation.UPDATE);
        }

        public async Task<ResultResponse> DeleteUser(int idUser)
        {
            var businessResult = await _userService.DeleteUser(idUser);

            return new ResultResponse(businessResult, Operation.DELETE);
        }

        public async Task<UserViewModel> GetUser(int idUser)
        {
            var user = await _userService.GetUser(idUser);

            var userViewModel = _mapper.Map<UserViewModel>(user);

            return userViewModel;
        }
    }
}
