using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MyWebAPi
{
    public interface IStudentService
    {
        Task<Student> CreateStudent(Student student);
        Task<Student> GetStudent(int id);
        Task<IList<Student>> GetStudents();
    }

    public class StudentService: IStudentService
    {
        public IConfiguration Configuration { get; }
        public IStudentRepository StudentRepository { get; }
        public ILogger<SampleService> Logger { get; }

        public StudentService(IConfiguration configuration,
           IStudentRepository studentRepository,
           ILogger<SampleService> logger)
        {
            Configuration = configuration;
            StudentRepository = studentRepository;
            Logger = logger;
        }


        public async Task<Student> GetStudent(int id)
        {
            return await StudentRepository.GetStudent(id);
        }

        public async Task<IList<Student>> GetStudents()
        {
            return await StudentRepository.GetStudents();
        }

        public async Task<Student> CreateStudent(Student student)
        {
            return await StudentRepository.CreateStudent(student);
        }
    }
}
