using Journal.BLL.Services.Concrete.Common;
using Journal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Journal.AbstractDAL.AbstractRepositories.Common;
using Journal.AbstractBLL.AbstractServices;
using System.IO;
using Journal.AbstractDAL.AbstractRepositories;

namespace Journal.BLL.Services.Concrete
{
    public class SubmissionDTOService : GenericService<Submission, object[]>, ISubmissionDTOService
    {
        protected readonly ISubmitFileDTOService submitFileService;

        public SubmissionDTOService(ISubmissionRepository repository, ISubmitFileDTOService submitFileService) : base(repository)
        {
            this.submitFileService = submitFileService;
        }

        public void DeleteFileFromFSandDBIfExists(SubmitFile submitFile)
        {
            if (submitFile != null)
            {
                DeleteFile(submitFile);
            }
            submitFileService.Delete(submitFile);
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

        public override void Dispose()
        {
            IDisposable dispose = submitFileService as IDisposable;
            if(dispose != null)
            {
                dispose.Dispose();
            }
            base.Dispose();
        }
    }
}
