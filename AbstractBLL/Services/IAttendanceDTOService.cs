using System.Collections.Generic;
using System.Threading.Tasks;
using Journal.AbstractBLL.AbstractServices.Common;
using Journal.BLLtoUIData.DTOs;

namespace Journal.AbstractBLL.AbstractServices
{
    public interface IAttendanceDTOService : IBasicDTOService<AttendanceDTO, int>
    {
        Task<IEnumerable<AttendanceDTO>> GetByWorkDayId(int workDayId);
        Task<IEnumerable<AttendanceDTO>> GetAttendeesByIds(List<int> attendaceIds);
        Task<IEnumerable<AttendanceDTO>> GetAllAsyncWithDayAndStudents();
        void UpdateBaseInfo(AttendanceDTO updatedAttendance);
    }
}
