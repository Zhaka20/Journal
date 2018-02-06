﻿using Journal.ViewModels.Controller.Mentors;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace Journal.Services.Abstractions
{
    public interface IMentorsControllerService : IDisposable
    {
        Task<MentorsHomeViewModel> GetHomeViewModelAsync(string mentorId);
        Task<MentorsListViewModel> GetMentorsListViewModelAsync();
        Task<DetailsViewModel> GetDetailsViewModelAsync(string mentorId);
        Task<AcceptStudentViewModel> GetAcceptStudentViewModelAsync(string mentorId);
        Task AcceptStudentAsync(string studentId,string mentorId);
        Task<ExpelStudentViewModel> GetExpelStudentViewModelAsync(string studentId);
        Task RemoveStudentAsync(string studentId,string mentorId);
        Task<MyStudentViewModel> GetStudentViewModelAsync(string id);
        CreateViewModel GetCreateMentorViewModel();
        Task<IdentityResult> CreateMentorAsync(CreateViewModel viewModel);
        Task<EditViewModel> GetEditViewModelAsync(string id);
        Task UpdateMentorAsync(EditViewModel viewModel);
        Task<DeleteViewModel> GetDeleteViewModel(string id);
        Task DeleteMenotorAsync(string id);

    }
}
