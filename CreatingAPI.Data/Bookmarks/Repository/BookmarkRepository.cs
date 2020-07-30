using CreatingAPI.Data.Core.Context;
using CreatingAPI.Domain.Bookmarks;
using CreatingAPI.Domain.Bookmarks.Interfaces;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CreatingAPI.Data.Bookmarks.Repository
{
    public class BookmarkRepository : IBookmarkRepository
    {
        private readonly DataContext _dataContext;

        public BookmarkRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<int> CreateAsync(Bookmark bookmark)
        {
            try
            {
                var bookmarkCreated = await _dataContext.AddAsync(bookmark);

                if (bookmarkCreated == null) return 0;

                await _dataContext.SaveChangesAsync();

                return bookmarkCreated.Entity.Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<bool> DeleteAsync(Bookmark bookmark)
        {
            _dataContext.Remove(bookmark);

            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<Bookmark> GetAsync(int id)
        {
            return await _dataContext.Bookmarks.FindAsync(id);
        }
    }
}
