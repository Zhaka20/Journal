using Journal.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Journal.AbstractBLL.AbstractServices;
using Journal.ViewModels.Controller.Journals;
using Journal.BLLtoUIData.DTOs;
using Journal.ViewFactory.Abstractions;
using Journal.WEB.ViewFactory.BuilderInputData.Controllers.Journal;
using Journal.DTOFactory.Abstractions;
using WorkDaysCreateViewModel = Journal.ViewModels.Controller.WorkDays.CreateViewModel;
using Journal.DTOBuilderDataFactory.BuilderInputData;

namespace Journal.Services.ControllerServices
{
    public class JournalsControllerService : IJournalsControllerService
    {
        protected readonly IJournalDTOService service;
        protected readonly IViewFactory viewFactory;
        protected readonly IDTOFactory dtoFactory;

        public JournalsControllerService(IJournalDTOService service, IViewFactory viewFactor,IDTOFactory dtoFactory)
        {
            this.viewFactory = viewFactor;
            this.service = service;
            this.dtoFactory = dtoFactory;
        }

        public async Task<FillViewModel> GetFillViewModelAsync(int journalId)
        {
            JournalDTO journal = await service.GetByIdAsyncWithWorkDaysAndMentor(journalId);
            if (journal == null)
            {
                return null;
            }
            var viewModelData = new FillPageData(journal);
            FillViewModel viewModel = viewFactory.CreateView<FillPageData,FillViewModel>(viewModelData);

            return viewModel;
        }

        public async Task CreateWorkDayAsync(WorkDaysCreateViewModel viewModel)
        {
            JournalDTO journal = await service.GetByIdAsync(viewModel.JournalId);
            WorkDayDTOBuilderData builderData = new WorkDayDTOBuilderData(viewModel);
            WorkDayDTO newWorkDay = dtoFactory.CreateDTO<WorkDayDTOBuilderData, WorkDayDTO>(builderData);
            journal.WorkDays.Add(newWorkDay);
            await service.SaveChangesAsync();
        }

        public async Task<IndexViewModel> GetJournalsIndexViewModelAsync()
        {
            IEnumerable<JournalDTO> journals = await service.GetAllAsyncWithMentor();
            IndexPageData viewModelData = new IndexPageData(journals);
            IndexViewModel viewModel = viewFactory.CreateView<IndexPageData, IndexViewModel>(viewModelData);
            return viewModel;
        }

        public async Task<DetailsViewModel> GetJournalDetailsViewModelAsync(int journalId)
        {
            JournalDTO journal = await service.GetByIdAsyncWithMentor(journalId);

            if (journal == null)
            {
                return null;
            }
            var viewModelData = new DetailsPageData(journal);
            DetailsViewModel viewModel = viewFactory.CreateView<DetailsPageData, DetailsViewModel>(viewModelData);
            return viewModel;
        }

        public async Task<int> CreateJournalAsync(CreateViewModel viewModel)
        {
            JournalDTOBuilderData builderData = new JournalDTOBuilderData(viewModel);
            JournalDTO newJournal = dtoFactory.CreateDTO<JournalDTOBuilderData, JournalDTO>(builderData);
            
            service.Create(newJournal);
            await service.SaveChangesAsync();
            return newJournal.Id;
        }

        public async Task<EditViewModel> GetEditJournalViewModelAsync(int journalId)
        {
            JournalDTO journal = await service.GetByIdAsyncWithMentor(journalId);
            if (journal == null)
            {
                return null;
            }

            EditPageData viewModelData = new EditPageData(journal);
            EditViewModel viewModel = viewFactory.CreateView<EditPageData, EditViewModel>(viewModelData);
            return viewModel;
        }

        public async Task UpdateJournalAsync(EditViewModel viewModel)
        {
            JournalDTOBuilderData builderData = new JournalDTOBuilderData(viewModel);
            JournalDTO updatedJournal = dtoFactory.CreateDTO<JournalDTOBuilderData, JournalDTO>(builderData);
            service.UpdateYear(updatedJournal);
            await service.SaveChangesAsync();
        }

        public async Task<DeleteViewModel> GetDeleteJournalViewModelAsync(int journalId)
        {
            JournalDTO journal = await service.GetByIdAsync(journalId);
            if (journal == null)
            {
                return null;
            }
            DeletePageData viewModelData = new DeletePageData(journal);
            DeleteViewModel viewModel = viewFactory.CreateView<DeletePageData, DeleteViewModel>(viewModelData);
            return viewModel;
        }

        public async Task DeleteJournalAsync(int journalId)
        {
            JournalDTO journal = await service.GetByIdAsync(journalId);
            service.Delete(journal);
            await service.SaveChangesAsync();
        }

        public void Dispose()
        {
            IDisposable dispose = service as IDisposable;
            if(dispose != null)
            {
                dispose.Dispose();
            }
        }

        WorkDaysCreateViewModel GetCreateWorkDayViewModel(int journalId)
        {
            var viewModel = viewFactory.CreateView<WorkDaysCreateViewModel>();
            return viewModel;
        }

        CreateViewModel GetCreateJournalViewModel(string mentorId)
        {
            var viewModel = viewFactory.CreateView<CreateViewModel>();
            return viewModel;
        }
    }
}