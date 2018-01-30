using Journal.DataModel.Models;
using System;
using Journal.AbstractBLL.AbstractServices;
using System.IO;
using Journal.AbstractDAL.AbstractRepositories;
using Journal.BLLtoUIData.DTOs;
using BLL.Services.Common;
using BLL.Services.Common.Abstract;

namespace Journal.BLL.Services.Concrete
{
    public class SubmissionDTOService : GenericDTOService<SubmissionDTO, Submission, object[]>, ISubmissionDTOService
    {
        protected readonly ISubmitFileDTOService submitFileService;

        public SubmissionDTOService(ISubmitFileDTOService submitFileService,
                                    ISubmissionRepository repository,
                                    IObjectToObjectMapper mapper)
                                  : base(repository,mapper)
        {
            this.submitFileService = submitFileService;
        }

        public void DeleteFileFromFSandDBIfExists(SubmitFileDTO dto)
        {
            if(dto == null)
            {
                throw new ArgumentNullException();
            }
            var submitFileEntity = mapper.Map<SubmitFileDTO, SubmitFile>(dto);
            if (submitFileEntity != null)
            {
                DeleteFile(submitFileEntity);
            }
            submitFileService.Delete(dto);
        }

        protected void DeleteFile(DataModel.Models.FileInfo file)
        {
            if (file == null) return;

            string fullPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Files/Assignments"), file.FileGuid);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }

        public void Dispose()
        {
            IDisposable dispose = submitFileService as IDisposable;
            if(dispose != null)
            {
                dispose.Dispose();
            }
        }
    }
}
