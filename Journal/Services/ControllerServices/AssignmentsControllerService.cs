using Journal.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.IO;
using Microsoft.AspNet.Identity;
using Journal.ViewModels.Controller.Assignments;
using Journal.AbstractBLL.AbstractServices;
using Journal.BLLtoUIData.DTOs;
using Journal.WEB.Services.Common;
using Journal.ViewModels.Shared.EntityViewModels;

namespace Journal.Services.ControllerServices
{
    public class AssignmentsControllerService : IAssignmentsControllerService
    {
        protected readonly IAssignmentDTOService assignmentService;
        protected readonly IMentorDTOService mentorService;
        protected readonly IStudentDTOService studentService;
        protected readonly IAssignmentFileDTOService fileService;
        protected readonly ISubmissionDTOService submissionService;
        protected readonly IObjectToObjectMapper mapper;

        public AssignmentsControllerService(IAssignmentDTOService service, 
                                            IMentorDTOService mentorService,
                                            IStudentDTOService studentService,
                                            IAssignmentFileDTOService fileService,
                                            ISubmissionDTOService submissionService,
                                            IObjectToObjectMapper mapper)
        {
            this.assignmentService = service;
            this.mentorService = mentorService;
            this.studentService = studentService;
            this.fileService = fileService;
            this.submissionService = submissionService;
            this.mapper = mapper
        }


        public async Task<IndexViewModel> GetIndexViewModelAsync()
        {
            IEnumerable<AssignmentDTO> assignments = await assignmentService.GetAllAsync(null, null, null, null,
                                                                             a => a.AssignmentFile,
                                                                             a => a.Creator,
                                                                             a => a.Submissions);

            IndexViewModel viewModel = new IndexViewModel
            {
                Assignments = assignments,
                AssignmentModel = new Assignment()
            };
            return viewModel;
        }
        public async Task<DetailsViewModel> GetDetailsViewModelAsync(int assignmentId)
        {
            AssignmentDTO assignment = await assignmentService.GetFirstOrDefaultAsync(s => s.AssignmentId == assignmentId,
                                                                              s => s.AssignmentFile,
                                                                              s => s.Creator,
                                                                              s => s.Submissions);
                                        
            if (assignment == null)
            {
                return null;
            }
            DetailsViewModel viewModel = new DetailsViewModel
            {
                Assignment = assignment
            };
            return viewModel;
        }
        public async Task<MentorViewModel> GetMentorAssignmentsViewModelAsync(string mentorId)
        {
            IEnumerable<AssignmentDTO> assignments = await assignmentService.GetAllAsync(a => a.CreatorId == mentorId);
            Mentor mentor = await mentorService.GetByIdAsync(mentorId);
            MentorViewModel viewModel = new MentorViewModel
            {
                Assignments = assignments,
                Mentor = mentor
            };
            return viewModel;
        }
        public CreateViewModel GetCreateViewModel()
        {
            return new CreateViewModel();
        }
        public async Task<int> CreateAsync(Controller controller, string mentorId, CreateViewModel inputModel, HttpPostedFileBase file)
        {

            AssignmentFileDTO assignmentFile = new AssignmentFileDTO
            {
                FileName = file.FileName,
                FileGuid = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName)
            };
            fileService.Create(assignmentFile);

            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Files/Assignments"), assignmentFile.FileGuid);
            file.SaveAs(path);

            MentorDTO mentor = await mentorService.GetByIdAsync(mentorId);
            AssignmentDTO newAssignment = new AssignmentDTO
            {
                Title = inputModel.Title,
                Created = DateTime.Now,
                CreatorId = mentor.Id,
                AssignmentFile = assignmentFile,
            };
            //fileService.SaveChanges();
            assignmentService.Create(newAssignment);
            await assignmentService.SaveChangesAsync();
            return newAssignment.AssignmentId;
        }
        public async Task<CreateAndAssignToSingleUserViewModel> GetCreateAndAssignToSingleUserViewModelAsync(string studentId)
        {
            StudentDTO studentDTO = await studentService.GetStudentByEmailAsync(studentId);
            if (studentDTO == null)
            {
                return null;
            }
            var studentViewModel = studentDTO;
            CreateAndAssignToSingleUserViewModel viewModel = new CreateAndAssignToSingleUserViewModel
            {
                Student = studentViewModel,
                Title = string.Empty
            };
            return viewModel;
        }
        public async Task<int> CreateAndAssignToSingleUserAsync(Controller controller, string studentId, CreateViewModel inputModel, HttpPostedFileBase file)
        {

            AssignmentFileDTO assignmentFile = new AssignmentFileDTO
            {
                FileName = file.FileName,
                FileGuid = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName)
            };
            fileService.Create(assignmentFile);

            string path = Path.Combine(controller.Server.MapPath("~/Files/Assignments"), assignmentFile.FileGuid);
            file.SaveAs(path);

