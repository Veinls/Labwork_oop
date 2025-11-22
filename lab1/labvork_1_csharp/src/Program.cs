using labvork_1_csharp.Models;
using labvork_1_csharp.Data;
using labvork_1_csharp.Services.Implementations;
using labvork_1_csharp.Services.Implementations.Courses;
using labvork_1_csharp.Services.Interfaces;

class Program
{
    static void Main()
    {
        var context = InMemoryDataContext.Instance;
        var studentService = new StudentService(context);
        var teacherService = new TeacherService(context);
        var courseManager = new CourseManager(context);
        var enrollmentManager = new StudentEnrollmentManager(courseManager, studentService);
        var assignmentManager = new TeacherAssignmentManager(courseManager, teacherService);

        Console.WriteLine("===================================\n");
        Console.WriteLine("СИСТЕМА УПРАВЛЕНИЯ");
        Console.WriteLine("===================================\n");

        Console.WriteLine("НАЧАЛЬНОЕ СОСТОЯНИЕ СИСТЕМЫ");
        Console.WriteLine("---------------------------");
        DisplayCoursesWithDetails(courseManager, enrollmentManager, assignmentManager);
        DisplayStudentsByGroup(studentService);
        DisplayTeachersByDepartment(teacherService);

        Console.WriteLine("\nОПЕРАЦИИ ПОИСКА");
        Console.WriteLine("----------------");
        DemonstrateSearchOperations(studentService, teacherService, courseManager);

        Console.WriteLine("\nОПЕРАЦИИ СО СТУДЕНТАМИ");
        Console.WriteLine("----------------------");
        DemonstrateStudentOperations(studentService, enrollmentManager, courseManager);
        
        Console.WriteLine("\nВСЕ КУРСЫ СТУДЕНА");
        Console.WriteLine("----------------------");
        DisplayStudentCourses(studentService, enrollmentManager, courseManager);

        Console.WriteLine("\nОПЕРАЦИИ С ПРЕПОДАВАТЕЛЯМИ");
        Console.WriteLine("---------------------------");
        DemonstrateTeacherOperations(teacherService, assignmentManager);
        
        Console.WriteLine("\nВСЕ КУРСЫ ПРЕПОДАВАТЕЛЯ");
        Console.WriteLine("----------------------");
        DisplayTeacherCourses(teacherService, assignmentManager, courseManager);

        Console.WriteLine("\nОПЕРАЦИИ С КУРСАМИ");
        Console.WriteLine("------------------");
        DemonstrateCourseOperations(courseManager,  assignmentManager);

        Console.WriteLine("\nФИНАЛЬНАЯ СТАТИСТИКА");
        Console.WriteLine("--------------------");
        DisplayFinalStatistics(courseManager, studentService, teacherService, enrollmentManager, assignmentManager);
    }

    static void DisplayCoursesWithDetails(CourseManager courseManager, StudentEnrollmentManager enrollmentManager, TeacherAssignmentManager assignmentManager)
    {
        Console.WriteLine("Курсы с детальной информацией:");
        foreach (var course in courseManager.GetAll())
        {
            var teacher = assignmentManager.GetRelatedEntities(course.CourseID).FirstOrDefault();
            var students = enrollmentManager.GetRelatedEntities(course.CourseID);
            
            Console.WriteLine($"  {course.CourseName}");
            Console.WriteLine($"    Тип: {course.GetCourseType()}");
            Console.WriteLine($"    Преподаватель: {teacher?.PersonFullName ?? "Не назначен"}");
            Console.WriteLine($"    Студентов: {students.Count()}");
            
            if (course is OfflineCourse offline)
                Console.WriteLine($"   Корпус: {offline.Building} Аудитория:, {offline.Classroom}");
            else if (course is OnlineCourse online)
                Console.WriteLine($"    Платформа: {online.Platform}");
        }
        Console.WriteLine();
    }

