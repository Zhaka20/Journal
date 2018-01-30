using Journal.AbstractBLL.AbstractServices;
using Journal.DataModel.Models;
using BLL.Services.Common;
using Journal.BLLtoUIData.DTOs;
using BLL.Services.Common.Abstract;
using Journal.AbstractDAL.AbstractRepositories;

namespace Journal.BLL.Services.Concrete
{
    public class SubmitFileDTOService : GenericDTOService<SubmitFileDTO, SubmitFile, int>, ISubmitFileDTOService
    {
        public SubmitFileDTOService(ISubmitFileRepository repository,
                                    IObjectToObjectMapper mapper)
                                  : base(repository, mapper)
        {
        }
    }
}
