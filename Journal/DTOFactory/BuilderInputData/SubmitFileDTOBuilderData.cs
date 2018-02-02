using System.Web;

namespace Journal.DTOBuilderDataFactory.BuilderInputData
{
    public class SubmitFileDTOBuilderData : FileInfoDTOBuilderData
    {
        private HttpPostedFileBase file;

        public SubmitFileDTOBuilderData(HttpPostedFileBase file)
        {
            this.file = file;
        }
    }
}
