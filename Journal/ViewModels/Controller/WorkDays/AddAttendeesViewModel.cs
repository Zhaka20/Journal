using Journal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;

namespace Journal.ViewModels.Controller.WorkDays
{
    public class AddAttendeesViewModel
    {
        public StudentViewModel StudentModel { get; set; }
        public IEnumerable<StudentViewModel> NotPresentStudents { get; set; }
    }
}