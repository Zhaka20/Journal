using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Submissions
{
    public class DetailsViewData
    {
        public DetailsViewData(SubmissionDTO submission)
        {
            Submission = submission;
        }

        public SubmissionDTO Submission { get; set; }
    }
}