using System.Collections.Generic;
using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Assignments
{
    public class AssignToStudentPageData
    {
        private IEnumerable<AssignmentDTO> notYetAssigned;

        public AssignToStudentPageData(IEnumerable<AssignmentDTO> notYetAssigned, StudentDTO student)
        {
            this.notYetAssigned = notYetAssigned;
            Student = student;
        }

        public IEnumerable<AssignmentDTO> Assignments { get; internal set; }
        public StudentDTO Student { get; internal set; }
    }
}