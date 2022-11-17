namespace MyWebAPi
{
    public class SampleService : ISampleService
    {
        public IConfiguration Configuration { get; }
        public IStudentService StudentService { get; }
        public ILogger<SampleService> Logger { get; }

        public SampleService(IConfiguration configuration,
            IStudentService studentService,
            ILogger<SampleService> logger)
        {
            Configuration = configuration;
            StudentService = studentService;
            Logger = logger;
        }


        public async Task<Student> GetStudent(int id)
        {
            return await StudentService.GetStudent(id);
        }

        public async Task<IList<Student>> GetStudents()
        {
            return await StudentService.GetStudents();
        }

        public async Task<Student> CreateStudent(Student student)
        {
            return await StudentService.CreateStudent(student);
        }
    }
}
