using Journal.BLLtoUIData.DTOs;
using Journal.PageDatas.Shared.EntityPageDatas;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Assignments
{
    public class RemoveStudentPageData
    {
        public RemoveStudentPageData(StudentDTO student, AssignmentDTO assignment)
        {
            Student = student;
            Assignment = assignment;
        }

        public AssignmentDTO Assignment { get; internal set; }
        public StudentDTO Student { get; internal set; }
    }
}