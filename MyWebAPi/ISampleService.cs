namespace MyWebAPi
{
    public interface ISampleService
    {
        Task<Student> CreateStudent(Student student);
        Task<Student> GetStudent(int id);
        Task<IList<Student>> GetStudents();
    }
}