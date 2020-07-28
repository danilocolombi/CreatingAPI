using System.ComponentModel.DataAnnotations;

namespace CreatingAPI.Application.Pickers.ViewModels
{
    public class PickerTopicViewModel
    {
        [Required(ErrorMessage = "The {0} is required")]
        [MinLength(3, ErrorMessage = "The {0} can't have less than 3 characters")]
        [MaxLength(150, ErrorMessage = "The {0} can't have more than 100 characters")]
        public string Description { get; set; }
    }
}
