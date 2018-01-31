using Journal.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Journal.AbstractBLL.AbstractServices;
using Journal.ViewModels.Controller.Journals;
using Journal.BLLtoUIData.DTOs;
using Journal.ViewModels.Shared.EntityViewModels;
using Journal.ViewFactory.Abstractions;
using Journal.WEB.ViewFactory.BuilderInputData.Controllers.Journal;

namespace Journal.Services.ControllerServices
{
    public class JournalsControllerService : IJournalsControllerService
    {
        protected readonly IJournalDTOService service;
        protected readonly IViewFactory viewFactory;

        public JournalsControllerService(IJournalDTOService service, IViewFactory viewFactor)
        {
            this.viewFactory = viewFactor;
            this.service = service;
        }

        public async Task<FillViewModel> GetFillViewModelAsync(int journalId)
        {
            JournalDTO journal = await service.GetFirstOrDefaultAsync(j => j.Id == journalId,
                                                                   a => a.WorkDays,
                                                                   b => b.Mentor);
            if (journal == null)
            {
                return null;
            }
            var pageData = new FillPageData
            {
                Journal = journal
            };
            FillViewModel viewModel = viewFactory.CreateView<FillPageData,FillViewModel>(pageData);

            return viewModel;
        }

        public async Task CreateWorkDayAsync(ViewModels.Controller.WorkDays.CreateViewModel viewModel)
        {
            JournalDTO journal = await service.GetByIdAsync(viewModel.JournalId);
            WorkDayDTO newWorkDay = new WorkDayDTO
            {
                JournalId = viewModel.JournalId,
                Day = viewModel.Day
            };
            journal.WorkDays.Add(newWorkDay);
            await service.SaveChangesAsync();
        }

        public async Task<IndexViewModel> GetJournalsIndexViewModelAsync()
        {
            IEnumerable<JournalDTO> journals = await service.GetAllAsync(includeProperties: j => j.Mentor);
            IndexPageData pageData = new IndexPageData
            {
                Journals = journals
            };
            IndexViewModel viewModel = viewFactory.CreateView<IndexPageData, IndexViewModel>(pageData);
            return viewModel;
        }

        public async Task<DetailsViewModel> GetJournalDetailsViewModelAsync(int journalId)
        {
            JournalDTO journal = await service.GetFirstOrDefaultAsync(j => j.Id == journalId,
                                                                   j => j.Mentor);

            if (journal == null)
            {
                return null;
            }
            var pageData = new DetailsPageData
            {
                Journal = journal
            };
            DetailsViewModel viewModel = viewFactory.CreateView<DetailsPageData, DetailsViewModel>(pageData);
            return viewModel;
        }

        public async Task<int> CreateJournalAsync(CreateViewModel viewModel)
        {
            JournalDTO newJournal = new JournalDTO
            {
                Year = viewModel.Year,
                MentorId = viewModel.MentorId,
            };

            service.Create(newJournal);
            await service.SaveChangesAsync();
            return newJournal.Id;
        }

        public async Task<EditViewModel> GetEditJournalViewModelAsync(int journalId)
        {
            JournalDTO journal = await service.GetFirstOrDefaultAsync(j => j.Id == journalId,
                                                                   j => j.Mentor);
            if (journal == null)
            {
                return null;
            }

            EditPageData pageData = new EditPageData
            {
                Journal = journal
            };
            EditViewModel viewModel = viewFactory.CreateView<EditPageData, EditViewModel>(pageData);
            return viewModel;
        }

        public async Task UpdateJournalAsync(EditViewModel viewModel)
        {
            JournalDTO updatedJournal = new JournalDTO
            {
                Id = viewModel.Id,
                Year = viewModel.Year,
            };
            service.Update(updatedJournal, j => j.Year);
            await service.SaveChangesAsync();
        }

        public async Task<DeleteViewModel> GetDeleteJournalViewModelAsync(int journalId)
        {
            JournalDTO journal = await service.GetByIdAsync(journalId);
            if (journal == null)
            {
                return null;
            }
            DeletePageData pageData = new DeletePageData
            {
                Journal = journal
            };
            DeleteViewModel viewModel = viewFactory.CreateView<DeletePageData, DeleteViewModel>(pageData);
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

        ViewModels.Controller.WorkDays.CreateViewModel GetCreateWorkDayViewModel(int journalId)
        {
            var viewModel = viewFactory.CreateView<ViewModels.Controller.WorkDays.CreateViewModel>();
            return viewModel;
        }

        CreateViewModel GetCreateJournalViewModel(string mentorId)
        {
            var viewModel = viewFactory.CreateView<CreateViewModel>();
            return viewModel;
        }
    }
}