using Journal.BLLtoUIData.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Journal.ViewFactory.BuilderInputData.Controllers.Submissions
{
    public class SubmissionViewData
    {
        public SubmissionViewData(SubmissionDTO submission)
        {
            Submission = submission;
        }

        public SubmissionDTO Submission { get; set; }
    }
}