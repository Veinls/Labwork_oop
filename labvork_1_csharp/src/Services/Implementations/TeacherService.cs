using labvork_1_csharp.Models;
using labvork_1_csharp.Data;
using labvork_1_csharp.Services.Interfaces;

namespace labvork_1_csharp.Services.Implementations
{
    public class TeacherService : IRepository<Teacher>
    {
        private readonly InMemoryDataContext _context;

        public TeacherService(InMemoryDataContext context)
        {
            _context = context;
        }
        
        public Teacher? GetByID(int ID)
        {
            return _context.Teachers.FirstOrDefault(teacher => teacher.PersonID == ID);
        }

        public IEnumerable<Teacher> GetAll()
        {
            return _context.Teachers.ToList();
        }

        public void Add(Teacher teacher)
        {
            teacher.PersonID = _context.Teachers.Any() ? _context.Teachers.Max(t => t.PersonID) + 1 : 1;
            _context.Teachers.Add(teacher);
        }

        public bool Remove(int ID)
        {
            var teacher = GetByID(ID);
            if (teacher == null) return false;
            
            foreach (var course in _context.Courses.Where(c => c.TeacherID == teacher.PersonID))
            {
                course.RemoveTeacher();
            }
            _context.Teachers.Remove(teacher);
            return true;
        }

        public IEnumerable<Teacher> GetByDepartment(string department)
        {
            return _context.Teachers.Where(t => t.Department == department).ToList();
        }
    }
}