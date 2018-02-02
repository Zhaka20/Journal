using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Journal.DTOBuilderDataFactory.BuilderInputData
{
    public class AssignmentFileDTOBuilderData
    {
        private HttpPostedFileBase file;

        public AssignmentFileDTOBuilderData(HttpPostedFileBase file)
        {
            this.file = file;
        }
    }
}
