using System.Collections.Generic;
using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Assignments
{
    public class AssignToStudentViewData
    {
        private IEnumerable<AssignmentDTO> notYetAssigned;

        public AssignToStudentViewData(IEnumerable<AssignmentDTO> notYetAssigned, StudentDTO student)
        {
            this.notYetAssigned = notYetAssigned;
            Student = student;
        }

        public IEnumerable<AssignmentDTO> Assignments { get; internal set; }
        public StudentDTO Student { get; internal set; }
    }
}