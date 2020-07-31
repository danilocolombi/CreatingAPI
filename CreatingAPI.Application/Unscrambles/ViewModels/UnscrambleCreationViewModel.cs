using CreatingAPI.Application.Activities.ViewModels;
using System.Collections.Generic;

namespace CreatingAPI.Application.Unscrambles.ViewModels
{
    public class UnscrambleCreationViewModel : ActivityCreationViewModel
    {
        public IEnumerable<ExerciseViewModel> Exercises { get; set; }
    }
}
