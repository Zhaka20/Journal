using System.Collections.Generic;
using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Assignments
{
    public class AssignToStudentsPageData
    {
        public AssignmentDTO Assignment { get; internal set; }
        public IEnumerable<StudentDTO> Students { get; internal set; }
    }
}