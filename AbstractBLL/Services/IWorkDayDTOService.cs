using System.Threading.Tasks;
using Journal.AbstractBLL.AbstractServices.Common;
using Journal.BLLtoUIData.DTOs;

namespace Journal.AbstractBLL.AbstractServices
{
    public interface IWorkDayDTOService : IBasicDTOService<WorkDayDTO, int>
    {
        Task<WorkDayDTO> GetWorkDayWithAttendeesByIdAsync(int workDayId);
        void UpdateDay(WorkDayDTO updatedWorkDay);
    }
}
