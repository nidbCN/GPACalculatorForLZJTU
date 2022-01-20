using System.Diagnostics.CodeAnalysis;

namespace GPACalculatorForLZJTU.Core.Models
{
    public class StudentEqualityComparer : EqualityComparer<Student>
    {
        public override bool Equals(Student? stu1, Student? stu2)
        {
            if (stu1 == null && stu2 == null)
                return true;
            else if (stu1 == null || stu2 == null)
                return false;

            return stu1.Id == stu2.Id;
        }

        public override int GetHashCode([DisallowNull] Student obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
