using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Journal.ViewModels.Controller.Students;
using Journal.App_Start;
using Journal.Services.Abstractions;

namespace Journal.Controllersw
{
    [Authorize]
    public class StudentsController : Controller
    {
        private ApplicationUserManager userManager;
        private IStudentsControllerService _service;

        public StudentsController(IStudentsControllerService service, ApplicationUserManager userManager)
        {
            this._service = service;
            this.userManager = userManager;
        }
        // GET: Students
        public async Task<ActionResult> Index()
        {
            IndexViewModel viewModel = await _service.GetIndexViewModelAsync();
            return View(viewModel);
        }

        public async Task<ActionResult> Home()
        {
            string studentId = User.Identity.GetUserId();
            HomeViewModel viewModel = await _service.GetHomeViewModelAsync(studentId);
            
            return View(viewModel);
        }

        // GET: Students/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DetailsViewModel viewModel = await _service.GetDetailsViewModelAsync(id);
            if(viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // GET: Students/Create
        [Authorize(Roles = "Admin, Mentor")]
        public ActionResult Create()
        {
            CreateViewModel viewModel = _service.GetCreateViewModel();
            return View(viewModel);
        }

        // POST: Students/Create
        [Authorize(Roles = "Admin, Mentor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateViewModel viewModel)
        {
            userManager = StaticConfig.ConfigureApplicationUserManager(userManager);

            if (ModelState.IsValid)
            {
                IdentityResult result = await _service.CreateAsync(viewModel);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                AddErrors(result);         
            }
            return View(viewModel);
        }

        // GET: Students/Edit/5
        [Authorize(Roles = "Admin, Mentor")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditViewModel viewModel = await _service.GetEditViewModelAsync(id);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Mentor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditViewModel viewModel)
        {    
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(viewModel);
                    return RedirectToAction("Student", "Mentors", new { id = viewModel.Id });
                }
                catch
                {
                    ViewBag.ErrorMassage = "Could not update the Student. Please try again!";
                }
             
            }
            return View(viewModel);
        }

        // GET: Students/Delete/5
        [Authorize(Roles = "Admin, Mentor")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeleteViewModel viewModel = await _service.GetDeleteViewModelAsync(id);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Mentor")]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.ErrorMessage = "Could not delete the Student. Please try again!";
                return RedirectToAction("Delete", new { id = id });
            }          
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _service.Dispose();
            }
            base.Dispose(disposing);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}
