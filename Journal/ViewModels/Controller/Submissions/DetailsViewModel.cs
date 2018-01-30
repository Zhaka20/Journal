﻿using Journal.ViewModels.Shared.EntityViewModels;

namespace Journal.ViewModels.Controller.Submissions
{
    public class DetailsViewModel
    {
        public SubmissionViewModel Submission { get; set; }
        public StudentViewModel StudentModel { get; set; }
        public AssignmentViewModel AssignmentModel { get; set; }
    }
}