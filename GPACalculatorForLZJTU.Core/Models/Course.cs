namespace GPACalculatorForLZJTU.Core.Models
{
    public class Course
    {
        public string? CourseName { get; set; }
        public double Credit { get; set; }
        public double Sorce { get; set; }
        public string? CourseType { get; set; }
        public bool IsRetake { get; set; }
        public Student Student { get; set; }
    }
}
