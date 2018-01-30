using Journal.AbstractBLL.AbstractServices;
using System;
using System.Threading.Tasks;
using Journal.DataModel.Models;
using Journal.AbstractDAL.AbstractRepositories;
using Journal.BLL.Services.Concrete.Common;

namespace Journal.BLL.Services.Concrete
{
    public class StudentDTOService : GenericService<Student, string>, IStudentDTOService
    {
        public StudentDTOService(IStudentRepository repository) : base(repository)
        {
        }
              
        public async Task<Student> GetStudentByEmailAsync(string studentEmail)
        {
            ThrowIfNull(studentEmail);
            return await repository.GetFirstOrDefaultAsync(s => s.Email == studentEmail);
        }

        private void ThrowIfNull(object arg)
        {
            if (arg == null) throw new ArgumentNullException();
        }

    }
}
