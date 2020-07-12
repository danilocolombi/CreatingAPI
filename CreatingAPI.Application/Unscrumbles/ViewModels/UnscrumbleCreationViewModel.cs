using CreatingAPI.Application.Activities.ViewModels;
using System.Collections.Generic;

namespace CreatingAPI.Application.Unscrumbles.ViewModels
{
    public class UnscrumbleCreationViewModel : ActivityCreationViewModel
    {

        public IEnumerable<ExerciseViewModel> Exercises { get; set; }
    }
}
