using CreatingAPI.Data.Core.Context;
using CreatingAPI.Domain.Unscrambles;
using CreatingAPI.Domain.Unscrambles.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CreatingAPI.Data.Unscrumbles.Repository
{
    public class UnscrambleRepository : IUnscrambleRepository
    {
        private readonly DataContext _dataContext;

        public UnscrambleRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<int> CreateAsync(Unscramble unscramble)
        {
            try
            {
                var unscrambledCreated = await _dataContext.AddAsync(unscramble);

                if (unscrambledCreated == null) return 0;

                await _dataContext.SaveChangesAsync();

                return unscrambledCreated.Entity.Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<bool> DeleteAsync(Unscramble unscramble)
        {
            _dataContext.Remove(unscramble);

            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<Unscramble> GetAsync(int id)
        {
            var usncrumble = await _dataContext.Unscrambles
                                                .Where(u => u.Id == id)
                                                .Include(u => u.Exercises)
                                                .FirstOrDefaultAsync();

            return usncrumble;
        }

        public async Task<bool> UpdateAsync(Unscramble unscramble)
        {
            var savedUnscrambled = await GetAsync(unscramble.Id);

            if (savedUnscrambled == null) return false;

            _dataContext.Exercises.RemoveRange(savedUnscrambled.Exercises);

            _dataContext.Entry(savedUnscrambled).CurrentValues.SetValues(unscramble);

            _dataContext.Exercises.AddRange(unscramble.Exercises);

            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
