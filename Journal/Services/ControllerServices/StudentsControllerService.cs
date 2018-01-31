using Journal.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Journal.AbstractBLL.AbstractServices;
using Microsoft.AspNet.Identity;
using System;
using Journal.ViewModels.Controller.Students;
using Journal.BLLtoUIData.DTOs;
using Journal.ViewFactory.Abstractions;
using Journal.WEB.ViewFactory.BuilderInputData.Controllers.Students;

namespace Journal.Services.ControllerServices
{
    public class StudentsControllerService : IStudentsControllerService
    {
        protected readonly IStudentDTOService studentService;
        protected readonly ApplicationUserManager userManager;
        protected readonly IViewFactory viewFactory;

        public StudentsControllerService(IStudentDTOService studentService, ApplicationUserManager userManager, IViewFactory viewFactory)
        {
            this.studentService = studentService;
            this.userManager = userManager;
            this.viewFactory = viewFactory;
        }

        public async Task<IndexViewModel> GetIndexViewModelAsync()
        {
            IEnumerable<StudentDTO> students = await studentService.GetAllAsync();
            var pageData = new IndexPageData
            {
                Students = students
            };

            IndexViewModel viewModel = viewFactory.CreateView<IndexPageData, IndexViewModel>(pageData);
            return viewModel;
        }

        public async Task<HomeViewModel> GetHomeViewModelAsync(string studentId)
        {
            StudentDTO student = await studentService.GetFirstOrDefaultAsync(
                                            s => s.Id == studentId,
                                            s => s.Mentor,
                                            s => s.Submissions.Select(sub => sub.Assignment.AssignmentFile),
                                            s => s.Submissions.Select(sub => sub.SubmitFile)
                                            );

            var pageData = new HomePageData
            {
                Student = student,
                Submissions = student.Submissions
            };

            HomeViewModel viewModel = viewFactory.CreateView<HomePageData, HomeViewModel>(pageData);
            return viewModel;
        }


        public async Task<DetailsViewModel> GetDetailsViewModelAsync(string studentId)
        {
            StudentDTO student = await studentService.GetByIdAsync(studentId);
            if (student == null)
            {
                return null;
            }
            var pageData = new DetailsPageData
            {
                Student = student
            };

            DetailsViewModel viewModel = viewFactory.CreateView<DetailsPageData, DetailsViewModel>(pageData);
            return viewModel;
        }

        public CreateViewModel GetCreateViewModel()
        {
            CreateViewModel viewModel = viewFactory.CreateView<CreateViewModel>();
            return viewModel;
        }

        public async Task<IdentityResult> CreateAsync(CreateViewModel viewModel)
        {
            StudentDTO newStudent = viewModel.ToStudentModel();

            IdentityResult result = await userManager.CreateAsync(newStudent, viewModel.Password);
            if (result.Succeeded)
            {
                IdentityResult roleResult = userManager.AddToRole(newStudent.Id, "Student");
            }
            return result;
        }

        public async Task<EditViewModel> GetEditViewModelAsync(string studentId)
        {
            StudentDTO student = await studentService.GetByIdAsync(studentId);
            if (student == null)
            {
                return null;
            }
            var pageData = new EditPageData
            {
                Student = student
            };
            EditViewModel viewModel = viewFactory.CreateView<EditPageData, EditViewModel>(pageData);
            return viewModel;
        }

        public async Task UpdateAsync(EditViewModel viewModel)
        {
            StudentDTO newStudent = viewModel.ToStudentModel();
            studentService.Update(newStudent,
                           e => e.Email,
                           e => e.UserName,
                           e => e.FirstName,
                           e => e.LastName,
                           e => e.PhoneNumber
                         );
            await studentService.SaveChangesAsync();
        }

        public async Task<DeleteViewModel> GetDeleteViewModelAsync(string studentId)
        {
            StudentDTO student = await studentService.GetByIdAsync(studentId);
            if (student == null)
            {
                return null;
            }
            var pageData = new DeletePageData
            {
                Student = student
            };
            DeleteViewModel viewModel = viewFactory.CreateView<DeletePageData, DeleteViewModel>(pageData);
            return viewModel;
        }

        public async Task DeleteAsync(string studentId)
        {
            await studentService.DeleteByIdAsync(studentId);
            await studentService.SaveChangesAsync();
        }

        public void Dispose()
        {
            IDisposable dispose = studentService as IDisposable;
            if (dispose != null)
            {
                dispose.Dispose();
            }
            userManager.Dispose();
        }
    }
}