using Journal.AbstractBLL.AbstractServices;
using System;
using System.Threading.Tasks;
using Journal.DataModel.Models;
using Journal.AbstractDAL.AbstractRepositories;
using BLL.Services.Common;
using Journal.BLLtoUIData.DTOs;
using BLL.Services.Common.Abstract;

namespace Journal.BLL.Services.Concrete
{
    public class MentorDTOService : GenericDTOService<MentorDTO, Mentor, string>, IMentorDTOService
    {
        protected readonly IStudentRepository studentsRepository;

        public MentorDTOService(IStudentRepository studentsRepository,
                                IMentorRepository mentorRepository, 
                                IObjectToObjectMapper mapper)
                              : base(mentorRepository, mapper)
        {
            this.studentsRepository = studentsRepository;
        }


        public async Task<MentorDTO> GetMentorByEmailAsync(string mentorEmail)
        {
            ThrowIfNull(mentorEmail);
            var mentor = await currentEntityRepository.GetFirstOrDefaultAsync(m => m.Email == mentorEmail);
            if(mentor == null)
            {
                return default(MentorDTO);
            }
            var result = mapper.Map<Mentor, MentorDTO>(mentor);
            return result;
        }

        public async Task AcceptStudentAsync(string studentId, string mentorId)
        {
            ThrowIfNull(studentId, mentorId);

            var student = await studentsRepository.GetSingleByIdAsync(studentId);
            if (student == null)
            {
                throw new ArgumentException("Student with given id doesn't exit");
            }

            var mentor = await currentEntityRepository.GetSingleByIdAsync(mentorId);
            if (mentor == null)
            {
                throw new ArgumentException("Mentor with given id doesn't exit");
            }

            mentor.Students.Add(student);
        }

        public async Task RemoveStudentAsync(string studentId, string mentorId)
        {
            ThrowIfNull(studentId, mentorId);
            var student = await studentsRepository.GetSingleByIdAsync(studentId);
            if (student == null)
            {
                throw new ArgumentException("Student with given id doesn't exit");
            }

            var mentor = await currentEntityRepository.GetSingleByIdAsync(mentorId);
            if (mentor == null)
            {
                throw new ArgumentException("Mentor with given id doesn't exit");
            }

            mentor.Students.Remove(student);
        }

        private void ThrowIfNull(params object[] args)
        {
            foreach (var arg in args)
            {
                if (arg == null) throw new ArgumentNullException();
            }
        }

        public void Dispose()
        {
            IDisposable dispose = studentsRepository as IDisposable;
            if (dispose != null)
            {
                dispose.Dispose();
            }
            dispose = currentEntityRepository as IDisposable;
            if(dispose != null)
            {
                dispose.Dispose();
            }
        }
    }
}
