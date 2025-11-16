using Xunit;
using labvork_1_csharp.Models;
using labvork_1_csharp.Data;
using labvork_1_csharp.Services.Implementations;
using labvork_1_csharp.Services.Implementations.Courses;

namespace labvork_1_tests_csharp
{
    public class ModelsTests
    {
        [Fact]
        public void Course_AssignTeacher_ShouldWorkCorrectly()
        {
            var course = new OnlineCourse { CourseID = 1, CourseName = "CourseName" };
            var teacher = new Teacher { PersonID = 1, PersonFirstName = "FirstName", PersonLastName = "LastName" };

            course.AssignTeacher(teacher);

            Assert.Equal(teacher, course.Teacher);
            Assert.Contains(course, teacher.Courses);
        }

        [Fact]
        public void Course_AddStudent_ShouldWorkCorrectly()
        {
           var course = new OnlineCourse { CourseID = 1, CourseName = "CourseName" };
           var student = new Student { PersonID = 1, PersonFirstName = "FirstName", PersonLastName = "LastName" };
           
           course.AddStudent(student);
           
           Assert.Contains(student, course.Students);
           Assert.Contains(course, student.Courses);
        }
    }
}