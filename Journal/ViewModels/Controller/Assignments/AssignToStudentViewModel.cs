using Journal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;

namespace Journal.ViewModels.Controller.Assignments
{
    public class AssignToStudentViewModel
    {
        public StudentViewModel Student { get; set; }
        public AssignmentSubmissionsViewModel AssignmentModel { get; set; }
        public IEnumerable<AssignmentSubmissionsViewModel> Assignments { get; set; }
    }
}