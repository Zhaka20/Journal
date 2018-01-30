using Journal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;

namespace Journal.ViewModels.Controller.Attendances
{
    public class IndexViewModel
    {
        public IEnumerable<AttendanceViewModel> Attendances { get; set; }
        public StudentViewModel StudentModel { get; set; }
        public AttendanceViewModel AttendanceModel { get; set; }
    }
}