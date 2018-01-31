using Journal.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Journal.AbstractBLL.AbstractServices;
using Microsoft.AspNet.Identity;
using System;
using Journal.ViewModels.Controller.Students;
using Journal.BLLtoUIData.DTOs;
using Journal.ViewModels.Shared.EntityViewModels;

namespace Journal.Services.ControllerServices
{
    public class StudentsControllerService : IStudentsControllerService
    {
        private IStudentDTOService service;
        private ApplicationUserManager userManager;

        public StudentsControllerService(IStudentDTOService service, ApplicationUserManager userManager)
        {
            this.service = service;
            this.userManager = userManager;
        }

        public async Task<IndexViewModel> GetIndexViewModelAsync()
        {
            IEnumerable<StudentDTO> students = await service.GetAllAsync();
            IEnumerable<StudentViewModel> studentListVM = students.ToShowStudentVMList();
            IndexViewModel viewModel = new IndexViewModel
            {
                StudentModel = new ShowViewModel(),
                Students = studentListVM
            };
            return viewModel;
        }

        public async Task<HomeViewModel> GetHomeViewModelAsync(string studentId)
        {
            StudentDTO student = await service.GetFirstOrDefaultAsync(
                                            s => s.Id == studentId,
                                            s => s.Mentor,
                                            s => s.Submissions.Select(sub => sub.Assignment.AssignmentFile),
                                            s => s.Submissions.Select(sub => sub.SubmitFile)
                                            );

            HomeViewModel viewModel = new HomeViewModel
            {
                Student = student,
                AssignmentModel = new Assignment(),
                SubmissionModel = new SubmissionViewModel(),
                Submissions = student.Submissions
            };
            return viewModel;
        }


        public async Task<DetailsViewModel> GetDetailsViewModelAsync(string studentId)
        {
            StudentDTO student = await service.GetByIdAsync(studentId);
            if (student == null)
            {
                return null;
            }
            DetailsViewModel viewModel = student.ToStudentDetailsVM();
            return viewModel;
        }

        public CreateViewModel GetCreateViewModel()
        {
            CreateViewModel viewModel = new CreateViewModel();
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
            StudentDTO student = await service.GetByIdAsync(studentId);
            if (student == null)
            {
                return null;
            }

            EditViewModel viewModel = student.ToEditStudentVM();
            return viewModel;
        }

        public async Task UpdateAsync(EditViewModel viewModel)
        {
            StudentDTO newStudent = viewModel.ToStudentModel();
            service.Update(newStudent,
                           e => e.Email,
                           e => e.UserName,
                           e => e.FirstName,
                           e => e.LastName,
                           e => e.PhoneNumber
                         );
            await service.SaveChangesAsync();
        }

        public async Task<DeleteViewModel> GetDeleteViewModelAsync(string studentId)
        {
            StudentDTO student = await service.GetByIdAsync(studentId);
            if (student == null)
            {
                return null;
            }

            DeleteViewModel viewModel = student.ToDeleteStudentVM();
            return viewModel;
        }

        public async Task DeleteAsync(string studentId)
        {
            await service.DeleteByIdAsync(studentId);
            await service.SaveChangesAsync();
        }

        public void Dispose()
        {
            IDisposable dispose = service as IDisposable;
            if(dispose != null)
            {
                dispose.Dispose();
            }
            userManager.Dispose();
        }        
    }
}