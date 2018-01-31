using Journal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;

namespace Journal.ViewModels.Controller.Students
{
    public class HomeViewModel
    {
        public StudentViewModel Student { get; set; }
        public AssignmentSubmissionsViewModel AssignmentModel { get; set; }
        public SubmissionViewModel SubmissionModel { get; set; }
        public IEnumerable<SubmissionViewModel> Submissions { get; set; }
    }
}