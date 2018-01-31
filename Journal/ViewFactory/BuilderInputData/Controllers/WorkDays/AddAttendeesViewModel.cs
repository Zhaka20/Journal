using Journal.BLLtoUIData.DTOs;
using System.Collections.Generic;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.WorkDays
{
    public class AddAttendeesViewModelBuilderData
    {
        public IEnumerable<StudentDTO> NotPresentStudents { get; set; }
    }
}