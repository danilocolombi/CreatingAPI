using CreatingAPI.Data.Core.Context;
using CreatingAPI.Domain.Pickers;
using CreatingAPI.Domain.Pickers.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CreatingAPI.Data.Pickers.Repository
{
    public class PickerRepository : IPickerRepository
    {
        private readonly DataContext _dataContext;

        public PickerRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<int> CreateAsync(Picker picker)
        {
            try
            {
                var pickerWasCreated = await _dataContext.AddAsync(picker);

                if (pickerWasCreated == null) return 0;

                await _dataContext.SaveChangesAsync();

                return pickerWasCreated.Entity.Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<bool> UpdateAsync(Picker picker)
        {
            try
            {
                var savedPicker = await GetAsync(picker.Id);

                if (savedPicker == null) return false;

                _dataContext.PickerTopics.RemoveRange(savedPicker.Topics);

                _dataContext.Entry(savedPicker).CurrentValues.SetValues(picker);

                _dataContext.PickerTopics.AddRange(picker.Topics);

                return await _dataContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<Picker> GetAsync(int id)
        {
            var picker = await _dataContext.Pickers
                                               .Where(u => u.Id == id)
                                               .Include(u => u.Topics)
                                               .FirstOrDefaultAsync();

            return picker;
        }

        public async Task<bool> DeleteAsync(Picker picker)
        {
            _dataContext.Remove(picker);

            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
