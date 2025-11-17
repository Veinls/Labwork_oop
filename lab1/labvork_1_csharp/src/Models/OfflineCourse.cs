namespace labvork_1_csharp.Models
{
    public class OfflineCourse: Course
    {
        public string Classroom { get; set; } = string.Empty;
        public string Building { get; set; } = "Кронверская, 49";
        public string? StartTime {  get; set; } 
        public string? EndTime {  get; set; }
            
        public override string GetCourseType()
        {
           return "Offline course";
        }

        public override string ToString()
        {
            return base.ToString() + $" – {Building}, аудитория {Classroom} ";
        }

        public string GetCourseInfo()
        {
            var timeInfo = (StartTime != null && EndTime != null) ? $"Время: {StartTime} – {EndTime}" : "Время не установлено";
            return $"Корпус: {Building}. Аудитория: {Classroom}. {timeInfo}";
        }
    }
}