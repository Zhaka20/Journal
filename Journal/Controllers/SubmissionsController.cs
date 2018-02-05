﻿using System;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Journal.ViewModels.Controller.Submissions;
using Microsoft.AspNet.Identity;
using Journal.Services.Abstractions;
using Journal.ViewModels.Shared.EntityViewModels;

namespace Journal.Controllers
{
    public class SubmissionsController : Controller
    {
        private ISubmissionsControllerService _service;

        public SubmissionsController(ISubmissionsControllerService service)
        {
            this._service = service;
        }
        // GET: Submissions
        public async Task<ActionResult> Index()
        {
            IndexViewModel viewModel = await _service.GetIndexViewModelAsync();
            return View(viewModel);
        }


        public async Task<ActionResult> Assignment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AssignmentViewModel viewModel = await _service.GetAssignmentSubmissionsViewModelAsync((int)id);
            if(viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // GET: Submissions/Details/5
        [Route("submissions/details/{assignmentId:int}/{studentId}")]
        public async Task<ActionResult> Details(int assignmentId, string studentId)
        {
            if (studentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailsViewModel viewModel = await _service.GetDetailsViewModelAsync(assignmentId, studentId);
            if(viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // GET: Submissions/Edit/5
        [HttpGet]
        [Route("submissions/edit/{assignmentId:int}/{studentId}")]
        public async Task<ActionResult> Edit(int assignmentId, string studentId)
        {
            if (studentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EditViewModel viewModel = await _service.GetEditViewModelAsync(assignmentId,studentId);
            if(viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // POST: Submissions/Edit/5
        [Route("submissions/edit/{assignmentId:int}/{studentId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            await _service.UpdateAsync(inputModel);
            return RedirectToAction("Details", "Submissions", new { assignmentId = inputModel.AssignmentId, studentId = inputModel.StudentId });
        }

        // GET: Submissions/Delete/5
        public async Task<ActionResult> Delete(int assignmentId, string studentId)
        {
            if (studentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DeleteViewModel viewModel = await _service.GetDeleteViewModelAsync(assignmentId,studentId);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // POST: Submissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int assignmentId, string studentId)
        {
            await _service.DeleteAsync(assignmentId,studentId);
            return RedirectToAction("Index");
        }
        [Route("assignments/DownloadSubmissionFile/{assignmentId:int}/{studentId}")]
        public async Task<FileResult> DownloadSubmissionFile(int assignmentId, string studentId)
        {
            if (studentId == null)
            {
                throw new ArgumentNullException();
            }

            IFileStreamWithInfo fileStream = await _service.GetSubmissionFileAsync(this, assignmentId,studentId);
                     
            return File(fileStream.FileStream,fileStream.FileType, fileStream.FileName);
        }

        [Authorize(Roles = "Mentor")]
        [HttpPost]
        [Route("Submissions/toggleStatus/{assignmentId:int}/{studentId}")]
        public async Task<ActionResult> ToggleStatus(int assignmentId, string studentId)
        {
            if(studentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await _service.ToggleCompleteStatusAsync(assignmentId,studentId);
            return RedirectToAction("Student", "Mentors", new { id = studentId });
        }


        [Authorize(Roles = "Mentor")]
        [Route("Submissions/evaluate/{assignmentId:int}/{studentId}")]
        public async Task<ActionResult> Evaluate(int assignmentId, string studentId)
        {
            EvaluateViewModel viewModel = await _service.GetEvaluateViewModelAsync(assignmentId,studentId);
            if(viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        [Authorize(Roles = "Mentor")]
        [HttpPost]
        public async Task<ActionResult> Evaluate(EvaluateInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                await _service.EvaluateAsync(inputModel);
                return RedirectToAction("Student", "Mentors", new { id = inputModel.studentId });
            }

            EvaluateViewModel viewModel = new EvaluateViewModel
            {
                Submission = await _service.GetSubmissionAsync(inputModel.assignmentId,inputModel.studentId),
                Grade = inputModel.Grade
            };
            return View(viewModel);
        }
   
        [Authorize(Roles = "Student")]
        public ActionResult UploadFile(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult> UploadFile(HttpPostedFileBase file, int id)
        {
            string studentId = User.Identity.GetUserId();
            await _service.UploadFileAsync(this, file, id, studentId);
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _service.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
