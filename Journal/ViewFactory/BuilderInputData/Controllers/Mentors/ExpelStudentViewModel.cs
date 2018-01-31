using Journal.ViewModels.Shared.EntityViewModels;

namespace Journal.ViewModels.Controller.Mentors
{
    public class ExpelStudentViewModel
    {
        public StudentViewModel Student { get; set; }
        public string MentorId { get; set; }
    }
}