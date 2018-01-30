using Journal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;

namespace Journal.ViewModels.Controller.Assignments
{
    public class AssignToStudentViewModel
    {
        public StudentViewModel Student { get; set; }
        public AssignmentViewModel AssignmentModel { get; set; }
        public IEnumerable<AssignmentViewModel> Assignments { get; set; }
    }
}