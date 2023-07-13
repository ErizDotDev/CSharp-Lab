namespace DC.Lab;

public class GroupSubQuery
{
    public static void Execute()
    {
        var students = Student.students;
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

        int count = queryGroupMax.Count();
        Console.WriteLine($"Number of groups = {count}");

        foreach (var item in queryGroupMax)
        {
            Console.WriteLine($"    {item.Level} Highest Score = {item.HighestScore}");
        }
    }
}
