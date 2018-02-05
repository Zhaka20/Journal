﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Journal.ViewModels.Controller.Assignments;
using Microsoft.AspNet.Identity;
using Journal.Services.Abstractions;

namespace Journal.Controllers
{
    public class AssignmentsController : Controller
    {
        private IAssignmentsControllerService _service;

        public AssignmentsController(IAssignmentsControllerService service)
        {
            this._service = service;
        }

        // GET: Assignments
        public async Task<ActionResult> Index()
        {
            IndexViewModel viewModel = await _service.GetIndexViewModelAsync();
            return View(viewModel);
        }

        // GET: Assignments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailsViewModel viewModel = await _service.GetDetailsViewModelAsync((int)id);
            return View(viewModel);
        }

        public async Task<ActionResult> Mentor(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MentorViewModel viewModel = await _service.GetMentorAssignmentsViewModelAsync(id);
            return View(viewModel);
        }

        // GET: Assignments/Create
        [Authorize(Roles = "Mentor")]
        public ActionResult Create()
        {
            CreateViewModel viewModel = _service.GetCreateViewModel();
            return View(viewModel);
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Mentor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateViewModel inputModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    try
                    {
                        string mentorId = User.Identity.GetUserId();
                        int newAssignmentId = await _service.CreateAsync(this, mentorId, inputModel, file);
                        ViewBag.FileStatus = "File uploaded successfully.";
                        return RedirectToAction("Details", "Assignments", new { id = newAssignmentId });
                    }
                    catch (Exception )
                    {
                        ViewBag.FileStatus = "Error while file uploading.";
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Upload file is not selected!");
                }
            }
            return View(inputModel);
        }


        // GET: Assignments/Create
        [Authorize(Roles = "Mentor")]
        public async Task<ActionResult> CreateAndAssignToSingleUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreateAndAssignToSingleUserViewModel viewModel = await _service.GetCreateAndAssignToSingleUserViewModelAsync(id);
            return View(viewModel);
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Mentor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAndAssignToSingleUser(CreateViewModel inputModel, HttpPostedFileBase file, string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    try
                    {
                        int newAssignmentId = await _service.CreateAndAssignToSingleUserAsync(this, id, inputModel, file);
                        ViewBag.FileStatus = "File uploaded successfully.";                     
                        return RedirectToAction("Details", "Assignments", new { id = newAssignmentId });                    
                    }
                    catch (Exception )
                    {
                        ViewBag.FileStatus = "Error while file uploading.";
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Upload file is not selected!");
                }
            }
            CreateAndAssignToSingleUserViewModel viewModel = await _service.GetCreateAndAssignToSingleUserViewModelAsync(id);
            return View(viewModel);
        }
        // GET: Assignments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EdtiViewModel viewModel = await _service.GetEdtiViewModelAsync((int)id);
            if(viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // POST: Assignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EdtiViewModel inputModel)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(inputModel);
                return RedirectToAction("Details", new { id = inputModel.AssignmentId });
            }
          
            return View(inputModel);
        }

        // GET: Assignments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeleteViewModel viewModel = await _service.GetDeleteViewModelAsync((int)id);
            if(viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            string mentorId = User.Identity.GetUserId();
            await _service.DeleteAsync(id);
            return RedirectToAction("Mentor", "Assignments", new { id = mentorId });
        }


        public async Task<FileResult> DownloadAssignmentFile(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            IFileStreamWithInfo fileStream = await _service.GetAssignmentFileAsync((int)id);
            return File(fileStream.FileStream, fileStream.FileType, fileStream.FileName);
        }
        public async Task<ActionResult> Submissions(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentsAndSubmissionsListViewModel viewModel = await _service.GetStudentsAndSubmissionsListViewModelAsync((int)id);
            if(viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        [Authorize(Roles = "Mentor")]
        [Route("assignments/removestudent/{id:int}/{studentId}")]
        public async Task<ActionResult> RemoveStudent(int id, string studentId)
        {
            if (studentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RemoveStudentViewModel viewModel = await _service.GetRemoveStudentViewModelAsync(id,studentId);
            return View(viewModel);
        }

        [ActionName("RemoveStudent")]
        [Authorize(Roles = "Mentor")]
        [Route("assignments/removestudent/{id:int}/{studentId}")]
        [HttpPost]
        public async Task<ActionResult> RemoveStudentPost(int id, string studentId)
        {
            if (studentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await _service.RemoveStudentFromAssignmentAsync(id, studentId);
            return RedirectToAction("StudentsAndSubmissionsList", new { id = id });
        }

        public async Task<ActionResult> AssignToStudent(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AssignToStudentViewModel viewModel = await _service.GetAssignToStudentViewModelAsync(id);
            if(viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignToStudent(string id, List<int> assignmentIds)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            await _service.AssignToStudentAsync(id, assignmentIds);
            return RedirectToAction("Student", "Mentors", new { id = id });
        }

        public async Task<ActionResult> AssignToStudents(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AssignToStudentsViewModel viewModel = await _service.GetAssignToStudentsViewModelAsync((int)id);
            if(viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        [Authorize(Roles = "Mentor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignToStudents(int? id, List<string> studentId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            await _service.AssignToStudentsAsync((int)id, studentId);
            return RedirectToAction("Assignment", "Submissions", new { id = id });
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
