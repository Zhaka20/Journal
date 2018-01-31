using Journal.BLLtoUIData.DTOs;
using System.Collections.Generic;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Students
{
    public class IndexPageData
    {
        public IEnumerable<StudentDTO> Students { get; set; }
    }
}