            string mentorId = controller.User.Identity.GetUserId();
            MentorDTO mentor = await mentorService.GetByIdAsync(mentorId);
            AssignmentDTO newAssignment = new AssignmentDTO
            {
                Title = inputModel.Title,
                Created = DateTime.Now,
                CreatorId = mentor.Id,
                AssignmentFile = assignmentFile,
            };

            assignmentService.Create(newAssignment);
            await assignmentService.SaveChangesAsync();

            StudentDTO student = await studentService.GetByIdAsync(studentId);
            if (student == null)
            {
                throw new Exception();
            }

            SubmissionDTO newSubmission = new SubmissionDTO
            {
                StudentId = student.Id,
                AssignmentId = newAssignment.AssignmentId,
                DueDate = DateTime.Now.AddDays(3)
            };

            student.Submissions.Add(newSubmission);

            await assignmentService.SaveChangesAsync();
            return newAssignment.AssignmentId;

        }
        public async Task<EdtiViewModel> GetEdtiViewModelAsync(int assignmentId)
        {
            AssignmentDTO assignment = await assignmentService.GetByIdAsync(assignmentId);
            if (assignment == null)
            {
                return null;
            }
            EdtiViewModel viewModel = new EdtiViewModel
            {
                Title = assignment.Title,
                AssignmentId = assignment.AssignmentId
            };
            return viewModel;
        }
        public async Task UpdateAsync(EdtiViewModel inputModel)
        {
            AssignmentDTO updatedAssignment = new AssignmentDTO
            {
                AssignmentId = inputModel.AssignmentId
            };
            assignmentService.Update(updatedAssignment, a => a.Title);           
            await assignmentService.SaveChangesAsync(); ;
        }
        public async Task<DeleteViewModel> GetDeleteViewModelAsync(int assignmentId)
        {
            AssignmentDTO assignment = await assignmentService.GetByIdAsync(assignmentId);
            if (assignment == null)
            {
                return null;
            }
            DeleteViewModel viewModel = new DeleteViewModel
            {
                Assignment = assignment
            };
            return viewModel;
        }
        public async Task DeleteAsync(int assignmentId)
        {
            AssignmentDTO assignment = await assignmentService.GetFirstOrDefaultAsync(a => a.AssignmentId == assignmentId,
                                                                         a => a.AssignmentFile,
                                                                         a => a.Submissions.Select(s => s.SubmitFile));

            foreach (SubmissionDTO submission in assignment.Submissions)
            {
                DeleteFile(submission.SubmitFile);
            }
            DeleteFile(assignment.AssignmentFile);

            assignmentService.Delete(assignment);
            await assignmentService.SaveChangesAsync();
        }
        public async Task<RemoveStudentViewModel> GetRemoveStudentViewModelAsync(int assignmentId, string studentId)
        {
            StudentDTO student = await studentService.GetByIdAsync(studentId);
            AssignmentDTO assignment = await assignmentService.GetFirstOrDefaultAsync(a => a.AssignmentId == assignmentId,
                                                                         a => a.AssignmentFile);

            RemoveStudentViewModel viewModel = new RemoveStudentViewModel
            {
                Assignment = assignment,
                Student = student
            };
            return viewModel;
        }
        public async Task RemoveStudentFromAssignmentAsync(int assignmentId, string studentId)
        {

            SubmissionDTO submission = await submissionService.GetByCompositeKeysAsync(assignmentId, studentId);
            if (submission != null)
            {
                submissionService.Delete(submission);
            }
            await submissionService.SaveChangesAsync();
        }
        public async Task<AssignToStudentViewModel> GetAssignToStudentViewModelAsync(string studentId)
        {
            StudentDTO student = await studentService.GetByIdAsync(studentId);
            if (student == null)
            {
                return null;
            }

            string mentorId = HttpContext.Current.User.Identity.GetUserId();

            IEnumerable<AssignmentDTO> assignmentsOfThisMentor = await assignmentService.GetAllAsync(a => a.CreatorId == mentorId, null,null,null,
                                                                                  a => a.Creator,
                                                                                  a => a.Submissions.Select(s => s.Student));

            IEnumerable<SubmissionDTO> studentsSubmissions = await submissionService.GetAllAsync(s => s.StudentId == studentId);
            var studentsAssignmentIds = studentsSubmissions.Select(s => s.AssignmentId);
                        
            IEnumerable<AssignmentDTO> notYetAssigned = assignmentsOfThisMentor.Where(a => !studentsAssignmentIds.Contains(a.AssignmentId));

            if (notYetAssigned == null)
            {
                return null;
            }

            AssignToStudentViewModel viewModel = new AssignToStudentViewModel
            {
                Assignments = notYetAssigned,
                Student = student,
                AssignmentModel = new Assignment()
            };
            return viewModel;
        }
        public async Task AssignToStudentAsync(string studentId, List<int> assignmentIds)
        {
            if (studentId == null || assignmentIds == null)
            {
                throw new ArgumentNullException();
            }

            StudentDTO student = await studentService.GetByIdAsync(studentId);
            if (student == null)
            {
                return;
            }

            IEnumerable<AssignmentDTO> newAssignmentsList = await assignmentService.GetAllAsync(a => assignmentIds.Contains(a.AssignmentId));
 
            if (newAssignmentsList == null)
            {
                return;
            }

            foreach (AssignmentDTO assignment in newAssignmentsList)
            {
                SubmissionDTO newSubmission = new SubmissionDTO
                {
                    StudentId = student.Id,
                    AssignmentId = assignment.AssignmentId,
                    DueDate = DateTime.Now.AddDays(3)
                };
                assignment.Submissions.Add(newSubmission);
            }

            await assignmentService.SaveChangesAsync();
        }
        public async Task<AssignToStudentsViewModel> GetAssignToStudentsViewModelAsync(int assigmentId)
        {
            AssignmentDTO assignment = await assignmentService.GetFirstOrDefaultAsync(s => s.AssignmentId == assigmentId,
                                                                         a => a.Creator,
                                                                         a => a.Submissions);
               
            if (assignment == null)
            {
                return null;
            }

            IEnumerable<string> assignedStudentIds = assignment.Submissions.Select(s => s.StudentId);

            IEnumerable<StudentDTO> otherStudents = await studentService.GetAllAsync(s => !assignedStudentIds.Contains(s.Id));

            AssignToStudentsViewModel viewModel = new AssignToStudentsViewModel
            {
                Assignment = assignment,
                StudentModel = new Student(),
                Students = otherStudents
            };
            return viewModel;
        }
        public async Task AssignToStudentsAsync(int assigmentId, List<string> studentIds)
        {
            if (studentIds == null)
            {
                throw new ArgumentNullException("studentIds");
            }
            AssignmentDTO assignment = await assignmentService.GetFirstOrDefaultAsync(s => s.AssignmentId == assigmentId,
                                                                   a => a.AssignmentFile);
            if (assignment == null)
            {
                throw new KeyNotFoundException();
            }

            IEnumerable<StudentDTO> students = await studentService.GetAllAsync(s => studentIds.Contains(s.Id));

            foreach (StudentDTO student in students)
            {
                SubmissionDTO newSubmission = new SubmissionDTO
                {
                    StudentId = student.Id,
                    AssignmentId = (int)assigmentId,
                    DueDate = DateTime.Now.AddDays(3)
                };

                student.Submissions.Add(newSubmission);
            }

            await assignmentService.SaveChangesAsync();
        }
        public async Task<IFileStreamWithInfo> GetAssignmentFileAsync(int assignmentId)
        {
            AssignmentDTO assignment = await assignmentService.GetByIdAsync(assignmentId);
            if (assignment == null || assignment.AssignmentFile == null)
            {
                return null;
            }
            string origFileName = assignment.AssignmentFile.FileName;
            string fileGuid = assignment.AssignmentFile.FileGuid;
            string mimeType = MimeMapping.GetMimeMapping(origFileName);
            IFileStreamWithInfo fileStream = null;
            try
            {
                string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Files/Assignments"), fileGuid);
                fileStream = new FileStreamWithInfo
                {
                    FileStream = System.IO.File.ReadAllBytes(filePath),
                    FileName = origFileName,
                    FileType = mimeType
                };
            }
            catch (Exception )
            {
                return null;
            }
            return fileStream;
        }
        public void Dispose()
        {
            IDisposable dispose = studentService as IDisposable;
            dispose = studentService as IDisposable;
            if (dispose != null)
            {
                dispose.Dispose();
            }

            dispose = mentorService as IDisposable;
            if (dispose != null)
            {
                dispose.Dispose();
            }

            dispose = fileService as IDisposable;
            if (dispose != null)
            {
                dispose.Dispose();
            }

            dispose = assignmentService as IDisposable;
            if (dispose != null)
            {
                dispose.Dispose();
            }
        }

        private void DeleteFile(Models.FileInfo file)
        {
            if (file == null) return;

            string fullPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Files/Assignments"), file.FileGuid);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }

        public async Task<StudentsAndSubmissionsListViewModel> GetStudentsAndSubmissionsListViewModelAsync(int assingmentId)
        {
            AssignmentDTO assignment = await assignmentService.GetFirstOrDefaultAsync(s => s.AssignmentId == assingmentId,
                                                                         a => a.AssignmentFile,  
                                                                         a => a.Creator,         
                                                                         a => a.Submissions.Select(s => s.Student));
                                            
            if (assignment == null)
            {
                return null;
            }

            StudentsAndSubmissionsListViewModel viewModel = new StudentsAndSubmissionsListViewModel
            {
                Assignment = assignment,
                StudentModel = new Student(),
                Submissions = assignment.Submissions

            };
            return viewModel;
        }

        public class FileStreamWithInfo : IFileStreamWithInfo
        {
            public string FileName { get; set; }
            public byte[] FileStream { get; set; }
            public string FileType { get; set; }
        }
    }
}