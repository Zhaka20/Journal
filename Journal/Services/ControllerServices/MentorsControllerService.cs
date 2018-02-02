using Journal.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System;
using Journal.AbstractBLL.AbstractServices;
using Journal.BLLtoUIData.DTOs;
using Journal.ViewModels.Controller.Mentors;
using Journal.ViewModels.Shared.EntityViewModels;
using Journal.ViewFactory.Abstractions;
using Journal.WEB.ViewFactory.BuilderInputData.Controllers.Mentors;
using Journal.DTOFactory.Abstractions;
using Journal.DTOBuilderDataFactory.BuilderInputData;

namespace Journal.Services.ControllerServices
{
    public class MentorsControllerService : IMentorsControllerService
    {
        protected readonly IMentorDTOService mentorService;
        protected readonly ApplicationUserManager userManager;
        protected readonly IStudentDTOService studentService;
        protected readonly IViewFactory viewFactory;
        protected readonly IDTOFactory dtoFactory;

        public MentorsControllerService(IMentorDTOService service,ApplicationUserManager userManager, IStudentDTOService studentService, IViewFactory viewFactory,IDTOFactory dtoFactory)
        {
            this.studentService = studentService;
            this.mentorService = service;
            this.userManager = userManager;
            this.viewFactory = viewFactory;
            this.dtoFactory = dtoFactory;
        }

        public async Task<MentorsHomeViewModel> GetHomeViewModelAsync(string mentorId)
        {
            MentorDTO mentor = await mentorService.GetByIdWithStudentsAndAssignmentsAsync(mentorId);

            var viewModelData = new HomePageData(mentor);
            MentorsHomeViewModel viewModel = viewFactory.CreateView<HomePageData, MentorsHomeViewModel>(viewModelData);
            return viewModel;
        }

        public async Task<MentorsListViewModel> GetMentorsListViewModelAsync()
        {
            var mentors = await mentorService.GetAllAsync();
            MentorsListPageData viewModelData = new MentorsListPageData(mentors);
            MentorsListViewModel viewModel = viewFactory.CreateView<MentorsListPageData, MentorsListViewModel>(viewModelData);
            return viewModel;
        }

        public async Task<DetailsViewModel> GetDetailsViewModelAsync(string mentorId)
        {
            MentorDTO mentor = await mentorService.GetByIdAsync(mentorId);
            if (mentor == null)
            {
                return null;
            }
            var viewModelData = new DetailsPageData(mentor);
            DetailsViewModel viewModel = viewFactory.CreateView<DetailsPageData, DetailsViewModel>(viewModelData);
            return viewModel;
        }

        public async Task<AcceptStudentViewModel> GetAcceptStudentViewModelAsync(string mentorId)
        {
            IEnumerable<StudentDTO> students = await studentService.GetNotMyStudents(mentorId);
            var viewModelData = new AcceptStudentPageData(students);
            AcceptStudentViewModel viewModel = viewFactory.CreateView<AcceptStudentPageData, AcceptStudentViewModel>(viewModelData);
            return viewModel;
        }

        public async Task AcceptStudentAsync(string studentId, string mentorId)
        {
            await mentorService.AcceptStudentAsync(studentId, mentorId);
            await mentorService.SaveChangesAsync();
        }

        public async Task<ExpelStudentViewModel> GetExpelStudentViewModelAsync(string studentId)
        {
            StudentDTO student = await studentService.GetByIdAsync(studentId);
            if (student == null)
            {
                return null;
            }
            var viewModelData = new ExpelStudentPageData(student);

            ExpelStudentViewModel viewModel = viewFactory.CreateView<ExpelStudentPageData, ExpelStudentViewModel>(viewModelData);
            return viewModel;
        }

        public async Task RemoveStudentAsync(string studentId,string mentorId)
        {
            await mentorService.RemoveStudentAsync(studentId,mentorId);
            await mentorService.SaveChangesAsync();
        }

        public async Task<MyStudentViewModel> GetStudentViewModelAsync(string studentId)
        {
            StudentDTO student = await studentService.GetByIdAsyncWithMentorSubmissionsFilesAndAssignmentFile(studentId);

            if(student == null)
            {
                return null;
            }

            var viewModelData = new MyStudentPageData(student);
            MyStudentViewModel viewModel = viewFactory.CreateView<MyStudentPageData, MyStudentViewModel>(viewModelData);
            return viewModel;
        }

        public CreateViewModel GetCreateMentorViewModel()
        {
            CreateViewModel viewModel = viewFactory.CreateView<CreateViewModel>();
            return viewModel;
        }

        public async Task<IdentityResult> CreateMentorAsync(CreateViewModel viewModel)
        {
            MentorDTOBuilderData builderData = new MentorDTOBuilderData(viewModel);
            MentorDTO newMenotor = dtoFactory.CreateDTO<MentorDTOBuilderData, MentorDTO>(builderData);
            IdentityResult result = await userManager.CreateAsync(newMenotor, viewModel.Password);
            if (result.Succeeded)
            {
                IdentityResult roleResult = userManager.AddToRole(newMenotor.Id, "Mentor");         
            }
            return result;
        }

        public async Task<EditViewModel> GetEditViewModelAsync(string mentorId)
        {
            MentorDTO mentor = await mentorService.GetByIdAsync(mentorId);
            if (mentor == null)
            {
                return null;
            }

            var viewModelData = new EditPageData(mentor);
            EditViewModel viewModel = viewFactory.CreateView<EditPageData, EditViewModel>(viewModelData);
            return viewModel;
        }

        public async Task UpdateMentorAsync(EditViewModel viewModel)
        {
            MentorDTOBuilderData builderData = new MentorDTOBuilderData(viewModel);
            MentorDTO newMentor = dtoFactory.CreateDTO<MentorDTOBuilderData, MentorDTO>(builderData);
            mentorService.UpdateMentorsBaseData(newMentor);
            await mentorService.SaveChangesAsync();
        }

        public async Task<DeleteViewModel> GetDeleteViewModel(string id)
        {
            MentorDTO mentor = await mentorService.GetByIdAsync(id);
            if (mentor == null)
            {
                return null;
            }
            var viewModelData = new DeletePageData(mentor);
            DeleteViewModel viewModel = viewFactory.CreateView<DeletePageData, DeleteViewModel>(viewModelData);
            return viewModel;
        }


        public async Task DeleteMenotorAsync(string id)
        {
            await mentorService.DeleteByIdAsync(id);
            await mentorService.SaveChangesAsync();
        }

        public void Dispose()
        {
            userManager.Dispose();
            IDisposable dispose = mentorService as IDisposable;
            if(dispose != null)
            {
                dispose.Dispose();
            }
            dispose = studentService as IDisposable;
            if (dispose != null)
            {
                dispose.Dispose();
            }
        }
    }
}