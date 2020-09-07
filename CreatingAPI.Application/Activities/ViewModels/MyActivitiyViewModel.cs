using CreatingAPI.Domain.Activities;
using System;

namespace CreatingAPI.Application.Activities.ViewModels
{
    public class MyActivitiyViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsPublic { get; set; }
        public KindOfActivity KindOfActivity { get; set; }

        public MyActivitiyViewModel(int id, string title, DateTime createdAt, bool isPublic, KindOfActivity kindOfActivity)
        {
            Id = id;
            Title = title;
            CreatedAt = createdAt;
            IsPublic = IsPublic;
            KindOfActivity = kindOfActivity;
        }
    }
}
