using Journal.BLLtoUIData.DTOs;
using Journal.ViewModels.Shared.EntityViewModels;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Submissions
{
    public class DeletePageData
    {
        public DeletePageData(SubmissionDTO submission)
        {
            Submission = submission;
        }

        public SubmissionDTO Submission { get; set; }
    }
}