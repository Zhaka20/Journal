using Journal.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Journal.AbstractBLL.AbstractServices;
using Journal.Extensions;
using Microsoft.AspNet.Identity;
using Journal.DataModel.Models;
using System;
using Journal.ViewModels.Controller.Students;

namespace Journal.Services.ControllerServices
{
    public class StudentsControllerService : IStudentsControllerService
    {
        private IStudentService service;
        private ApplicationUserManager userManager;

        public StudentsControllerService(IStudentService service, ApplicationUserManager userManager)
        {
            this.service = service;
            this.userManager = userManager;
        }

        public async Task<IndexViewModel> GetIndexViewModelAsync()
        {
            IEnumerable<Student> students = await service.GetAllAsync();
            IEnumerable<ShowViewModel> studentListVM = students.ToShowStudentVMList();
            IndexViewModel viewModel = new IndexViewModel
            {
                StudentModel = new ShowViewModel(),
                Students = studentListVM
            };
            return viewModel;
        }

        public async Task<HomeViewModel> GetHomeViewModelAsync(string studentId)
        {
            Student student = await service.GetFirstOrDefaultAsync(
                                            s => s.Id == studentId,
                                            s => s.Mentor,
                                            s => s.Submissions.Select(sub => sub.Assignment.AssignmentFile),
                                            s => s.Submissions.Select(sub => sub.SubmitFile)
                                            );

            HomeViewModel viewModel = new HomeViewModel
            {
                Student = student,
                AssignmentModel = new Assignment(),
                SubmissionModel = new Submission(),
                Submissions = student.Submissions
            };
            return viewModel;
        }


        public async Task<DetailsViewModel> GetDetailsViewModelAsync(string studentId)
        {
            Student student = await service.GetByIdAsync(studentId);
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
            Student newStudent = viewModel.ToStudentModel();

            IdentityResult result = await userManager.CreateAsync(newStudent, viewModel.Password);
            if (result.Succeeded)
            {
                IdentityResult roleResult = userManager.AddToRole(newStudent.Id, "Student");
            }
            return result;
        }

        public async Task<EditViewModel> GetEditViewModelAsync(string studentId)
        {
            Student student = await service.GetByIdAsync(studentId);
            if (student == null)
            {
                return null;
            }

            EditViewModel viewModel = student.ToEditStudentVM();
            return viewModel;
        }

        public async Task UpdateAsync(EditViewModel viewModel)
        {
            Student newStudent = viewModel.ToStudentModel();
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
            Student student = await service.GetByIdAsync(studentId);
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