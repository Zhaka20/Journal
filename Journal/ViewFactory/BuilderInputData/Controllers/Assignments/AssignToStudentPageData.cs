using System.Collections.Generic;
using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Assignments
{
    public class AssignToStudentPageData
    {
        public IEnumerable<AssignmentDTO> Assignments { get; internal set; }
        public StudentDTO Student { get; internal set; }
    }
}