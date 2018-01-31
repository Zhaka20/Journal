using Journal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;

namespace Journal.ViewModels.Controller.Assignments
{
    public class AssignToStudentsViewModel
    {
        public IEnumerable<StudentViewModel> Students { get; set; }
        public StudentViewModel StudentModel { get; set; }
        public AssignmentSubmissionsViewModel Assignment { get; set; }
    }
}