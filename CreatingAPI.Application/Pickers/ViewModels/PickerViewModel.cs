using CreatingAPI.Application.Activities.ViewModels;
using System.Collections.Generic;

namespace CreatingAPI.Application.Pickers.ViewModels
{
    public class PickerViewModel : ActivityViewModel
    {
        public IEnumerable<PickerTopicViewModel> Topics { get; set; }
    }
}
