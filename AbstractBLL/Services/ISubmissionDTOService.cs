using System.Collections.Generic;
using System.Threading.Tasks;
using Journal.AbstractBLL.AbstractServices.Common;
using Journal.BLLtoUIData.DTOs;

namespace Journal.AbstractBLL.AbstractServices
{
    public interface ISubmissionDTOService : IBasicDTOService<SubmissionDTO, object[]>
    {
        void DeleteFileFromFSandDBIfExists(SubmitFileDTO submitFile);
        Task<IEnumerable<SubmissionDTO>> GetAllWithStudentAssignmentSubmitFileAsync();
        Task<SubmissionDTO> GetByCompositeKeysAsync(int assignmentId, string studentId);
        void UpdateDueDateCompletedAndGrade(SubmissionDTO editedSubmission);
    }
}
