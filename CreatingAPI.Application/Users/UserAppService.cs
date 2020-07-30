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

        public async Task<ResultResponse> CreateAsync(UserCreationViewModel userCreationViewModel)
        {
            var user = _mapper.Map<User>(userCreationViewModel);

            var businessResult = await _userService.CreateAsync(user);

            return new ResultResponse(businessResult, Operation.CREATE);
        }

        public async Task<ResultResponse> ChangePasswordAsync(int idUser, string newPassword)
        {
            var businessResult = await _userService.ChangePasswordAsync(idUser, newPassword);

            return new ResultResponse(businessResult, Operation.UPDATE);
        }

        public async Task<ResultResponse> DeleteAsync(int idUser)
        {
            var businessResult = await _userService.DeleteAsync(idUser);

            return new ResultResponse(businessResult, Operation.DELETE);
        }

        public async Task<UserViewModel> GetAsync(int idUser)
        {
            var user = await _userService.GetAsync(idUser);

            var userViewModel = _mapper.Map<UserViewModel>(user);

            return userViewModel;
        }
    }
}
