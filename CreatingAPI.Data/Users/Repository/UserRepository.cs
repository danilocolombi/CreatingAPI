using CreatingAPI.Data.Core.Context;
using CreatingAPI.Domain.Users;
using CreatingAPI.Domain.Users.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CreatingAPI.Data.Users.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<int> CreateAsync(User user)
        {
            try
            {
                var userCreated = await _dataContext.AddAsync(user);

                if (userCreated == null) return 0;

                await _dataContext.SaveChangesAsync();

                return userCreated.Entity.Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<bool> DeleteAsync(User user)
        {
            _dataContext.Remove(user);

            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<User> GetAsync(int id)
        {
            var user = await _dataContext.Users.FindAsync(id);

            return user;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            _dataContext.Update(user);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> IsEmailAlreadyRegistered(string email)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email.Address == email);

            return user == null ? false : true;
        }
    }
}
