using CreatingAPI.Data.Core.Context;
using CreatingAPI.Domain.TicTacToes;
using CreatingAPI.Domain.TicTacToes.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CreatingAPI.Data.TicTacToes.Repository
{
    public class TicTacToeRepository : ITicTacToeRepository
    {
        private readonly DataContext _dataContext;

        public TicTacToeRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<int> CreateTicTacToe(TicTacToe ticTacToe)
        {
            try
            {
                var ticTacToeWasCreated = await _dataContext.AddAsync(ticTacToe);

                if (ticTacToeWasCreated == null) return 0;

                await _dataContext.SaveChangesAsync();

                return ticTacToeWasCreated.Entity.Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<bool> UpdateTicTacToe(TicTacToe ticTacToe)
        {
            try
            {
                var savedTicTacToe = await GetTicTacToe(ticTacToe.Id);

                if (savedTicTacToe == null) return false;

                _dataContext.TicTacToeSquares.RemoveRange(savedTicTacToe.Squares);

                _dataContext.Entry(savedTicTacToe).CurrentValues.SetValues(ticTacToe);

                _dataContext.TicTacToeSquares.AddRange(ticTacToe.Squares);

                return await _dataContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<TicTacToe> GetTicTacToe(int id)
        {
            var ticTacToe = await _dataContext.TicTacToes
                                                .Where(u => u.Id == id)
                                                .Include(u => u.Squares)
                                                .FirstOrDefaultAsync();

            return ticTacToe;
        }

        public async Task<bool> DeleteTicTacToe(TicTacToe ticTacToe)
        {
            _dataContext.Remove(ticTacToe);

            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
