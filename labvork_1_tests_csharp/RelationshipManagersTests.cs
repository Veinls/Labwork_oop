using Xunit;
using labvork_1_csharp.Models;
using labvork_1_csharp.Data;
using labvork_1_csharp.Services.Implementations;
using labvork_1_csharp.Services.Implementations.Courses;

namespace labvork_1_tests_csharp
{
    public class CoreRelationshipManagersTests
    {
        private readonly InMemoryDataContext _context;
        private readonly StudentService _studentService;
        private readonly TeacherService _teacherService;
        private readonly CourseManager _courseManager;
        private readonly StudentEnrollmentManager _enrollmentManager;
        private readonly TeacherAssignmentManager _assignmentManager;

        public CoreRelationshipManagersTests()
        {
            _context = InMemoryDataContext.Instance;
            _studentService = new StudentService(_context);
            _teacherService = new TeacherService(_context);
            _courseManager = new CourseManager(_context);
            _enrollmentManager = new StudentEnrollmentManager(_courseManager, _studentService);
            _assignmentManager = new TeacherAssignmentManager(_courseManager, _teacherService);
        }

        [Fact]
        public void StudentEnrollmentManager_AddAndRemoveRelationship_ShouldWork()
        {
            var courseId = 1;
            var studentId = 4;
            var initialCount = _enrollmentManager.GetRelatedEntities(courseId).Count();
    
            var addResult = _enrollmentManager.AddRelationship(courseId, studentId);
            var studentsAfterAdd = _enrollmentManager.GetRelatedEntities(courseId).Count();
 
            var removeResult = _enrollmentManager.RemoveRelationship(courseId, studentId);
            var studentsAfterRemove = _enrollmentManager.GetRelatedEntities(courseId).Count();

            Assert.True(addResult);
            Assert.True(removeResult);
            Assert.Equal(initialCount + 1, studentsAfterAdd);  
            Assert.Equal(initialCount, studentsAfterRemove);   
        }

        [Fact]
        public void TeacherAssignmentManager_AddRelationship_ShouldWork()
        {
            var result = _assignmentManager.AddRelationship(4, 2);
            var teacher = _assignmentManager.GetRelatedEntities(4).FirstOrDefault();

            Assert.True(result);
            Assert.NotNull(teacher);
        }
    }
}