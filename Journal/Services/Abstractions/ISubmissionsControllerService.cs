using Journal.ViewModels.Controller.Submissions;
using Journal.ViewModels.Shared.EntityViewModels;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Journal.Services.Abstractions
{
    public interface ISubmissionsControllerService : IDisposable
    {
        Task<IndexViewModel> GetIndexViewModelAsync();
        Task<AssignmentSubmissionsViewModel> GetAssignmentSubmissionsViewModelAsync(int assignmentId);
        Task<DetailsViewModel> GetDetailsViewModelAsync(int assignmentId, string studentId);
        Task<EditViewModel> GetEditViewModelAsync(int assignmentId, string studentId);
        Task UpdateAsync(EditViewModel viewModel);
        Task<DeleteViewModel> GetDeleteViewModelAsync(int assignmentId, string studentId);
        Task DeleteAsync(int assignmentId, string studentId);
        Task<IFileStreamWithInfo> GetSubmissionFileAsync(Controller server,int assignmentId, string studentId);
        Task<bool> ToggleCompleteStatusAsync(int assignmentId, string studentId);
        Task<EvaluateViewModel> GetEvaluateViewModelAsync(int assignmentId, string studentId);
        Task EvaluateAsync(EvaluateInputModel inputModel);
        Task UploadFileAsync(Controller controller, HttpPostedFileBase file, int assignmentId, string studentId);
        Task<SubmissionViewModel> GetSubmissionAsync(int assignmentId, string studentId);
    }
}
