using Journal.AbstractBLL.AbstractServices.Common;
using Journal.BLLtoUIData.DTOs;
using System.Threading.Tasks;

namespace Journal.AbstractBLL.AbstractServices
{
    public interface IStudentDTOService : IBasicDTOService<StudentDTO,string>
    {
        Task<StudentDTO> GetStudentByEmailAsync(string studentEmail);
    }
}
