using System.Collections.Generic;
using System.Threading.Tasks;
using Journal.AbstractBLL.AbstractServices.Common;
using Journal.BLLtoUIData.DTOs;

namespace Journal.AbstractBLL.AbstractServices
{
    public interface IJournalDTOService : IBasicDTOService<JournalDTO, int>
    {
        Task<JournalDTO> GetByIdAsyncWithWorkDaysAndMentor(int journalId);
        Task<IEnumerable<JournalDTO>> GetAllAsyncWithMentor();
        Task<JournalDTO> GetByIdAsyncWithMentor(int journalId);
        void UpdateYear(JournalDTO updatedJournal);
    }
}
