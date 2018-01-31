using Journal.BLLtoUIData.DTOs;
using Journal.ViewModels.Shared.EntityViewModels;
using System.ComponentModel.DataAnnotations;

namespace Journal.WEB.ViewFactory.BuilderInputData.Controllers.Submissions
{
    public class EvaluatePageData
    {
        public SubmissionDTO Submission { get; set; }
        public int AssignmentId { get; set; }
        public string StudentId { get; set; }
    }
}