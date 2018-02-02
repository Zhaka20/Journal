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
using Journal.ViewFactory.Abstractions;
using Journal.WEB.ViewFactory.BuilderInputData.Controllers.Assignments;
using Journal.DTOFactory.Abstractions;
using Journal.DTOBuilderDataFactory.BuilderInputData;

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
        protected readonly IViewFactory viewFactory;
        protected readonly IDTOFactory dtoFactory;

        public AssignmentsControllerService(IAssignmentDTOService service, 
                                            IMentorDTOService mentorService,
                                            IStudentDTOService studentService,
                                            IAssignmentFileDTOService fileService,
                                            ISubmissionDTOService submissionService,
                                            IObjectToObjectMapper mapper,
                                            IViewFactory viewFactory,
                                            IDTOFactory dtoFactory)
        {
            this.assignmentService = service;
            this.mentorService = mentorService;
            this.studentService = studentService;
            this.fileService = fileService;
            this.submissionService = submissionService;
            this.mapper = mapper;
            this.viewFactory = viewFactory;
            this.dtoFactory = dtoFactory;
        }


        public async Task<IndexViewModel> GetIndexViewModelAsync()
        {
            IEnumerable<AssignmentDTO> assignments = await assignmentService.GetAllWithAssignmentFileCreatorAndSubmissionsAsync();

            IndexPageData viewModelData = new IndexPageData(assignments);
            IndexViewModel viewModel = viewFactory.CreateView<IndexPageData, IndexViewModel>(viewModelData);
            return viewModel;
        }
        public async Task<DetailsViewModel> GetDetailsViewModelAsync(int assignmentId)
        {
            AssignmentDTO assignment = await assignmentService.GetByIdWithAssignmentFileCreatorAndSubmissionsAsync(assignmentId);
                                                       
            if (assignment == null)
            {
                return null;
            }
            DetailsPageData viewModelData = new DetailsPageData(assignment);
            DetailsViewModel viewModel = viewFactory.CreateView<DetailsPageData, DetailsViewModel>(viewModelData);
            return viewModel;
        }
        public async Task<MentorViewModel> GetMentorAssignmentsViewModelAsync(string mentorId)
        {
            IEnumerable<AssignmentDTO> assignments = await assignmentService.GetByCreatorsIdAsync(mentorId);
            MentorDTO mentor = await mentorService.GetByIdAsync(mentorId);
            MentorPageData viewModelData = new MentorPageData(assignments, mentor);
            MentorViewModel viewModel = viewFactory.CreateView<MentorPageData, MentorViewModel>(viewModelData);
           
            return viewModel;
        }
        public CreateViewModel GetCreateViewModel()
        {
            return new CreateViewModel();
        }
        public async Task<int> CreateAsync(Controller controller, string mentorId, CreateViewModel inputModel, HttpPostedFileBase file)
        {

            AssignmentFileDTO assignmentFile = dtoFactory.CreateDTO<AssignmentFileDTOBuilderData, AssignmentFileDTO>(new AssignmentFileDTOBuilderData(file));
            fileService.Create(assignmentFile);

            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Files/Assignments"), assignmentFile.FileGuid);
            file.SaveAs(path);

            MentorDTO mentor = await mentorService.GetByIdAsync(mentorId);
            AssignmentDTOBuilderData builderData = new AssignmentDTOBuilderData(inputModel,mentorId,assignmentFile);
            AssignmentDTO newAssignment = dtoFactory.CreateDTO<AssignmentDTOBuilderData, AssignmentDTO>(builderData);

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
            var viewModelData = new CreateAndAssignToSingleUserPageData(studentDTO);
            var viewModel = viewFactory.CreateView<CreateAndAssignToSingleUserPageData, CreateAndAssignToSingleUserViewModel>(viewModelData);

            return viewModel;
        }
        public async Task<int> CreateAndAssignToSingleUserAsync(Controller controller, string studentId, CreateViewModel inputModel, HttpPostedFileBase file)
        {

            AssignmentFileDTO assignmentFile = dtoFactory.CreateDTO<AssignmentFileDTOBuilderData, AssignmentFileDTO>(new AssignmentFileDTOBuilderData(file));
            fileService.Create(assignmentFile);

            string path = Path.Combine(controller.Server.MapPath("~/Files/Assignments"), assignmentFile.FileGuid);
            file.SaveAs(path);

            string mentorId = controller.User.Identity.GetUserId();
            MentorDTO mentor = await mentorService.GetByIdAsync(mentorId);
            AssignmentDTOBuilderData builderData = new AssignmentDTOBuilderData(inputModel, mentorId, assignmentFile);
            AssignmentDTO newAssignment = dtoFactory.CreateDTO<AssignmentDTOBuilderData, AssignmentDTO>(builderData);
         
            assignmentService.Create(newAssignment);
            await assignmentService.SaveChangesAsync();

            StudentDTO student = await studentService.GetByIdAsync(studentId);
            if (student == null)
            {
                throw new Exception();
            }

            SubmissionDTOBuilderData bulderData = new SubmissionDTOBuilderData(studentId, newAssignment.AssignmentId, DateTime.Now.AddDays(3));
            SubmissionDTO newSubmission = dtoFactory.CreateDTO<SubmissionDTOBuilderData, SubmissionDTO>(bulderData);
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
            EdtiPageData viewModelData = new EdtiPageData(assignment);
            EdtiViewModel viewModel = viewFactory.CreateView<EdtiPageData, EdtiViewModel>(viewModelData);
            return viewModel;
        }
        public async Task UpdateAsync(EdtiViewModel inputModel)
        {
            AssignmentDTOBuilderData builderData = new AssignmentDTOBuilderData(inputModel);
            AssignmentDTO updatedAssignment = dtoFactory.CreateDTO<AssignmentDTOBuilderData, AssignmentDTO>(builderData);
            assignmentService.UpdateTitle(updatedAssignment);           
            await assignmentService.SaveChangesAsync(); ;
        }
        public async Task<DeleteViewModel> GetDeleteViewModelAsync(int assignmentId)
        {
            AssignmentDTO assignment = await assignmentService.GetByIdAsync(assignmentId);
            if (assignment == null)
            {
                return null;
            }
            DeletePageData viewModelData = new DeletePageData(assignment);
            DeleteViewModel viewModel = viewFactory.CreateView<DeletePageData, DeleteViewModel>(viewModelData);
            return viewModel;
        }
        public async Task DeleteAsync(int assignmentId)
        {
            AssignmentDTO assignment = await assignmentService.GetByIdAsyncWithSubmissionAndFiles(assignmentId);

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
            AssignmentDTO assignment = await assignmentService.GetByIdWithFileAsync(assignmentId);

            var viewModelData = new RemoveStudentPageData(student, assignment);
            var viewModel = viewFactory.CreateView<RemoveStudentPageData, RemoveStudentViewModel>(viewModelData);
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
                        
            IEnumerable<AssignmentDTO> notYetAssigned = assignmentService.GetNotYetAssignedList(mentorId, studentId);

            if (notYetAssigned == null)
            {
                return null;
            }

            var viewModelData = new AssignToStudentPageData(notYetAssigned, student);
            var viewModel = viewFactory.CreateView<AssignToStudentPageData, AssignToStudentViewModel>(viewModelData);
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

            IEnumerable<AssignmentDTO> newAssignmentsList = await assignmentService.GetAllByIdAsync(assignmentIds);
 
            if (newAssignmentsList == null)
            {
                return;
            }

            foreach (AssignmentDTO assignment in newAssignmentsList)
            {
                SubmissionDTOBuilderData bulderData = new SubmissionDTOBuilderData(studentId, assignment.AssignmentId, DateTime.Now.AddDays(3));
                SubmissionDTO newSubmission = dtoFactory.CreateDTO<SubmissionDTOBuilderData, SubmissionDTO>(bulderData);
                assignment.Submissions.Add(newSubmission);
            }

            await assignmentService.SaveChangesAsync();
        }
        public async Task<AssignToStudentsViewModel> GetAssignToStudentsViewModelAsync(int assigmentId)
        {
            AssignmentDTO assignment = await assignmentService.GetByIdWithCreatorAndSubmissionsAsync(assigmentId);
               
            if (assignment == null)
            {
                return null;
            }

            IEnumerable<string> assignedStudentIds = assignment.Submissions.Select(s => s.StudentId);

            IEnumerable<StudentDTO> otherStudents = await studentService.GetAllNotYetAssignedStudentsAsync(assignedStudentIds);

            var viewModelData = new AssignToStudentsPageData(assignment, otherStudents);
            var viewModel = viewFactory.CreateView<AssignToStudentsPageData, AssignToStudentsViewModel>(viewModelData);
            return viewModel;
        }
        public async Task AssignToStudentsAsync(int assigmentId, List<string> studentIds)
        {
            if (studentIds == null)
            {
                throw new ArgumentNullException("studentIds");
            }
            AssignmentDTO assignment = await assignmentService.GetByIdWithFileAsync(assigmentId);
            if (assignment == null)
            {
                throw new KeyNotFoundException();
            }

            IEnumerable<StudentDTO> students = await studentService.GetAllByIdAsync(studentIds);

            foreach (StudentDTO student in students)
            {
                SubmissionDTOBuilderData bulderData = new SubmissionDTOBuilderData(student.Id, assignment.AssignmentId, DateTime.Now.AddDays(3));
                SubmissionDTO newSubmission = dtoFactory.CreateDTO<SubmissionDTOBuilderData, SubmissionDTO>(bulderData);

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
                    FileStream = File.ReadAllBytes(filePath),
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

        private void DeleteFile(FileInfoDTO file)
        {
            if (file == null) return;

            string fullPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Files/Assignments"), file.FileGuid);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        public async Task<StudentsAndSubmissionsListViewModel> GetStudentsAndSubmissionsListViewModelAsync(int assingmentId)
        {
            AssignmentDTO assignment = await assignmentService.GetByIdIncludeAssingmentFileCreatorSubmissionStudentAsync(assingmentId);
               
            if (assignment == null)
            {
                return null;
            }

            var viewModelData = new StudentsAndSubmissionsListPageData(assignment);
            var viewModel = viewFactory.CreateView<StudentsAndSubmissionsListPageData, StudentsAndSubmissionsListViewModel>(viewModelData);
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