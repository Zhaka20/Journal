using Journal.Services.Abstractions;
using System;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.IO;
using Journal.AbstractBLL.AbstractServices;
using System.Collections.Generic;
using Journal.BLLtoUIData.DTOs;
using Journal.ViewModels.Controller.Submissions;
using Journal.ViewModels.Shared.EntityViewModels;
using Journal.WEB.ViewFactory.BuilderInputData.Controllers.Submissions;
using Journal.ViewFactory.Abstractions;
using Journal.ViewFactory.BuilderInputData.Controllers.Submissions;
//using Journal.ViewFactory.BuilderInputData.Controllers.Submissions;

namespace Journal.Services.ControllerServices
{
    public class SubmissionsControllerService : ISubmissionsControllerService
    {
        protected readonly ISubmissionDTOService service;
        protected readonly IAssignmentDTOService assigmentService;
        protected readonly IViewFactory viewFactory;

        public SubmissionsControllerService(ISubmissionDTOService submissionService,
                                            IAssignmentDTOService assignmentService,
                                            IViewFactory viewFactory
                                            )
        {
            this.viewFactory = viewFactory;
            this.service = submissionService;
            this.assigmentService = assignmentService;
        }
        public async Task<IndexViewModel> GetIndexViewModelAsync()
        {
            IEnumerable<SubmissionDTO> submissions = await service.GetAllAsync(null, null, null, null,
                                                                            s => s.Assignment,
                                                                            s => s.Student,
                                                                            s => s.SubmitFile);

            var pageData = new IndexPageData
            {
                Submissions = submissions
            };
            IndexViewModel viewModel = viewFactory.CreateView<IndexPageData, IndexViewModel>(pageData);
            return viewModel;
        }

        public async Task<AssignmentSubmissionsViewModel> GetAssignmentSubmissionsViewModelAsync(int assignmentId)
        {
            AssignmentDTO assignment = await assigmentService.GetFirstOrDefaultAsync(a => a.AssignmentId == assignmentId,
                                                                                  a => a.Creator,
                                                                                  a => a.Submissions.Select(s => s.Student),
                                                                                  a => a.Submissions.Select(s => s.SubmitFile));

            if (assignment == null)
            {
                return null;
            }

            var pageData = new AssignmentSubmissionsPageData
            {
                Assignment = assignment
            };


            var viewModel = viewFactory.CreateView<AssignmentSubmissionsPageData, AssignmentSubmissionsViewModel>(pageData);
            return viewModel;
        }

        public async Task<DetailsViewModel> GetDetailsViewModelAsync(int assignmentId, string studentId)
        {
            SubmissionDTO submission = await service.GetByCompositeKeysAsync(assignmentId, studentId );
            if (submission == null)
            {
                return null;
            }
            var pageData = new DetailsPageData
            {
                Submission = submission
            };

            var viewModel = viewFactory.CreateView<DetailsPageData, DetailsViewModel>(pageData);
            return viewModel;
        }

        public async Task<EditViewModel> GetEditViewModelAsync(int assignmentId, string studentId)
        {
            SubmissionDTO submission = await service.GetByCompositeKeysAsync(assignmentId, studentId );
            if (submission == null)
            {
                return null;
            }

            var pageData = new EditPageData
            {
                Submission = submission
            };
            var viewModel = viewFactory.CreateView<EditPageData, EditViewModel>(pageData);
            return viewModel;
        }

        public async Task UpdateAsync(EditViewModel viewModel)
        {
            SubmissionDTO editedSubmission = new SubmissionDTO
            {
                AssignmentId = viewModel.AssignmentId,
                StudentId = viewModel.StudentId,
                Grade = (byte?)viewModel.Grade,
                DueDate = viewModel.DueDate,
                Completed = viewModel.Completed
            };
            service.Update(editedSubmission,
                           s => s.DueDate,
                           s => s.Completed,
                           s => s.Grade);

            await service.SaveChangesAsync();
        }

        public async Task<DeleteViewModel> GetDeleteViewModelAsync(int assignmentId, string studentId)
        {
            SubmissionDTO submission = await service.GetByCompositeKeysAsync(assignmentId, studentId );
            if (submission == null)
            {
                return null;
            }
            var pageData = new DeletePageData
            {
                Submission = submission
            };

            DeleteViewModel viewModel = viewFactory.CreateView<DeletePageData, DeleteViewModel>(pageData);
            return viewModel;
        }

        public async Task DeleteAsync(int assignmentId, string studentId)
        {
            SubmissionDTO submission = await service.GetByCompositeKeysAsync(assignmentId, studentId );
            if (submission.SubmitFile != null)
            {
                DeleteFile(submission.SubmitFile);
            }
            service.Delete(submission);
            await service.SaveChangesAsync();
        }

