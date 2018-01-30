using Journal.ViewModels.Controller.Assignments;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Journal.Services.Abstractions
{
    public interface IAssignmentsControllerService : IDisposable
    {
        Task<IndexViewModel> GetIndexViewModelAsync();
        Task<DetailsViewModel> GetDetailsViewModelAsync(int assignmentId);
        Task<MentorViewModel> GetMentorAssignmentsViewModelAsync(string mentorId);
        CreateViewModel GetCreateViewModel();
        Task<int> CreateAsync(Controller controller, string mentorId, CreateViewModel inputModel, HttpPostedFileBase file);
        Task<CreateAndAssignToSingleUserViewModel> GetCreateAndAssignToSingleUserViewModelAsync(string studentId);
        Task<int> CreateAndAssignToSingleUserAsync(Controller controller, string studentId, CreateViewModel inputModel, HttpPostedFileBase file);
        Task<EdtiViewModel> GetEdtiViewModelAsync(int assignmentId);
        Task UpdateAsync(EdtiViewModel inputModel);
        Task<DeleteViewModel> GetDeleteViewModelAsync(int assignmentId);
        Task DeleteAsync(int assignmentId);
        Task<IFileStreamWithInfo> GetAssignmentFileAsync(int assignmentId);
        Task<RemoveStudentViewModel> GetRemoveStudentViewModelAsync(int assignmentId, string studentId);
        Task RemoveStudentFromAssignmentAsync(int assignmentId, string studentId);
        Task<AssignToStudentViewModel> GetAssignToStudentViewModelAsync(string studentId);
        Task AssignToStudentAsync(string id, List<int> assignmentIds);
        Task<AssignToStudentsViewModel> GetAssignToStudentsViewModelAsync(int assigmentId);
        Task AssignToStudentsAsync(int assigmentId, List<string> studentIds);
        Task<StudentsAndSubmissionsListViewModel> GetStudentsAndSubmissionsListViewModelAsync(int assingmentId);
    }
}
