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
            var viewModelData = new FillViewData(journal);
            FillViewModel viewModel = viewFactory.CreateView<FillViewData,FillViewModel>(viewModelData);

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

        public async Task<IndexViewModel> GetIndexViewModelAsync()
        {
            IEnumerable<JournalDTO> journals = await service.GetAllAsyncWithMentor();
            IndexViewData viewModelData = new IndexViewData(journals);
            IndexViewModel viewModel = viewFactory.CreateView<IndexViewData, IndexViewModel>(viewModelData);
            return viewModel;
        }

        public async Task<DetailsViewModel> GetDetailsViewModelAsync(int journalId)
        {
            JournalDTO journal = await service.GetByIdAsyncWithMentor(journalId);

            if (journal == null)
            {
                return null;
            }
            var viewModelData = new DetailsViewData(journal);
            DetailsViewModel viewModel = viewFactory.CreateView<DetailsViewData, DetailsViewModel>(viewModelData);
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

        public async Task<EditViewModel> GetEditViewModelAsync(int journalId)
        {
            JournalDTO journal = await service.GetByIdAsyncWithMentor(journalId);
            if (journal == null)
            {
                return null;
            }

            EditViewData viewModelData = new EditViewData(journal);
            EditViewModel viewModel = viewFactory.CreateView<EditViewData, EditViewModel>(viewModelData);
            return viewModel;
        }

        public async Task UpdateJournalAsync(EditViewModel viewModel)
        {
            JournalDTOBuilderData builderData = new JournalDTOBuilderData(viewModel);
            JournalDTO updatedJournal = dtoFactory.CreateDTO<JournalDTOBuilderData, JournalDTO>(builderData);
            service.UpdateYear(updatedJournal);
            await service.SaveChangesAsync();
        }

        public async Task<DeleteViewModel> GetDeleteViewModelAsync(int journalId)
        {
            JournalDTO journal = await service.GetByIdAsync(journalId);
            if (journal == null)
            {
                return null;
            }
            DeleteViewData viewModelData = new DeleteViewData(journal);
            DeleteViewModel viewModel = viewFactory.CreateView<DeleteViewData, DeleteViewModel>(viewModelData);
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

        WorkDaysCreateViewModel IJournalsControllerService.GetCreateWorkDayViewModel(int journalId)
        {
            var viewModel = viewFactory.CreateView<WorkDaysCreateViewModel>();
            return viewModel;
        }

        CreateViewModel IJournalsControllerService.GetCreateViewModel(string mentorId)
        {
            var viewModel = viewFactory.CreateView<CreateViewModel>();
            return viewModel;
        }

    }
}