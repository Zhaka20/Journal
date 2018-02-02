using System;
using System.Collections.Generic;
using Journal.ViewModels.Controller.Submissions;

namespace Journal.DTOBuilderDataFactory.BuilderInputData
{
    public class SubmissionDTOBuilderData
    {
        private int assignmentId;
        private DateTime dueDate;
        private string studentId;
        private EditViewModel viewModel;

        public SubmissionDTOBuilderData(EditViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public SubmissionDTOBuilderData(string studentId, int assignmentId, DateTime dueDate)
        {
            this.studentId = studentId;
            this.assignmentId = assignmentId;
            this.dueDate = dueDate;
        }
    }
}
