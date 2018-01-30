using Journal.DataModel.Models;
using Journal.AbstractBLL.AbstractServices;
using BLL.Services.Common;
using Journal.BLLtoUIData.DTOs;
using BLL.Services.Common.Abstract;
using Journal.AbstractDAL.AbstractRepositories;

namespace Journal.BLL.Services.Concrete
{
    public class AttendanceDTOService : GenericDTOService<AttendanceDTO, Attendance, int>, IAttendanceDTOService
    {
        public AttendanceDTOService(IAttendanceRepository repository, IObjectToObjectMapper mapper) : base(repository, mapper)
        {
        }
    }
}
