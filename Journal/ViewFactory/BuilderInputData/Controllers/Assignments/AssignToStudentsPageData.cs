using System.Collections.Generic;
using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Assignments
{
    public class AssignToStudentsPageData
    {
        private IEnumerable<StudentDTO> otherStudents;

        public AssignToStudentsPageData(AssignmentDTO assignment, IEnumerable<StudentDTO> otherStudents)
        {
            Assignment = assignment;
            this.otherStudents = otherStudents;
        }

        public AssignmentDTO Assignment { get; internal set; }
        public IEnumerable<StudentDTO> Students { get; internal set; }
    }
}