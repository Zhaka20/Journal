using Journal.ViewModels.Shared.EntityViewModels;

namespace Journal.ViewModels.Controller.WorkDays
{
    public class DetailsViewModel
    {
        public WorkDayViewModel WorkDay { get; set; }
        public AttendanceViewModel AttendanceModel { get; set; }
    }
}