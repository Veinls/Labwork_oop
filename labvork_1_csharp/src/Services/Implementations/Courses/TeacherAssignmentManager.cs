using labvork_1_csharp.Models;
using labvork_1_csharp.Services.Interfaces;
using labvork_1_csharp.Data;

namespace labvork_1_csharp.Services.Implementations.Courses
{
    // Менеджер по назначению учителей
    public class TeacherAssignmentManager : IRelationshipManager<Course, Teacher>
    {
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<Teacher> _teacherRepository;

        public TeacherAssignmentManager(IRepository<Course> courseRepository, IRepository<Teacher> teacherRepository)
        {
            _courseRepository = courseRepository;
            _teacherRepository = teacherRepository;
        }

        public bool AddRelationship(int courseId, int teacherId)
        {
            var course = _courseRepository.GetByID(courseId);
            var teacher = _teacherRepository.GetByID(teacherId);

            if (course == null || teacher == null)
            {
                return false;
            }
            course.AssignTeacher(teacher);
            return true;
        }

        public bool RemoveRelationship(int courseId, int teacherId)
        {
            var course = _courseRepository.GetByID(courseId);
            
            if (course?.Teacher == null)
            {
                return false;
            }
            course.RemoveTeacher();
            return true;
        }

        public IEnumerable<Teacher> GetRelatedEntities(int courseId)
        {
            var course = _courseRepository.GetByID(courseId);
            return course?.Teacher != null ? new[] { course.Teacher } : Enumerable.Empty<Teacher>();
        }
    }
}