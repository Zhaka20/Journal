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

namespace Journal.Services.ControllerServices
{
    public class MentorsControllerService : IMentorsControllerService
    {
        protected readonly IMentorDTOService mentorService;
        protected readonly ApplicationUserManager userManager;
        protected readonly IStudentDTOService studentService;
        protected readonly IViewFactory viewFactory;

        public MentorsControllerService(IMentorDTOService service,ApplicationUserManager userManager, IStudentDTOService studentService, IViewFactory viewFactory)
        {
            this.studentService = studentService;
            this.mentorService = service;
            this.userManager = userManager;
            this.viewFactory = viewFactory;
        }

        public async Task<MentorsHomeViewModel> GetHomeViewModelAsync(string mentorId)
        {
            MentorDTO mentor = await mentorService.GetFirstOrDefaultAsync(m => m.Id == mentorId,
                                                                 m => m.Students,
                                                                 m => m.Assignments);

            var pageData = new HomePageData
            {
                Mentor = mentor
            };

            MentorsHomeViewModel viewModel = viewFactory.CreateView<HomePageData, MentorsHomeViewModel>(pageData);
            return viewModel;
        }

        public async Task<MentorsListViewModel> GetMentorsListViewModelAsync()
        {
            var mentors = await mentorService.GetAllAsync();
            MentorsListPageData pageData = new MentorsListPageData
            {
                Mentors = mentors
            };

            MentorsListViewModel viewModel = viewFactory.CreateView<MentorsListPageData, MentorsListViewModel>(pageData);
            return viewModel;
        }

        public async Task<DetailsViewModel> GetDetailsViewModelAsync(string mentorId)
        {
            MentorDTO mentor = await mentorService.GetByIdAsync(mentorId);
            if (mentor == null)
            {
                return null;
            }
            var pageData = new DetailsPageData
            {
                Mentor = mentor
            };


            DetailsViewModel viewModel = viewFactory.CreateView<DetailsPageData, DetailsViewModel>(pageData);
            return viewModel;
        }

        public async Task<AcceptStudentViewModel> GetAcceptStudentViewModelAsync(string mentorId)
        {
            IEnumerable<StudentDTO> students = await studentService.GetAllAsync(s => s.Mentor.Id != mentorId);
            var pageData = new AcceptStudentPageData
            {
                Students = students
            };

            AcceptStudentViewModel viewModel = viewFactory.CreateView<AcceptStudentPageData, AcceptStudentViewModel>(pageData);
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
            var pageData = new ExpelStudentPageData
            {
                Student = student
            };

            ExpelStudentViewModel viewModel = viewFactory.CreateView<ExpelStudentPageData,ExpelStudentViewModel>(pageData)
            return viewModel;
        }

        public async Task RemoveStudentAsync(string studentId,string mentorId)
        {
            await mentorService.RemoveStudentAsync(studentId,mentorId);
            await mentorService.SaveChangesAsync();
        }

        public async Task<MyStudentViewModel> GetStudentViewModelAsync(string studentId)
        {
            StudentDTO student = await studentService.GetFirstOrDefaultAsync(s => s.Id == studentId,
                                                   s => s.Mentor,
                                                   s => s.Submissions.Select(sub => sub.SubmitFile),
                                                   s => s.Submissions.Select(sub => sub.Assignment.AssignmentFile));

            if(student == null)
            {
                return null;
            }

            var pageData = new MyStudentPageData
            {
                Student = student
            };
            MyStudentViewModel viewModel = viewFactory.CreateView<MyStudentPageData, MyStudentViewModel>(pageData);
            return viewModel;
        }

        public CreateViewModel GetCreateMentorViewModel()
        {
            CreateViewModel viewModel = viewFactory.CreateView<CreateViewModel>();
            return viewModel;
        }

        public async Task<IdentityResult> CreateMentorAsync(CreateViewModel viewModel)
        {
            MentorDTO newMenotor = viewModel.ToMentorModel();
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

            var pageData = new EditPageData
            {
                Mentor = mentor
            };
            EditViewModel viewModel = viewFactory.CreateView<EditPageData, EditViewModel>(pageData);
            return viewModel;
        }

        public async Task UpdateMentorAsync(EditViewModel viewModel)
        {
            Mentor newMentor = viewModel.ToMentorModel();
            mentorService.Update(newMentor,  e => e.UserName,
                                       e => e.FirstName,
                                       e => e.LastName,
                                       e => e.PhoneNumber);
            await mentorService.SaveChangesAsync();
        }

        public async Task<DeleteViewModel> GetDeleteViewModel(string id)
        {
            MentorDTO mentor = await mentorService.GetByIdAsync(id);
            if (mentor == null)
            {
                return null;
            }
            var pageData = new DeletePageData
            {
                Mentor = mentor
            };

            DeleteViewModel viewModel = viewFactory.CreateView<DeletePageData, DeleteViewModel>(pageData);
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