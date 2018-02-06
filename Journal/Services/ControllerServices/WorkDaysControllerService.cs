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
using Journal.DTOBuilderDataFactory.BuilderInputData;
using Journal.DTOFactory.Abstractions;

namespace Journal.Services.ControllerServices
{
    public class WorkDaysControllerService : IWorkDaysControllerService
    {
        protected readonly IWorkDayDTOService workDayService;
        protected readonly IStudentDTOService studentService;
        protected readonly IAttendanceDTOService attendanceService;
        protected readonly IObjectToObjectMapper mapper;
        protected readonly IViewFactory viewModelFactory;
        protected readonly IDTOFactory dtoFactory;

        public WorkDaysControllerService(IWorkDayDTOService workDayService,
                                         IStudentDTOService studentService,
                                         IAttendanceDTOService attendanceService,
                                         IObjectToObjectMapper mapper,
                                         IViewFactory viewFactory,
                                         IDTOFactory dtoFactory)
        {
            this.attendanceService = attendanceService;
            this.studentService = studentService;
            this.workDayService = workDayService;
            this.mapper = mapper;
            this.viewModelFactory = viewFactory;
            this.dtoFactory = dtoFactory;
        }

        public async Task<IndexViewModel> GetWorkDaysIndexViewModel()
        {
            IEnumerable<WorkDayDTO> workDayDTOs = await workDayService.GetAllAsync();
            IEnumerable<WorkDayViewModel> workDayViewModels = viewModelFactory.CreateView<IEnumerable<WorkDayDTO>, IEnumerable<WorkDayViewModel>>(workDayDTOs);

            var viewModelData = new IndexViewData(workDayDTOs);
            IndexViewModel viewModel = viewModelFactory.CreateView<IndexViewData, IndexViewModel>(viewModelData);
            
            return viewModel;
        }

        public async Task<DetailsViewModel> GetWorkDayDetailsViewModelAsync(int workDayId)
        {
            WorkDayDTO workDayDTO = await workDayService.GetWorkDayWithAttendeesByIdAsync(workDayId);

            if (workDayDTO == null)
            {
                return null;
            }

            var viewModelData = new DetailsViewData(workDayDTO);

            DetailsViewModel viewModel = viewModelFactory.CreateView<DetailsViewData, DetailsViewModel>(viewModelData);          
            return viewModel;
        }

        public CreateViewModel GetCreateWorkDayViewModel(int journalId)
        {
            CreateViewModel viewModel = viewModelFactory.CreateView<CreateViewModel>();
            return viewModel;
        }

        public async Task<int> CreateWorkDayAsync(CreateViewModel inputModel)
        {
            var builderData = new WorkDayDTOBuilderData(inputModel);
            WorkDayDTO newWorkDay = dtoFactory.CreateDTO<WorkDayDTOBuilderData, WorkDayDTO>(builderData);

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
            EditViewData viewModelData = new EditViewData(workDayDTO);
        
            EditViewModel viewModel = viewModelFactory.CreateView<EditViewData, EditViewModel>(viewModelData);                      
            return viewModel;
        }

        public async Task WorkDayUpdateAsync(EditViewModel inputModel)
        {
            WorkDayDTOBuilderData builderData = new WorkDayDTOBuilderData(inputModel);
            WorkDayDTO updatedWorkDay = dtoFactory.CreateDTO<WorkDayDTOBuilderData, WorkDayDTO>(builderData);
            workDayService.UpdateDay(updatedWorkDay);
            await workDayService.SaveChangesAsync();
        }

        public async Task<DeleteViewModel> GetWorkDayDeleteViewModelAsync(int id)
        {
            WorkDayDTO workDayDTO = await workDayService.GetByIdAsync(id);
            if (workDayDTO == null)
            {
                return null;
            }
            var viewModelData = new DeleteViewData(workDayDTO);
           
            var viewModel = viewModelFactory.CreateView<DeleteViewData, DeleteViewModel>(viewModelData);

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
            IEnumerable<StudentDTO> mentorsAllStudents = await studentService.GetByMentorId(mentorId);

            IEnumerable<AttendanceDTO> attendances = await attendanceService.GetByWorkDayId(workDayId);

            List<StudentDTO> presentStudents = new List<StudentDTO>();
            foreach (var attendance in attendances)
            {
                presentStudents.Add(attendance.Student);
            }

            IEnumerable<StudentDTO> notPresentStudents = mentorsAllStudents.Except(presentStudents);
            var viewModelData = new AddAttendeesViewData(notPresentStudents);
            var viewModel = viewModelFactory.CreateView<AddAttendeesViewData, AddAttendeesViewModel>(viewModelData);
            return viewModel;
        }

        public async Task AddWorkDayAttendeesAsync(int workDayId, List<string> attendeeIds)
        {
            if (attendeeIds != null)
            {
                WorkDayDTO workDay = await workDayService.GetByIdAsync(workDayId);
                            
                IEnumerable<StudentDTO> students = await studentService.GetStudentsByIds(attendeeIds);

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
              
                IEnumerable<AttendanceDTO> attendances = await attendanceService.GetAttendeesByIds(attendaceIds);

                foreach (AttendanceDTO attendee in attendances)
                {
                    attendee.Left = DateTime.Now;
                }

                await workDayService.SaveChangesAsync();
            }
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