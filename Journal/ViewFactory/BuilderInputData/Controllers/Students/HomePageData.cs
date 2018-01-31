using Journal.BLLtoUIData.DTOs;
using Journal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Students
{
    public class HomePageData
    {
        public StudentDTO Student { get; set; }
        public IEnumerable<SubmissionDTO> Submissions { get; set; }
    }
}