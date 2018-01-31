using Journal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;

namespace Journal.ViewModels.Controller.Assignments
{
    public class IndexViewModel
    {
        public IEnumerable<AssignmentViewModel> Assignments { get; set; }
        public AssignmentViewModel AssignmentModel { get; set; }
    }
}