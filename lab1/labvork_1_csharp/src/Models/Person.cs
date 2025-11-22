namespace labvork_1_csharp.Models
{
    public abstract class Person
    {
        public int PersonID { get; set; }
        public string PersonFirstName { get; set; } = string.Empty;
        public string PersonLastName { get; set; } = string.Empty;
        public string PersonFullName => $"{PersonFirstName} {PersonLastName}";
    }
}
