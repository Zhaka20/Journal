using Journal.DataModel.Models;
using Journal.AbstractBLL.AbstractServices;
using BLL.Services.Common;
using Journal.BLLtoUIData.DTOs;
using BLL.Services.Common.Abstract;

namespace Journal.BLL.Services.Concrete
{
    public class AttendanceDTOService : GenericDTOService<AttendanceDTO, Attendance, int>, IAttendanceDTOService
    {
        public AttendanceDTOService(IGenericService<Attendance, int> entityService, IObjectToObjectMapper mapper) : base(entityService, mapper)
        {
        }
    }
}
