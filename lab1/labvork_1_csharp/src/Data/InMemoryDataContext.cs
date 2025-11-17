using labvork_1_csharp.Models;

namespace labvork_1_csharp.Data
{
    // Паттерн Singleton (Одиночка)
    public sealed class InMemoryDataContext
    {
        private static readonly Lazy<InMemoryDataContext> _instance = new Lazy<InMemoryDataContext>(() 
            => new InMemoryDataContext());
        public List<Course> Courses { get; set; } = new List<Course>();
        public List<Teacher> Teachers { get; set; } = new List<Teacher>();
        public List<Student> Students { get; set; } = new List<Student>();

        private InMemoryDataContext()
        {
            SeedData();
        }
        
        public static InMemoryDataContext Instance => _instance.Value;

        private void SeedData()
        {
            var programmingTeacher = new Teacher { PersonID = 1, PersonFirstName = "Иван", PersonLastName = "Петров", Department = "Программирование" };
            var designTeacher = new Teacher { PersonID = 2, PersonFirstName = "Мария", PersonLastName = "Иванова", Department = "Дизайн" };
            var algorithmsTeacher = new Teacher { PersonID = 3, PersonFirstName = "Алексей", PersonLastName = "Сидоров", Department = "Компьютерные науки" };

            Teachers.Add(programmingTeacher);
            Teachers.Add(designTeacher);
            Teachers.Add(algorithmsTeacher);

            var student1 = new Student { PersonID = 1, PersonFirstName = "Анна", PersonLastName = "Кузнецова", Group = "M3201" };
            var student2 = new Student { PersonID = 2, PersonFirstName = "Дмитрий", PersonLastName = "Васильев", Group = "M3201" };
            var student3 = new Student { PersonID = 3, PersonFirstName = "София", PersonLastName = "Петрова", Group = "M3202" };
            var student4 = new Student { PersonID = 4, PersonFirstName = "Максим", PersonLastName = "Николаев", Group = "M3203" };
            var student5 = new Student { PersonID = 5, PersonFirstName = "Екатерина", PersonLastName = "Смирнова", Group = "M3203" };

            Students.Add(student1);
            Students.Add(student2);
            Students.Add(student3);
            Students.Add(student4);
            Students.Add(student5);

            var csharpCourse = new OnlineCourse 
            { 
                CourseID = 1, 
                CourseName = "C# для начинающих", 
                CourseDescription = "Основы языка C# и .NET платформы", 
                VideoConferenceLink = "https://backboost.courses.itmo.ru/",
                Platform = "backboost"
            };
            csharpCourse.AssignTeacher(programmingTeacher);

            var webDesignCourse = new OfflineCourse 
            { 
                CourseID = 2, 
                CourseName = "Веб-дизайн", 
                CourseDescription = "Создание современных пользовательских интерфейсов",
                Classroom = "101",
                Building = "Кронверская, 49",
                StartTime = "10:00",
                EndTime = "11:30"
            };
            webDesignCourse.AssignTeacher(designTeacher);

            var algorithmsCourse = new OfflineCourse 
            { 
                CourseID = 3, 
                CourseName = "Алгоритмы и структуры данных", 
                CourseDescription = "Изучение основных алгоритмов и структур данных",
                Classroom = "205", 
                Building = "Ломоносова, 9",
                StartTime = "14:00",
                EndTime = "15:30"
            };
            algorithmsCourse.AssignTeacher(algorithmsTeacher);

            var advancedCsharpCourse = new OnlineCourse 
            { 
                CourseID = 4, 
                CourseName = "Продвинутый C#", 
                CourseDescription = "Углубленное изучение языка и современных практик",
                VideoConferenceLink = "https://backboost.courses.itmo.ru/",
                Platform = "backboost"
            };
            advancedCsharpCourse.AssignTeacher(programmingTeacher);

            Courses.Add(csharpCourse);
            Courses.Add(webDesignCourse);
            Courses.Add(algorithmsCourse);
            Courses.Add(advancedCsharpCourse);

            csharpCourse.AddStudent(student1);
            csharpCourse.AddStudent(student2);
            csharpCourse.AddStudent(student3);

            webDesignCourse.AddStudent(student2);
            webDesignCourse.AddStudent(student4);

            algorithmsCourse.AddStudent(student1);
            algorithmsCourse.AddStudent(student3);
            algorithmsCourse.AddStudent(student5);

            advancedCsharpCourse.AddStudent(student4);
            advancedCsharpCourse.AddStudent(student5);
        }
    }
}