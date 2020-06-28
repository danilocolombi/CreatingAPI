using System;

namespace CreatingAPI.Application.Unscrumbles.ViewModels
{
    public class UnscrumbleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsPublic { get; set; }
    }
}
