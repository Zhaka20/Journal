using Journal.BLLtoUIData.DTOs;
using Journal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Submissions
{
    public class IndexPageData
    {
        public IndexPageData(IEnumerable<SubmissionDTO> submissions)
        {
            Submissions = submissions;
        }

        public IEnumerable<SubmissionDTO> Submissions { get; set; }
    }
}