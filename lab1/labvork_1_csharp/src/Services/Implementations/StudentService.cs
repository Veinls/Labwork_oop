using labvork_1_csharp.Models;
using labvork_1_csharp.Data;
using labvork_1_csharp.Services.Interfaces;

namespace labvork_1_csharp.Services.Implementations
{
    public class StudentService : IRepository<Student>
    {
        private readonly InMemoryDataContext _context;

        public StudentService(InMemoryDataContext context)
        {
            _context = context;
        }

        public Student? GetByID(int ID)
        {
            return _context.Students.FirstOrDefault(student => student.PersonID == ID);
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        public void Add(Student student)
        {
            student.PersonID = _context.Students.Any() ? _context.Students.Max(s => s.PersonID) + 1 : 1;
            _context.Students.Add(student);
        }

        public bool Remove(int ID)
        {
            var student = GetByID(ID);
            if (student == null)
            {
                return false;
                
            }
            _context.Students.Remove(student);
            return true;
        }

        public IEnumerable<Student> GetByGroup(string group)
        {
            return _context.Students.Where(s => s.Group == group).ToList();
        }
    }
}