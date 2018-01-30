using Journal.ViewModels.Shared.EntityViewModels;

namespace Journal.ViewModels.Controller.Submissions
{
    public class DeleteViewModel
    {
        public SubmissionViewModel Submission { get; set; }
        public StudentViewModel StudentModel { get; set; }
        public AssignmentSubmissionsViewModel AssignmentModel { get; set; }
    }
}