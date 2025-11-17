using Xunit;
using labvork_1_csharp.Models;
using labvork_1_csharp.Data;
using labvork_1_csharp.Services.Implementations;
using labvork_1_csharp.Services.Implementations.Courses;

namespace labvork_1_tests_csharp
{
    public class CriticalIntegrationTest
    {
        [Fact]
        public void FullStudentLifecycle_ShouldWork()
        {
            var context = InMemoryDataContext.Instance;
            var studentService = new StudentService(context);
            var courseManager = new CourseManager(context);
            var enrollmentManager = new StudentEnrollmentManager(courseManager, studentService);

            var newStudent = new Student { PersonFirstName = "Test", PersonLastName = "User", Group = "M3205" };
            
            studentService.Add(newStudent);
            var enrollmentResult = enrollmentManager.AddRelationship(1, newStudent.PersonID);
            var studentRemovalResult = studentService.Remove(newStudent.PersonID);

            Assert.True(enrollmentResult);
            Assert.True(studentRemovalResult);
        }
    }
}