using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Submissions
{
    public class DetailsPageData
    {
        public DetailsPageData(SubmissionDTO submission)
        {
            Submission = submission;
        }

        public SubmissionDTO Submission { get; set; }
    }
}