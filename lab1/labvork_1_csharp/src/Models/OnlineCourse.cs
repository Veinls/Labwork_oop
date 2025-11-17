namespace labvork_1_csharp.Models
{
    public class OnlineCourse: Course
    {
        public string VideoConferenceLink { get; set; } = string.Empty;
        public string Platform { get; set; } = "YouTube";

        public override string GetCourseType()
        {
            return "Online course";
        }

        public override string ToString()
        {
            return base.ToString() + $" – {Platform}";
        }
    }
}