        public async Task<IFileStreamWithInfo> GetSubmissionFileAsync(Controller controller, int assignmentId, string studentId)
        {
            SubmissionDTO submission = await service.GetByCompositeKeysAsync(assignmentId, studentId );
            if (submission == null || submission.SubmitFile == null)
            {
                return null;
            }
            string origFileName = submission.SubmitFile.FileName;
            string fileGuid = submission.SubmitFile.FileGuid;
            string mimeType = MimeMapping.GetMimeMapping(origFileName);
            IFileStreamWithInfo fileStream = null;
            try
            {
                string filePath = Path.Combine(controller.Server.MapPath("~/Files/Assignments"), fileGuid);
                fileStream = new FileStreamWithInfo
                {
                    FileStream = File.ReadAllBytes(filePath),
                    FileName = origFileName,
                    FileType = mimeType
                };
            }
            catch
            {
                return null;
            }
            return fileStream;
        }

        public async Task<bool> ToggleCompleteStatusAsync(int assignmentId, string studentId)
        {
            SubmissionDTO submission = await service.GetByCompositeKeysAsync(assignmentId, studentId );
            if (submission == null)
            {
                throw new Exception();
            }
            submission.Completed = submission.Completed == true ? false : true;
            await service.SaveChangesAsync();
            return submission.Completed;
        }

        public async Task<EvaluateViewModel> GetEvaluateViewModelAsync(int assignmentId, string studentId)
        {
            SubmissionDTO submission = await service.GetByCompositeKeysAsync(assignmentId, studentId );
            if (submission == null)
            {
                return null;
            }

            var pageData = new EvaluatePageData
            {
                AssignmentId = assignmentId,
                StudentId = studentId,
                Submission = submission
            };

            EvaluateViewModel viewModel = viewFactory.CreateView<EvaluatePageData, EvaluateViewModel>(pageData);
            return viewModel;
        }

        public async Task EvaluateAsync(EvaluateInputModel inputModel)
        {
            SubmissionDTO submission = await service.GetByCompositeKeysAsync(inputModel.assignmentId, inputModel.studentId );
            if (submission == null)
            {
                throw new Exception();
            }

            submission.Grade = (byte)inputModel.Grade;
            await service.SaveChangesAsync();
        }

        public async Task UploadFileAsync(Controller controller, HttpPostedFileBase file, int assignmentId, string studentId)
        {
            SubmissionDTO submission = await service.GetByCompositeKeysAsync(assignmentId, studentId );
            if (submission == null)
            {
                throw new Exception();
            }
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    if (submission.Completed)
                    {
                        controller.ViewBag.FileStatus = "This assignment is already complete!You cannot updload a file to this assignment.";
                        return;
                    }

                    SubmitFile newSubmitFile = new SubmitFile
                    {
                        FileName = file.FileName,
                        FileGuid = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName)
                    };

                    string path = Path.Combine(controller.Server.MapPath("~/Files/Assignments"), newSubmitFile.FileGuid);
                    file.SaveAs(path);

                    service.DeleteFileFromFSandDBIfExists(submission.SubmitFile);

                    submission.SubmitFile = newSubmitFile;
                    submission.Submitted = DateTime.Now;

                    await service.SaveChangesAsync();
                }
                catch (Exception )
                {
                    controller.ViewBag.FileStatus = "Error while file uploading.";
                }
            }
            else
            {
                controller.ModelState.AddModelError("", "Upload file is not selected!");
            }
            return;
        }

        private void DeleteFile(DataModel.Models.FileInfo file)
        {
            if (file == null) return;

            string fullPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Files/Assignments"), file.FileGuid);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
        public void Dispose()
        {
            IDisposable dispose = assigmentService as IDisposable;
            if (dispose != null)
            {
                dispose.Dispose();
            }
            dispose = service as IDisposable;
            if (dispose != null)
            {
                dispose.Dispose();
            }
        }

        public async Task<SubmissionViewModel> GetSubmissionAsync(int assignmentId, string studentId)
        {
            SubmissionDTO submission = await service.GetByCompositeKeysAsync(assignmentId, studentId );
            if(submission == null)
            {
                return null;
            }
            var pageData = new SubmissionPageData
            {
                Submission = submission
            };

            var viewModel = viewFactory.CreateView<SubmissionPageData, SubmissionViewModel>(pageData);
            return viewModel;
        }
    }

    public class FileStreamWithInfo : IFileStreamWithInfo
    {
        public string FileName { get; set; }
        public byte[] FileStream { get; set; }
        public string FileType { get; set; }
    }

}