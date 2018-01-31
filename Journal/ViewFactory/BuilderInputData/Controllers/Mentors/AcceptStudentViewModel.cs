using Journal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;

namespace Journal.ViewModels.Controller.Mentors
{
    public class AcceptStudentViewModel
    {
        public IEnumerable<StudentViewModel> Students { get; set; }
        public StudentViewModel Student { get; set; }
    }

}