namespace DC.Lab;

public class GroupSubQuery
{
    public static void Execute()
    {
        var students = Student.students;

        // Query syntax
        var queryGroupMax =
            from student in students
            group student by student.Year into studentGroup
            select new
            {
                Level = studentGroup.Key,
                HighestScore = (
                    from student2 in studentGroup
                    select student2.ExamScores.Average()
                ).Max()
            };

        // Method syntax
        var queryGroupMin =
            students
                .GroupBy(s => s.Year)
                .Select(studentGroup => new
                {
                    Level = studentGroup.Key,
                    HighestScore = studentGroup
                        .Select(s2 => s2.ExamScores.Average()).Min()
                });

        int count = queryGroupMax.Count();
        Console.WriteLine($"Number of groups = {count}");

        foreach (var item in queryGroupMax)
        {
            Console.WriteLine($"    {item.Level} Highest Score = {item.HighestScore}");
        }
    }
}
