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
using Journal.DTOFactory.Abstractions;
using Journal.DTOBuilderDataFactory.BuilderInputData;

namespace Journal.Services.ControllerServices
{
    public class StudentsControllerService : IStudentsControllerService
    {
        protected readonly IStudentDTOService studentService;
        protected readonly ApplicationUserManager userManager;
        protected readonly IViewFactory viewFactory;
        protected readonly IDTOFactory dtoFactory;

        public StudentsControllerService(IStudentDTOService studentService, ApplicationUserManager userManager, IViewFactory viewFactory, IDTOFactory dtoFactory)
        {
            this.studentService = studentService;
            this.userManager = userManager;
            this.viewFactory = viewFactory;
            this.dtoFactory = dtoFactory;
        }

        public async Task<IndexViewModel> GetIndexViewModelAsync()
        {
            IEnumerable<StudentDTO> students = await studentService.GetAllAsync();
            var pageData = new IndexPageData(students);
            IndexViewModel viewModel = viewFactory.CreateView<IndexPageData, IndexViewModel>(pageData);
            return viewModel;
        }

        public async Task<HomeViewModel> GetHomeViewModelAsync(string studentId)
        {
            StudentDTO student = await studentService.GetByIdAsyncWithMentorSubmissionsFilesAndAssignmentFile(studentId);

            var pageData = new HomePageData(student);
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
            var viewModelData = new DetailsPageData(student);
            
            DetailsViewModel viewModel = viewFactory.CreateView<DetailsPageData, DetailsViewModel>(viewModelData);
            return viewModel;
        }

        public CreateViewModel GetCreateViewModel()
        {
            CreateViewModel viewModel = viewFactory.CreateView<CreateViewModel>();
            return viewModel;
        }

        public async Task<IdentityResult> CreateAsync(CreateViewModel viewModel)
        {
            StudentDTOBuilderData builderData = new StudentDTOBuilderData(viewModel);
            StudentDTO newStudent = dtoFactory.CreateDTO<StudentDTOBuilderData, StudentDTO>(builderData);

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
            var pageData = new EditPageData(student);
            EditViewModel viewModel = viewFactory.CreateView<EditPageData, EditViewModel>(pageData);
            return viewModel;
        }

        public async Task UpdateAsync(EditViewModel viewModel)
        {
            StudentDTOBuilderData builderData = new StudentDTOBuilderData(viewModel);
            StudentDTO newStudent = dtoFactory.CreateDTO<StudentDTOBuilderData, StudentDTO>(builderData);
            studentService.UpdateStudentsBaseInfo(newStudent);
            await studentService.SaveChangesAsync();
        }

        public async Task<DeleteViewModel> GetDeleteViewModelAsync(string studentId)
        {
            StudentDTO student = await studentService.GetByIdAsync(studentId);
            if (student == null)
            {
                return null;
            }
            var pageData = new DeletePageData(student);
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