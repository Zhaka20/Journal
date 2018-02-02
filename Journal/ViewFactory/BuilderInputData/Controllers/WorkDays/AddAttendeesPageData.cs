using Journal.BLLtoUIData.DTOs;
using System.Collections.Generic;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.WorkDays
{
    public class AddAttendeesPageData
    {
        public AddAttendeesPageData(IEnumerable<StudentDTO> notPresentStudents)
        {
            NotPresentStudents = notPresentStudents;
        }

        public IEnumerable<StudentDTO> NotPresentStudents { get; set; }
    }
}