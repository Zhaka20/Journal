using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Journal.AbstractBLL.AbstractServices.Common;
using Journal.BLLtoUIData.DTOs;

namespace Journal.AbstractBLL.AbstractServices
{
    public interface IAssignmentDTOService : IBasicDTOService<AssignmentDTO, int>
    {
        Task<AssignmentDTO> GetByIdIncludeCreatorSubmissionStudentAndSubmitFileAsync(int assignmentId);
        Task<IEnumerable<AssignmentDTO>> GetAllWithAssignmentFileCreatorAndSubmissionsAsync();
        Task<AssignmentDTO> GetByIdWithAssignmentFileCreatorAndSubmissionsAsync(int assignmentId);
        Task<IEnumerable<AssignmentDTO>> GetByCreatorsIdAsync(string mentorId);
        void UpdateTitle(AssignmentDTO updatedAssignment);
        Task<AssignmentDTO> GetByIdAsyncWithSubmissionAndFiles(Func<object, bool> p1, Func<object, object> p2, Func<object, object> p3);
        Task<AssignmentDTO> GetByIdAsyncWithSubmissionAndFiles(int assignmentId);
        Task<AssignmentDTO> GetByIdWithFileAsync(int assignmentId);
        IEnumerable<AssignmentDTO> GetNotYetAssignedList(string mentorId, string studentId);
        Task<IEnumerable<AssignmentDTO>> GetAllByIdAsync(List<int> assignmentIds);
        Task<AssignmentDTO> GetByIdWithCreatorAndSubmissionsAsync(int assigmentId);
        Task<AssignmentDTO> GetByIdIncludeAssingmentFileCreatorSubmissionStudentAsync(int assingmentId);
    }
}
