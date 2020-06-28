using CreatingAPI.Domain.Core;
using CreatingAPI.Domain.Users.Interfaces;
using System.Threading.Tasks;

namespace CreatingAPI.Domain.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResultResponseBusiness> CreateUser(User user)
        {
            if (!user.IsValid())
                return new ResultResponseBusiness(false, user.ValidationErrors);

            if (await _userRepository.IsEmailAlreadyRegistered(user.Email.Address))
                return new ResultResponseBusiness(false, new ValidationError("This e-mail is already registered"));

            var createdUserId = await _userRepository.CreateUser(user);

            if (createdUserId <= 0)
                return new ResultResponseBusiness(false, new ValidationError("There was an error while creating the user"));

            return new ResultResponseBusiness(true);
        }

        public async Task<ResultResponseBusiness> DeleteUser(int id)
        {
            var user = await _userRepository.GetUser(id);

            if (user == null)
                return new ResultResponseBusiness(false, new ValidationError("The user wasn't found"));

            var userWasDeleted = await _userRepository.DeleteUser(user);

            if (!userWasDeleted)
                return new ResultResponseBusiness(false, new ValidationError("There was an error while deleting the user"));

            return new ResultResponseBusiness(true);
        }

        public async Task<User> GetUser(int id)
        {
            return await _userRepository.GetUser(id);
        }

        public async Task<ResultResponseBusiness> ChangePassword(int id, string newPassword)
        {
            var user = await _userRepository.GetUser(id);

            if (user == null)
                return new ResultResponseBusiness(false, new ValidationError("The user wasn't found"));

            user.SetPassword(newPassword);

            if (!user.IsValid())
                return new ResultResponseBusiness(false, user.ValidationErrors);

            var userWasUpdated = await _userRepository.UpdateUser(user);

            if (!userWasUpdated)
                return new ResultResponseBusiness(false, new ValidationError("There was an error while updating the user"));

            return new ResultResponseBusiness(true);
        }
    }
}
