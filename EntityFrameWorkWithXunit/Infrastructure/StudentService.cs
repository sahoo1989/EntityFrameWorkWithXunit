using EntityFrameWorkWithXunit.Application;
using EntityFrameWorkWithXunit.Domain;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWorkWithXunit.Infrastructure
{
    public class StudentService : IStudentService
    {
        private readonly StudentDbContext _studentDbContext;
        public StudentService(StudentDbContext studentDbContext)
        {
            _studentDbContext = studentDbContext;
        }
        public async Task<Student> GetStudentByIdAsync(int id)
        {
            var result = await _studentDbContext.students.Where(x => x.StudentId == id).FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteStudentAsync(int id)
        {
            if (await _studentDbContext.students.FindAsync(id) == null)
            {
                return false;
            }
            else
            {
                var result = await _studentDbContext.students.Where(x => x.StudentId == id).FirstAsync();
                _studentDbContext.students.Remove(result);
                await _studentDbContext.SaveChangesAsync();
                return true;
            }
        }

       

        public async Task<List<Student>> GetStudentsAsync()
        {
            var result = await _studentDbContext.students.ToListAsync();
            return result;
        }

        public async Task SaveStudentAsync(Student student)
        {
            _studentDbContext.students.Add(student);
            await _studentDbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateStudentAsync(Student student)
        {
            var result = await _studentDbContext.students.FindAsync(student.StudentId);
            if (result == null)
            {
                return false;
            }
            else
            {
                result.DateOfBirth = student.DateOfBirth;
                result.Name = student.Name;
                result.Mobile = student.Mobile;
                _studentDbContext.Update<Student>(result);
                await _studentDbContext.SaveChangesAsync();
                return true;
            }
        }
    }
}
