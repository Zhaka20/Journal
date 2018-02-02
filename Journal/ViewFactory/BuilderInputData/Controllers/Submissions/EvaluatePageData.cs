﻿using Journal.BLLtoUIData.DTOs;
using Journal.ViewModels.Shared.EntityViewModels;
using System.ComponentModel.DataAnnotations;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Submissions
{
    public class EvaluatePageData
    {
        public EvaluatePageData(int assignmentId, string studentId, SubmissionDTO submission)
        {
            AssignmentId = assignmentId;
            StudentId = studentId;
            Submission = submission;
        }

        public SubmissionDTO Submission { get; set; }
        public int AssignmentId { get; set; }
        public string StudentId { get; set; }
    }
}