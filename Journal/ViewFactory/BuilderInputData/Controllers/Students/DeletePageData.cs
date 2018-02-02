using Journal.BLLtoUIData.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Students
{
    public class DeletePageData
    {
        public DeletePageData(StudentDTO student)
        {
            Student = student;
        }

        public StudentDTO Student { get; set; }
    }
}