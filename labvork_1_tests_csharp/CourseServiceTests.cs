using Xunit;
using labvork_1_csharp.Models;
using labvork_1_csharp.Data;
using labvork_1_csharp.Services.Implementations;
using labvork_1_csharp.Services.Implementations.Courses;

namespace labvork_1_tests_csharp
{
   public class ServicesTests
   {
      private readonly InMemoryDataContext _context;
      private readonly StudentService _studentService;
      private readonly TeacherService _teacherService;
      private readonly CourseManager _courseManager;

      public ServicesTests()
      {
         _context = InMemoryDataContext.Instance;
         _studentService = new StudentService(_context);
         _teacherService = new TeacherService(_context);
         _courseManager = new CourseManager(_context);
      }
      
      [Fact]
      public void StudentService_AddAndGet_ShouldWork()
      {
         var newStudent = new Student { PersonFirstName = "Test", PersonLastName = "Student", Group = "M3205" };

         _studentService.Add(newStudent);
         var retrieved = _studentService.GetByID(newStudent.PersonID);

         Assert.NotNull(retrieved);
         Assert.Equal("Test Student", retrieved.PersonFullName);
         
         _studentService.Remove(newStudent.PersonID);
      }

      [Fact]
      public void TeacherService_Remove_ShouldRemoveTeacherAndClearCourses()
      {
         var teacher = new Teacher { PersonFirstName = "Temp", PersonLastName = "Teacher", Department = "Test" };
         _teacherService.Add(teacher);
         var teacherID = teacher.PersonID;

         var result = _teacherService.Remove(teacherID);

         Assert.True(result);
         Assert.Null(_teacherService.GetByID(teacherID));
      }

      [Fact]
      public void CourseManager_GetAll_ShouldReturnCourses()
      {
         var courses = _courseManager.GetAll().ToList();
         
         Assert.NotEmpty(courses);
         Assert.All(courses, c => Assert.NotNull(c.CourseName));
      }
   } 
}