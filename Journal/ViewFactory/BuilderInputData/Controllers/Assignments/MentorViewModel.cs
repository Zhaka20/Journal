using Journal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;

namespace Journal.ViewModels.Controller.Assignments
{
    public class MentorViewModel
    {
        public MentorViewModel Mentor { get; set; }
        public IEnumerable<AssignmentSubmissionsViewModel> Assignments { get; set; }
        public AssignmentSubmissionsViewModel AssignmentModel { get; set; }
    }
}