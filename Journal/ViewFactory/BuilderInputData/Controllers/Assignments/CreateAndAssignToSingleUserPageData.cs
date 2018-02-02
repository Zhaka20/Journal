
using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Assignments
{
    public class CreateAndAssignToSingleUserPageData
    {
        private StudentDTO studentDTO;

        public CreateAndAssignToSingleUserPageData(StudentDTO studentDTO)
        {
            this.studentDTO = studentDTO;
        }

        public StudentDTO Student { get; internal set; }
    }
}