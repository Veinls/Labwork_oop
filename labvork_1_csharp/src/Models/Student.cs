namespace labvork_1_csharp.Models
{
    public class Student : Person
    {
        public string Group { get; set; } = string.Empty;

        public List<Course> Courses { get; set; } = new List<Course>();
    }
}