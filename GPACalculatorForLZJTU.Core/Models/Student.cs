namespace GPACalculatorForLZJTU.Core.Models
{
    public class Student
    {
        public Student(int id, string name, string majorName)
        {
            Id = id;
            Name = name
                ?? throw new ArgumentNullException(nameof(name));
            MajorName = majorName
                ?? throw new ArgumentNullException(nameof(majorName));
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string MajorName { get; set; }
        public double GradePointAverage { get; set; }
        public IEnumerable<Course>? Courses { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Name}";
        }
    }
}
