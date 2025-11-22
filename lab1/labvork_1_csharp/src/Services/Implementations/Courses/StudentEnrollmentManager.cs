using labvork_1_csharp.Models;
using labvork_1_csharp.Services.Interfaces;
using labvork_1_csharp.Data;

namespace labvork_1_csharp.Services.Implementations.Courses
{
    // Менеджер по набору студентов
    public class StudentEnrollmentManager : IRelationshipManager<Course, Student>
    {
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<Student> _studentRepository;

        public StudentEnrollmentManager(IRepository<Course> courseRepository, IRepository<Student> studentRepository)
        {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
        }

        public bool AddRelationship(int courseID, int studentID)
        {
            var course = _courseRepository.GetByID(courseID);
            var student = _studentRepository.GetByID(studentID);

            if (course == null || student == null)
            {
                return false;
            }
            course.AddStudent(student);
            return true;
        }

        public bool RemoveRelationship(int courseID, int studentID)
        {
            var course = _courseRepository.GetByID(courseID);
            var student = _studentRepository.GetByID(studentID);

            if (course == null || student == null)
            {
                return false;
            }
            course.RemoveStudent(student);
            return true;
        }

        public IEnumerable<Student> GetRelatedEntities(int courseID)
        {
            var course = _courseRepository.GetByID(courseID);
            return course?.Students ?? Enumerable.Empty<Student>();
        }
    }
}