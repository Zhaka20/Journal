using Journal.ViewModels.Shared.EntityViewModels;

namespace Journal.ViewModels.Controller.Mentors
{
    public class MyStudentViewModel
    {
        public StudentViewModel Student { get; set; }
        public AssignmentViewModel AssignmentModel { get; set; }
        public SubmissionViewModel SubmissionModel { get; set; }
    }
}