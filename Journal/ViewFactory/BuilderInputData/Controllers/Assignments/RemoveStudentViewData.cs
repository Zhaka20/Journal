using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Assignments
{
    public class RemoveStudentViewData
    {
        public RemoveStudentViewData(StudentDTO student, AssignmentDTO assignment)
        {
            Student = student;
            Assignment = assignment;
        }

        public AssignmentDTO Assignment { get; internal set; }
        public StudentDTO Student { get; internal set; }
    }
}