using Journal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;

namespace Journal.ViewModels.Controller.Submissions
{
    public class SubmissionsIndexViewModel
    {
        public IEnumerable<SubmissionViewModel> Submissions { get; set; }
        public AssignmentViewModel AssignmentModel { get; set; }
        public SubmissionViewModel SubmissionModel { get; set; }
    }
}