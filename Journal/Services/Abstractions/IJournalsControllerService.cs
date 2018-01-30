using Journal.ViewModels.Controller.Journals;
using System;
using System.Threading.Tasks;

namespace Journal.Services.Abstractions
{
    public interface IJournalsControllerService : IDisposable
    {
        Task<FillViewModel> GetFillViewModelAsync(int journalId);
        ViewModels.Controller.WorkDays.CreateViewModel GetCreateWorkDayViewModel(int journalId);
        Task CreateWorkDayAsync(ViewModels.Controller.WorkDays.CreateViewModel viewModel);
        Task<IndexViewModel> GetIndexViewModelAsync();
        Task<DetailsViewModel> GetDetailsViewModelAsync(int journalId);
        CreateViewModel GetCreateViewModel(string mentorId);
        Task<int> CreateJournalAsync(CreateViewModel viewModel);
        Task<EditViewModel> GetEditViewModelAsync(int journalId);
        Task UpdateJournalAsync(EditViewModel viewModel);
        Task<DeleteViewModel> GetDeleteViewModelAsync(int journalId);
        Task DeleteJournalAsync(int journalId);
    }
}
