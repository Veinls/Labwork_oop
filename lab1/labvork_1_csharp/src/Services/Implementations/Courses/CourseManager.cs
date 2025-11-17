using labvork_1_csharp.Models;
using labvork_1_csharp.Services.Interfaces;
using labvork_1_csharp.Data;

namespace labvork_1_csharp.Services.Implementations.Courses
{
    // Курс-менеджер
    public class CourseManager : IRepository<Course>
    {
        private readonly InMemoryDataContext _context;

        public CourseManager(InMemoryDataContext context)
        {
            _context = context;
        }

        public Course? GetByID(int ID)
        {
            return _context.Courses.FirstOrDefault(course => course.CourseID == ID);
        }

        public IEnumerable<Course> GetAll()
        {
            return _context.Courses.ToList();
        }

        public void Add(Course course)
        {
            course.CourseID = _context.Courses.Any() ? _context.Courses.Max(c => c.CourseID) + 1 : 1;
            _context.Courses.Add(course);
        }

        public bool Remove(int ID)
        {
            var course = GetByID(ID);
            if (course == null)
            {
                return false;
            }
            _context.Courses.Remove(course);
            return true;
        }
    }
}