    static void DisplayStudentsByGroup(StudentService studentService)
    {
        Console.WriteLine("Студенты по группам:");
        var groups = studentService.GetAll().Select(s => s.Group).Distinct();
        foreach (var group in groups)
        {
            var students = studentService.GetByGroup(group);
            Console.WriteLine($"  Группа {group}: {students.Count()} студентов");
            foreach (var student in students)
                Console.WriteLine($"    - {student.PersonFullName}");
        }
        Console.WriteLine();
    }

    static void DisplayTeachersByDepartment(TeacherService teacherService)
    {
        Console.WriteLine("Преподаватели по кафедрам:");
        var departments = teacherService.GetAll().Select(t => t.Department).Distinct();
        foreach (var department in departments)
        {
            var teachers = teacherService.GetByDepartment(department);
            Console.WriteLine($"  Кафедра {department}: {teachers.Count()} преподавателей");
            foreach (var teacher in teachers)
                Console.WriteLine($"    - {teacher.PersonFullName}");
        }
        Console.WriteLine();
    }

    static void DemonstrateSearchOperations(StudentService studentService, TeacherService teacherService, CourseManager courseManager)
    {
        Console.WriteLine("Поиск студента по ID 2: " + studentService.GetByID(2)?.PersonFullName);
        Console.WriteLine("Поиск преподавателя по ID 1: " + teacherService.GetByID(1)?.PersonFullName);
        Console.WriteLine("Поиск курса по ID 3: " + courseManager.GetByID(3)?.CourseName);
        
        var nonExistent = studentService.GetByID(666);
        Console.WriteLine("Поиск несуществующего студента: " + (nonExistent == null ? "Не найден" : "Найден"));
    }

    static void DemonstrateStudentOperations(StudentService studentService, StudentEnrollmentManager enrollmentManager, CourseManager courseManager)
    {
        Console.WriteLine("Добавление нового студента:");
        var newStudent = new Student { PersonFirstName = "Артем", PersonLastName = "Новиков", Group = "M3204" };
        studentService.Add(newStudent);
        Console.WriteLine("  Добавлен: " + newStudent.PersonFullName);
        var becomeStudents = studentService.GetAll().Count();
        Console.WriteLine("  Стало студентов: " + becomeStudents);
        Console.WriteLine("Запись студента на курс Алгоритмы:");
        enrollmentManager.AddRelationship(3, 6);
        var algorithmStudents = enrollmentManager.GetRelatedEntities(3);
        Console.WriteLine("  Студентов на курсе: " + algorithmStudents.Count());
        Console.WriteLine("Отмена записи студента:");
        enrollmentManager.RemoveRelationship(3, 6);
        algorithmStudents = enrollmentManager.GetRelatedEntities(3);
        Console.WriteLine("  Студентов после отмены: " + algorithmStudents.Count());

        Console.WriteLine("Удаление студента:");
        studentService.Remove(6);
        var remainingStudents = studentService.GetAll().Count();
        Console.WriteLine("  Осталось студентов: " + remainingStudents);
    }
    
    static void DisplayStudentCourses(StudentService studentService, StudentEnrollmentManager enrollmentManager, CourseManager courseManager)
    {
        foreach (var student in studentService.GetAll())
        {
            var studentCourses = courseManager.GetAll().Where(course => enrollmentManager.GetRelatedEntities(course.CourseID).Any(s => s.PersonID == student.PersonID)).ToList();
            
            Console.WriteLine($"Студент: {student.PersonFullName} (Группа: {student.Group})");
            Console.WriteLine($"Курсов: {studentCourses.Count}");
            foreach (var course in studentCourses)
            {
                Console.WriteLine($"  - {course.CourseName} ({course.GetCourseType()})");
            }
            Console.WriteLine();
        }
    }

