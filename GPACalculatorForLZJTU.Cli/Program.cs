using ClosedXML.Excel;
using GPACalculatorForLZJTU.Core.Models;

static double Switcher(string scoreStr)
    => int.TryParse(scoreStr, out var score) ? score : (scoreStr) switch
    {
        "优秀" => 90,
        "良好" => 80,
        "中等" => 70,
        "及格" => 60,
        "不及格" => 0,
        _ => 0,
    };

Console.WriteLine("Please input file path");
var fileName = Console.ReadLine();
Console.WriteLine("Read list.xlsx");

var workBook = new XLWorkbook(fileName);
var workSheet = workBook.Worksheet("Sheet1");

var rows = workSheet.RowsUsed();

// var rows = workSheet.Rows(2);

var majorList = new[] { "车辆工程", "车辆工程(詹)", "车辆工程（卓越计划班）" };

var result = rows.Skip(2)
    .Where(row => majorList.Contains(row.Cell(13).GetString()))
    .Where(row => row.Cell(6).GetString() == "必修")
    .Where(row => row.Cell(8).GetValue<int>() == 0)
    .GroupBy(row => row.Cell(1).GetString(),
    row => new Course()
    {
        CourseName = row.Cell(3).GetString(),
        Credit = row.Cell(4).GetDouble(),
        Sorce = Switcher(row.Cell(5).GetString()),
        CourseType = row.Cell(6).GetString(),
        IsRetake = row.Cell(8).GetValue<int>() == 1
    })
    .Select(group => {
        var student = new Student(group.Key, "", "")
        {
            GradePointAverage = group
            .Select(course => course.Sorce * course.Credit).Sum() / group.Sum(course => course.Credit)
        };
        return student;
    })
    .OrderBy(stu => stu.GradePointAverage);

foreach (var item in result)
{
    Console.WriteLine(item.ToString());
}