using Journal.BLLtoUIData.DTOs;
using System.Collections.Generic;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Students
{
    public class IndexViewData
    {
        public IndexViewData(IEnumerable<StudentDTO> students)
        {
            Students = students;
        }

        public IEnumerable<StudentDTO> Students { get; set; }
    }
}