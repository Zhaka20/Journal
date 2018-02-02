using Journal.BLLtoUIData.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Journal.ViewFactory.BuilderInputData.Controllers.Submissions
{
    public class SubmissionPageData
    {
        public SubmissionPageData(SubmissionDTO submission)
        {
            Submission = submission;
        }

        public SubmissionDTO Submission { get; set; }
    }
}