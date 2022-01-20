using ClosedXML.Excel;
using GPACalculatorForLZJTU.Core.Models;

Console.WriteLine("Please input file path");
var fileName = Console.ReadLine();
Console.WriteLine("Read list.xlsx");

var workBook = new XLWorkbook(fileName);
var workSheet = workBook.Worksheet("Sheet1");

var rows = workSheet.Rows(2, workSheet.RowCount() - 2);

var majorList = new [] { "车辆工程", "车辆工程(詹)", "车辆工程（卓越计划班）"};

var result = rows.GroupBy(stu => new Student(
        stu.Cell(1).GetValue<int>(), stu.Cell(2).GetString(), stu.Cell(13).GetString()),
    row => new Course()
    {
        CourseName = row.Cell(13).GetString(),
        Credit = row.Cell(4).GetDouble(),
        Sorce = row.Cell(5).GetDouble(),
        CourseType = row.Cell(6).GetString(),
        IsRetake = row.Cell(8).GetValue<int>() == 1
    },
    (stu, courses) => {
        courses = courses
            .Where(course => course.CourseType == "必修")
            .Where(course => course.IsRetake == false);

        stu.Courses = courses;
        stu.GradePointAverage = courses.Select(x => x.Sorce * x.Credit).Sum() / courses.Sum(course => course.Credit);
        return stu;
    })
    .Where(stu => majorList.Contains(stu.MajorName));

foreach (var item in result)
{

}