using System.Threading.Tasks;

namespace CreatingAPI.Domain.Quizzes.Interfaces
{
    public interface IQuizRepository
    {
        Task<int> CreateAsync(Quiz quiz);
        Task<bool> UpdateAsync(Quiz quiz);
        Task<bool> DeleteAsync(Quiz quiz);
        Task<Quiz> GetAsync(int id);
    }
}
