using System;
using System.ComponentModel.DataAnnotations;

namespace CreatingAPI.Application.Bookmarks.ViewModels
{
    public class BookmarkCreationViewModel
    {
        [Required]
        [Range(1, Int32.MaxValue)]
        public int UserId { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int UnscrumbleId { get; set; }
    }
}
