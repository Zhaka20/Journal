using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Students
{
    public class EditPageData
    {
        public EditPageData(StudentDTO student)
        {
            Student = student;
        }

        public StudentDTO Student { get; set; }
    }
}