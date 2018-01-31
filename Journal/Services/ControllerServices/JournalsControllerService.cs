using Journal.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Journal.AbstractBLL.AbstractServices;
using Journal.ViewModels.Controller.Journals;
using Journal.BLLtoUIData.DTOs;
using Journal.ViewModels.Shared.EntityViewModels;

namespace Journal.Services.ControllerServices
{
    public class JournalsControllerService : IJournalsControllerService
    {
        protected readonly IJournalDTOService service;

        public JournalsControllerService(IJournalDTOService service)
        {
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
            FillViewModel viewModel = new FillViewModel
            {
                Journal = journal,
                WorkDayModel = new WorkDayViewModel()
            };

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
            IndexViewModel viewModel = new IndexViewModel
            {
                Journals = journals,
                JournalModel = new JournalViewModel(),
                MentorModel = new MentorViewModel()
            };
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
            DetailsViewModel viewModel = new DetailsViewModel
            {
                Journal = journal
            };
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

            EditViewModel viewModel = new EditViewModel
            {
                Year = journal.Year,
                Id = journal.Id
            };
            return viewModel;
        }

        public async Task UpdateJournalAsync(ViewModels.Journals.EditViewModel viewModel)
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
            DeleteViewModel viewModel = new DeleteViewModel
            {
                Journal = journal
            };
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
            ViewModels.Controller.WorkDays.CreateViewModel viewModel = new ViewModels.Controller.WorkDays.CreateViewModel
            {
                Day = DateTime.Now,
                JournalId = journalId
            };
            return viewModel;
        }

        CreateViewModel GetCreateJournalViewModel(string mentorId)
        {
            CreateViewModel viewModel = new CreateViewModel
            {
                MentorId = mentorId,
                Year = DateTime.Now.Year
            };
            return viewModel;
        }
    }
}