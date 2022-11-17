namespace MyWebAPi
{
    public interface IStudentRepository
    {
        Task<Student> CreateStudent(Student student);
        Task<Student> GetStudent(int id);
        Task<IList<Student>> GetStudents();
    }
}