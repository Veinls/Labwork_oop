namespace labvork_1_csharp.Models
{
    public abstract class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string CourseDescription { get; set; } = string.Empty;
        
        public int? TeacherID { get; set; }
        public Teacher? Teacher { get; set; }
        
        public List<Student> Students { get; set; } = new List<Student>();

        public abstract string GetCourseType();

        public void AssignTeacher(Teacher teacher)
        {
            Teacher = teacher;
            TeacherID = teacher.PersonID;

            if (!teacher.Courses.Contains(this))
            {
                teacher.Courses.Add(this);
            }
        }

        public void RemoveTeacher()
        {
            if (Teacher != null)
            {
                Teacher.Courses.Remove(this);
                Teacher = null;
                TeacherID = null;
            }
        }

        public void AddStudent(Student student)
        {
            if (!Students.Contains(student))
            {
                Students.Add(student);
            }

            if (!student.Courses.Contains(this))
            {
                student.Courses.Add(this);
            }
            
        }

        public void RemoveStudent(Student student)
        {
            Students.Remove(student);
            student.Courses.Remove(this);
        }
        
        public void RemoveStudentByID(int studentID)
        {
            var student = Students.FirstOrDefault(student => student.PersonID == studentID);
            if (student != null)
            {
                RemoveStudent(student);
            }
        }

        public override string ToString()
        {
            return $"{CourseName}({GetCourseType()}) – {Teacher?.PersonFirstName ?? "Нет преподователя"}";
        }
    }
}