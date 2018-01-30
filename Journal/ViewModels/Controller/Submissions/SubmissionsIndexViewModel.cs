using Journal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;

namespace Journal.ViewModels.Controller.Submissions
{
    public class IndexViewModel
    {
        public IEnumerable<SubmissionViewModel> Submissions { get; set; }
        public AssignmentSubmissionsViewModel AssignmentModel { get; set; }
        public SubmissionViewModel SubmissionModel { get; set; }
    }
}