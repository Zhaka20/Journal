using Journal.BLLtoUIData.DTOs;
using System.Collections.Generic;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Students
{
    public class IndexPageData
    {
        public IndexPageData(IEnumerable<StudentDTO> students)
        {
            Students = students;
        }

        public IEnumerable<StudentDTO> Students { get; set; }
    }
}