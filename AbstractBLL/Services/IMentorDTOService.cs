using Journal.AbstractBLL.AbstractServices.Common;
using Journal.BLLtoUIData.DTOs;
using System.Threading.Tasks;

namespace Journal.AbstractBLL.AbstractServices
{
    public interface IMentorDTOService : IBasicDTOService<MentorDTO, string>
    {
        Task<MentorDTO> GetMentorByEmailAsync(string mentorEmail);
        Task AcceptStudentAsync(string studentId, string mentorId);
        Task RemoveStudentAsync(string studentId, string mentorId);
    }
}
