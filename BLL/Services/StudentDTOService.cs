using Journal.AbstractBLL.AbstractServices;
using System;
using System.Threading.Tasks;
using Journal.DataModel.Models;
using Journal.AbstractDAL.AbstractRepositories;
using Journal.BLLtoUIData.DTOs;
using BLL.Services.Common;
using BLL.Services.Common.Abstract;

namespace Journal.BLL.Services.Concrete
{
    public class StudentDTOService : GenericDTOService<StudentDTO, Student, string>, IStudentDTOService
    {
        public StudentDTOService(IStudentRepository repository, IObjectToObjectMapper mapper) : base(repository,mapper)
        {
        }
              
        public async Task<StudentDTO> GetStudentByEmailAsync(string studentEmail)
        {
            ThrowIfNull(studentEmail);
            var student =  await currentEntityRepository.GetFirstOrDefaultAsync(s => s.Email == studentEmail);
            if(student == null)
            {
                return default(StudentDTO);
            }
            var result = mapper.Map<Student, StudentDTO>(student);
            return result;
        }

        private void ThrowIfNull(object arg)
        {
            if (arg == null) throw new ArgumentNullException();
        }

    }
}
