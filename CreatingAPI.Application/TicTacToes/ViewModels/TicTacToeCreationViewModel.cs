using CreatingAPI.Application.Activities.ViewModels;
using System.Collections.Generic;

namespace CreatingAPI.Application.TicTacToes.ViewModels
{
    public class TicTacToeCreationViewModel : ActivityCreationViewModel
    {
        public IEnumerable<TicTacToeSquareViewModel> Squares { get; set; }
    }
}
