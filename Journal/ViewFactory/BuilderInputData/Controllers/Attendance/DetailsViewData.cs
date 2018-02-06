using Journal.BLLtoUIData.DTOs;
using Journal.ViewModels.Shared.EntityViewModels;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Attendance
{
    public class DetailsViewData
    {
        public DetailsViewData(AttendanceDTO attendance)
        {
            Attendance = attendance;
        }

        public AttendanceDTO Attendance { get; internal set; }
    }
}