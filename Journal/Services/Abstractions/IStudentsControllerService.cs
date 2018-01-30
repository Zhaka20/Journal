using Journal.ViewModels.Controller.Students;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace Journal.Services.Abstractions
{
    public interface IStudentsControllerService : IDisposable
    {
        Task<IndexViewModel> GetIndexViewModelAsync();
        Task<HomeViewModel> GetHomeViewModelAsync(string studentId);
        Task<DetailsViewModel> GetDetailsViewModelAsync(string studentId);
        CreateViewModel GetCreateViewModel();
        Task<IdentityResult> CreateAsync(CreateViewModel student);
        Task<EditViewModel> GetEditViewModelAsync(string studentId);
        Task UpdateAsync(EditViewModel studentViewModel);
        Task<DeleteViewModel> GetDeleteViewModelAsync(string studentId);
        Task DeleteAsync(string id);
    }
}