    static void DemonstrateTeacherOperations(TeacherService teacherService, TeacherAssignmentManager assignmentManager)
    {
        Console.WriteLine("Добавление нового преподавателя:");
        var newTeacher = new Teacher { PersonFirstName = "Сергей", PersonLastName = "Волков", Department = "Математика" };
        teacherService.Add(newTeacher);
        Console.WriteLine("  Добавлен: " + newTeacher.PersonFullName);

        Console.WriteLine("Назначение преподавателя на курс:");
        assignmentManager.AddRelationship(4, 4);
        var courseTeacher = assignmentManager.GetRelatedEntities(4).FirstOrDefault();
        Console.WriteLine("  Преподаватель курса: " + courseTeacher?.PersonFullName);

        Console.WriteLine("Смена преподавателя:");
        assignmentManager.RemoveRelationship(4, 4);
        assignmentManager.AddRelationship(4, 3);
        courseTeacher = assignmentManager.GetRelatedEntities(4).FirstOrDefault();
        Console.WriteLine("  Новый преподаватель: " + courseTeacher?.PersonFullName);
    }
    
    static void DisplayTeacherCourses(TeacherService teacherService, TeacherAssignmentManager assignmentManager, CourseManager courseManager)
    {
        foreach (var teacher in teacherService.GetAll())
        {
            var teacherCourses = courseManager.GetAll().Where(course => assignmentManager.GetRelatedEntities(course.CourseID).Any(t => t.PersonID == teacher.PersonID)).ToList();
            
            Console.WriteLine($"Преподаватель: {teacher.PersonFullName}");
            Console.WriteLine($"Курсов: {teacherCourses.Count}");
            foreach (var course in teacherCourses)
            {
                Console.WriteLine($"  - {course.CourseName} ({course.GetCourseType()})");
            }
            Console.WriteLine();
        }
    }

    static void DemonstrateCourseOperations(CourseManager courseManager, TeacherAssignmentManager assignmentManager)
    {
        Console.WriteLine("Создание нового курса:");
        var newCourse = new OnlineCourse 
        { 
            CourseName = "Базы данных", 
            CourseDescription = "Основы работы с базами данных",
            VideoConferenceLink = "какая то ссылка"
        };
        courseManager.Add(newCourse);
        Console.WriteLine("  Создан курс: " + newCourse.CourseName);
        
        var teacher = assignmentManager.GetRelatedEntities(newCourse.CourseID).FirstOrDefault();
        Console.WriteLine("  Преподаватель: " + (teacher?.PersonFullName ?? "Не назначен"));
        
        Console.WriteLine("Информация о онлайн-курсах:");
        foreach (var course in courseManager.GetAll().OfType<OnlineCourse>())
        {
            Console.WriteLine("  "  + course.CourseName + ":  Описание: " + course.CourseDescription + ", Платформа:  " + course.Platform + " – " + course.VideoConferenceLink);
        }

        Console.WriteLine("Информация о оффлайн-курсах:");
        foreach (var course in courseManager.GetAll().OfType<OfflineCourse>())
        {
            Console.WriteLine("  "  + course.CourseName + ":  " + course.GetCourseInfo());
        }
    }

    static void DisplayFinalStatistics(CourseManager courseManager, StudentService studentService, TeacherService teacherService, StudentEnrollmentManager enrollmentManager, TeacherAssignmentManager assignmentManager)
    {
        Console.WriteLine("Курсов в системе: " + courseManager.GetAll().Count());
        Console.WriteLine("Студентов в системе: " + studentService.GetAll().Count());
        Console.WriteLine("Преподавателей в системе: " + teacherService.GetAll().Count());
        
        var totalEnrollments = courseManager.GetAll()
            .Sum(course => enrollmentManager.GetRelatedEntities(course.CourseID).Count());
        Console.WriteLine("Всего записей на курсы: " + totalEnrollments);
        
        var coursesWithTeachers = courseManager.GetAll()
            .Count(course => assignmentManager.GetRelatedEntities(course.CourseID).Any());
        Console.WriteLine("Курсов с назначенными преподавателями: " + coursesWithTeachers);
    }
}