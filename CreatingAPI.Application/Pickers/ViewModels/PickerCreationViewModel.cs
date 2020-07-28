using CreatingAPI.Application.Activities.ViewModels;
using System.Collections.Generic;

namespace CreatingAPI.Application.Pickers.ViewModels
{
    public class PickerCreationViewModel : ActivityCreationViewModel
    {
        public IEnumerable<PickerTopicViewModel> Topics { get; set; }
    }
}
