using Journal.BLLtoUIData.DTOs;
using Journal.ViewModels.Shared.EntityViewModels;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Attendance
{
    public class DetailsPageData
    {
        public DetailsPageData(AttendanceDTO attendance)
        {
            Attendance = attendance;
        }

        public AttendanceDTO Attendance { get; internal set; }
    }
}