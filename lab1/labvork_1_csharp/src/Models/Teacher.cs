namespace labvork_1_csharp.Models
{
    public class Teacher : Person
    {
        public string Department { get; set; } = string.Empty;
        
        public List<Course> Courses { get; set; } = new List<Course>();
    }
}