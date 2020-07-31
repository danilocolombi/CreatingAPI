using CreatingAPI.Data.Core.Context;
using CreatingAPI.Domain.Quizzes;
using CreatingAPI.Domain.Quizzes.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CreatingAPI.Data.Quizzes.Repository
{
    public class QuizRepository : IQuizRepository
    {
        private readonly DataContext _dataContext;

        public QuizRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<int> CreateAsync(Quiz quiz)
        {
            try
            {
                var quizWasCreated = await _dataContext.AddAsync(quiz);

                if (quizWasCreated == null) return 0;

                await _dataContext.SaveChangesAsync();

                return quizWasCreated.Entity.Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<bool> UpdateAsync(Quiz quiz)
        {
            try
            {
                var savedQuiz = await GetAsync(quiz.Id);

                if (savedQuiz == null) return false;

                _dataContext.QuizQuestions.RemoveRange(savedQuiz.Questions);

                _dataContext.Entry(savedQuiz).CurrentValues.SetValues(quiz);

                _dataContext.QuizQuestions.AddRange(quiz.Questions);

                return await _dataContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<Quiz> GetAsync(int id)
        {
            var ticTacToe = await _dataContext.Quizzes
                                                .Where(u => u.Id == id)
                                                .Include(u => u.Questions)
                                                .FirstOrDefaultAsync();

            return ticTacToe;
        }

        public async Task<bool> DeleteAsync(Quiz quiz)
        {
            _dataContext.Remove(quiz);

            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
