using Journal.AbstractBLL.AbstractServices.Common;
using Journal.BLLtoUIData.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Journal.AbstractBLL.AbstractServices
{
    public interface IStudentDTOService : IBasicDTOService<StudentDTO,string>
    {
        Task<StudentDTO> GetStudentByEmailAsync(string studentEmail);
        Task<IEnumerable<StudentDTO>> GetByMentorId(string mentorId);
        Task<IEnumerable<StudentDTO>> GetStudentsByIds(List<string> attendeeIds);
        Task<StudentDTO> GetByIdAsyncWithMentorSubmissionsFilesAndAssignmentFile(string studentId);
        void UpdateStudentsBaseInfo(StudentDTO newStudent);
        Task<IEnumerable<StudentDTO>> GetNotMyStudents(string mentorId);
        Task<IEnumerable<StudentDTO>> GetAllNotYetAssignedStudentsAsync(IEnumerable<string> assignedStudentIds);
        Task<IEnumerable<StudentDTO>> GetAllByIdAsync(List<string> studentIds);
    }
}
