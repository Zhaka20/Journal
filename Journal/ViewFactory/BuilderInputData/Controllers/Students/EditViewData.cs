using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Students
{
    public class EditViewData
    {
        public EditViewData(StudentDTO student)
        {
            Student = student;
        }

        public StudentDTO Student { get; set; }
    }
}