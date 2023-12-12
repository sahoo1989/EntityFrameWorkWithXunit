using EntityFrameWorkWithXunit.Domain;

namespace EntityFrameWorkWithXunit.Infrastructure
{
    public interface IStudentService
    {
        public Task<List<Student>> GetStudentsAsync();

        public Task<Student> GetStudentByIdAsync(int id);
        public Task SaveStudentAsync(Student student);
        public Task<bool> DeleteStudentAsync(int id);
        public Task<bool> UpdateStudentAsync(Student student);
    }
}
