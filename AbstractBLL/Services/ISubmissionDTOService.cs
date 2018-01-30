using Journal.AbstractBLL.AbstractServices.Common;
using Journal.BLLtoUIData.DTOs;

namespace Journal.AbstractBLL.AbstractServices
{
    public interface ISubmissionDTOService : IBasicDTOService<SubmissionDTO, object[]>
    {
        void DeleteFileFromFSandDBIfExists(SubmitFileDTO submitFile);
    }
}
