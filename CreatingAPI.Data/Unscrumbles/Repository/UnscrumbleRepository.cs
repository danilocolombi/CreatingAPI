using CreatingAPI.Data.Core.Context;
using CreatingAPI.Domain.Unscrumbles;
using CreatingAPI.Domain.Unscrumbles.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CreatingAPI.Data.Unscrumbles.Repository
{
    public class UnscrumbleRepository : IUnscrumbleRepository
    {
        private readonly DataContext _dataContext;

        public UnscrumbleRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<int> CreateUnscrumble(Unscrumble unscrumble)
        {
            try
            {
                var unscrumbledCreated = await _dataContext.AddAsync(unscrumble);

                if (unscrumbledCreated == null) return 0;

                await _dataContext.SaveChangesAsync();

                return unscrumbledCreated.Entity.Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<bool> DeleteUnscrumble(Unscrumble unscrumble)
        {
            _dataContext.Remove(unscrumble);

            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<Unscrumble> GetUnscrumble(int id)
        {
            var usncrumble = await _dataContext.Unscrumbles
                                                .Where(u => u.Id == id)
                                                .Include(u => u.Exercises)
                                                .FirstOrDefaultAsync();

            return usncrumble;
        }

        public async Task<bool> UpdateUnscrumble(Unscrumble unscrumble)
        {
            var savedUnscrumbled = await GetUnscrumble(unscrumble.Id);

            if (savedUnscrumbled == null) return false;

            _dataContext.Exercises.RemoveRange(savedUnscrumbled.Exercises);

            _dataContext.Entry(savedUnscrumbled).CurrentValues.SetValues(unscrumble);

            _dataContext.Exercises.AddRange(unscrumble.Exercises);

            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
