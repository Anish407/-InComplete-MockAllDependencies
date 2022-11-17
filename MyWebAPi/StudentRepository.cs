namespace MyWebAPi
{
    public class StudentRepository : IStudentRepository
    {
        public StudentRepository()
        {

        }


        private IList<Student> students = new List<Student>
        {
            new Student()
            {
                Id=1,
                Name="Anish"
            },
            new Student()
            {
                Id=2,
                Name="Jiya"
            },new Student()
            {
                Id=3,
                Name="Jeeba"
            },
        };


        public Task<Student> GetStudent(int id)
        {
            return Task.FromResult(students.FirstOrDefault(i => i.Id == id));
        }

        public Task<IList<Student>> GetStudents()
        {
            return Task.FromResult(students);
        }

        public Task<Student> CreateStudent(Student student)
        {
            students.Add(student);
            return Task.FromResult(student);
        }

    }
}
