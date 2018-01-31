using Journal.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Journal.ViewModels.Controller.WorkDays;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Journal.ViewModels.Shared.EntityViewModels;
using Journal.BLLtoUIData.DTOs;
using Journal.AbstractBLL.AbstractServices;
using Journal.WEB.Services.Common;
using Journal.ViewFactory.Abstractions;
using Journal.WEB.ViewFactory.BuilderInputData.Controllers.WorkDays;

namespace Journal.Services.ControllerServices
{
    public class WorkDaysControllerService : IWorkDaysControllerService
    {
        protected readonly IWorkDayDTOService workDayService;
        protected readonly IStudentDTOService studentService;
        protected readonly IAttendanceDTOService attendanceService;
        protected readonly IObjectToObjectMapper mapper;
        protected readonly IViewFactory viewModelFactory;

        public WorkDaysControllerService(IWorkDayDTOService workDayService,
                                         IStudentDTOService studentService,
                                         IAttendanceDTOService attendanceService,
                                         IObjectToObjectMapper mapper,
                                         IViewFactory viewFactory)
        {
            this.attendanceService = attendanceService;
            this.studentService = studentService;
            this.workDayService = workDayService;
            this.mapper = mapper;
            this.viewModelFactory = viewFactory;
        }

        public async Task<IndexViewModel> GetWorkDaysIndexViewModel()
        {
            IEnumerable<WorkDayDTO> workDayDTOs = await workDayService.GetAllAsync();
            IEnumerable<WorkDayViewModel> workDayViewModels = viewModelFactory.CreateView<IEnumerable<WorkDayDTO>, IEnumerable<WorkDayViewModel>>(workDayDTOs);

            var viewModelBuilderData = new IndexViewModelBuilderData
            {
                WorkDays = workDayDTOs
            };
            IndexViewModel viewModel = viewModelFactory.CreateView<IndexViewModelBuilderData, IndexViewModel>(viewModelBuilderData);
            
            return viewModel;
        }

        public async Task<DetailsViewModel> GetWorkDayDetailsViewModelAsync(int workDayId)
        {
            WorkDayDTO workDayDTO = await workDayService.GetFirstOrDefaultAsync(w => w.Id == workDayId, w => w.Attendances);

            if (workDayDTO == null)
            {
                return null;
            }

            var viewModelbuilderData = new DetailsViewModelBuilderData
            {
                WorkDay = workDayDTO
            };

            DetailsViewModel viewModel = viewModelFactory.CreateView<DetailsViewModelBuilderData, DetailsViewModel>(viewModelbuilderData);          
            return viewModel;
        }

        public async Task<int> CreateWorkDayAsync(CreateViewModel inputModel)
        {
            WorkDayDTO newWorkDay = new WorkDayDTO
            {
                JournalId = inputModel.JournalId,
                Day = inputModel.Day
            };

            workDayService.Create(newWorkDay);
            await workDayService.SaveChangesAsync();
            return newWorkDay.Id;
        }

        public async Task<EditViewModel> GetWorkDayEditViewModelAsync(int workDayId)
        {
            WorkDayDTO workDayDTO = await workDayService.GetByIdAsync(workDayId);
            if (workDayDTO == null)
            {
                return null;
            }
            var viewModelbuilderData = new CreateViewModelBuilderData
            {
                WorkDay = workDayDTO
            };
            EditViewModel viewModel = viewModelFactory.CreateView<CreateViewModelBuilderData, EditViewModel>(viewModelbuilderData);                      
            return viewModel;
        }

        public async Task WorkDayUpdateAsync(EditViewModel inputModel)
        {
            //WorkDayDTO updatedWorkDay = new WorkDayDTO
            //{
            //    Id = inputModel.WorkDayToEdit.Id,
            //    Day = inputModel.WorkDayToEdit.Day
            //};
            WorkDayDTO updatedWorkDay = mapper.Map<WorkDayViewModel, WorkDayDTO>(inputModel.WorkDayToEdit);
            workDayService.Update();
            await workDayService.SaveChangesAsync();
        }

