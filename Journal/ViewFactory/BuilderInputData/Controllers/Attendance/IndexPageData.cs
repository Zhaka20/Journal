using Journal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;
using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Attendance
{
    public class IndexPageData
    {
        public IndexPageData(IEnumerable<AttendanceDTO> attendances)
        {
            Attendances = attendances;
        }

        public IEnumerable<AttendanceDTO> Attendances { get; internal set; }
    }
}