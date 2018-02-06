using Journal.BLLtoUIData.DTOs;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Submissions
{
    public class EditViewData
    {
        public EditViewData(SubmissionDTO submission)
        {
            Submission = submission;
        }

        public SubmissionDTO Submission { get; set; }
    }
}