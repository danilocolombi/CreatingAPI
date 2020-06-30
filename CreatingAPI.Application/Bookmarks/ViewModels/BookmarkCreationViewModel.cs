using System;
using System.ComponentModel.DataAnnotations;

namespace CreatingAPI.Application.Bookmarks.ViewModels
{
    public class BookmarkCreationViewModel
    {
        [Required(ErrorMessage = "The {0} is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid {0}")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid {0}")]
        public int UnscrumbleId { get; set; }
    }
}