        public async Task<DeleteViewModel> GetWorkDayDeleteViewModelAsync(int id)
        {
            WorkDayDTO workDayDTO = await workDayService.GetByIdAsync(id);
            if (workDayDTO == null)
            {
                return null;
            }
            var viewModelBuilderData = new DeleteViewModelBuilderData
            {
                WorkDay = workDayDTO
            };
            var viewModel = viewModelFactory.CreateView<DeleteViewModelBuilderData, DeleteViewModel>(viewModelBuilderData);

            return viewModel;
        }

        public async Task WorkDayDeleteAsync(int workDayId)
        {
            WorkDayDTO workDay = await workDayService.GetByIdAsync(workDayId);
            workDayService.Delete(workDay);
            await workDayService.SaveChangesAsync();
        }

        public async Task<AddAttendeesViewModel> GetAddAttendeesViewModelAsync(int workDayId)
        {
            string mentorId = HttpContext.Current.User.Identity.GetUserId();
            //IQueryable<Student> mentorsAllStudents = db.Students.Where(s => s.MentorId == mentorId);

            IEnumerable<StudentDTO> mentorsAllStudents = await studentService.GetAllAsync(s => s.MentorId == mentorId);

            //IQueryable<Student> presentStudents = from attendance in db.Attendances
            //                      where attendance.WorkDayId == workDayId
            //                      select attendance.Student;

            IEnumerable<AttendanceDTO> attendances = await attendanceService.GetAllAsync(a => a.WorkDayId == workDayId);

            List<StudentDTO> presentStudents = new List<StudentDTO>();
            foreach (var attendance in attendances)
            {
                presentStudents.Add(attendance.Student);
            }

            //List<Student> notPresentStudents = await mentorsAllStudents.Except(presentStudents).ToListAsync();

            IEnumerable<StudentDTO> notPresentStudents = mentorsAllStudents.Except(presentStudents);
            var viewModelBuilderData = new AddAttendeesViewModelBuilderData
            {
                NotPresentStudents = notPresentStudents
            };
            var viewModel = viewModelFactory.CreateView<AddAttendeesViewModelBuilderData, AddAttendeesViewModel>(viewModelBuilderData);
            return viewModel;
        }

        public async Task AddWorkDayAttendeesAsync(int workDayId, List<string> attendeeIds)
        {
            if (attendeeIds != null)
            {
                WorkDayDTO workDay = await workDayService.GetByIdAsync(workDayId);

                //IQueryable<Student> query = from student in db.Students
                //            where attendeeIds.Contains(student.Id)
                //            select student;

                IEnumerable<StudentDTO> students = await studentService.GetAllAsync(s => attendeeIds.Contains(s.Id));
                //List<Student> listOfStudents = await query.ToListAsync();


                //WORKDAYSERVICE.ADDATTENDEESIMPLEMENT NEEDED
                foreach (StudentDTO student in students)
                {
                    workDay.Attendances.Add(new AttendanceDTO { Student = student, Come = DateTime.Now });
                }
                await workDayService.SaveChangesAsync();
            }

        }

        public async Task CheckAsLeftAsync(int workDayId, List<int> attendaceIds)
        {
            if (attendaceIds != null)
            {
                WorkDayDTO workDay = await workDayService.GetByIdAsync(workDayId);
                //WorkDay workDay = await db.WorkDays.FindAsync(workDayId);

                //IQueryable<Attendance> query = from attenance in db.Attendances
                //            where attendaceIds.Contains(attenance.Id)
                //            select attenance;
                //List<Attendance> listOfAttendees = await query.ToListAsync();
                IEnumerable<AttendanceDTO> attendances = await attendanceService.GetAllAsync(a => attendaceIds.Contains(a.Id));

                //foreach (Attendance attendee in listOfAttendees)
                //{
                //    attendee.Left = DateTime.Now;
                //}

                foreach (AttendanceDTO attendee in attendances)
                {
                    attendee.Left = DateTime.Now;
                }

                await workDayService.SaveChangesAsync();
                //await db.SaveChangesAsync();
            }
        }

        public CreateViewModel GetCreateWorkDayViewModel(int journalId)
        {
            CreateViewModel viewModel = viewModelFactory.CreateView<CreateViewModel>();
            return viewModel;
        }

        public void Dispose()
        {
            IDisposable disposable = attendanceService as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
            disposable = studentService as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
            disposable = workDayService as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

    }
}