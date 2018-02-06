using Journal.BLLtoUIData.DTOs;
using Journal.ViewModels.Shared.EntityViewModels;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Attendance
{
    public class DeleteViewData
    {
        public DeleteViewData(AttendanceDTO attendance)
        {
            Attendance = attendance;
        }

        public AttendanceDTO Attendance { get; internal set; }
    }
}