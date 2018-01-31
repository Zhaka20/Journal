﻿using Journal.ViewModels.Controller.WorkDays;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Journal.Services.Abstractions
{
    public interface IWorkDaysControllerService : IDisposable
    {
        Task<IndexViewModel> GetWorkDaysIndexViewModel();
        Task<DetailsViewModel> GetWorkDayDetailsViewModelAsync(int workDayId);
        CreateViewModel GetCreateWorkDayViewModel(int journalId);
        Task<int> CreateWorkDayAsync(CreateViewModel inputModel);
        Task<EditViewModel> GetWorkDayEditViewModelAsync(int workDayId);
        Task WorkDayUpdateAsync(EditViewModel inputModel);
        Task<DeleteViewModel> GetWorkDayDeleteViewModelAsync(int id);
        Task WorkDayDeleteAsync(int workDayId);
        Task<AddAttendeesViewModel> GetAddAttendeesViewModelAsync(int workDayId);
        Task AddWorkDayAttendeesAsync(int workDayId, List<string> attendeeIds);
        Task CheckAsLeftAsync(int workDayId, List<int> attendaceIds);
    }
}
