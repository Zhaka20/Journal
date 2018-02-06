using Journal.BLLtoUIData.DTOs;
using Journal.ViewModels.Shared.EntityViewModels;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Submissions
{
    public class DeleteViewData
    {
        public DeleteViewData(SubmissionDTO submission)
        {
            Submission = submission;
        }

        public SubmissionDTO Submission { get; set; }
    }
}