namespace DC.Lab;

public class GroupQuery
{
    public static void Execute()
    {
        var students = Student.students;

        var groupByLastNameQuery =
            from student in students
            group student by student.LastName into newGroup
            orderby newGroup.Key
            select newGroup;

        Console.WriteLine("Sorting by last name");

        foreach (var nameGroup in groupByLastNameQuery)
        {
            Console.WriteLine($"Key: {nameGroup.Key}");

            foreach (var student in nameGroup)
            {
                Console.WriteLine($"\t{student.LastName} {student.FirstName}");
            }
        }

        var groupByFirstLetterQuery =
            from student in students
            group student by student.LastName[0];

        Console.WriteLine("\nSorting by first letter of the last name");

        foreach (var studentGroup in groupByFirstLetterQuery)
        {
            Console.WriteLine($"Key: {studentGroup.Key}");

            foreach (var student in studentGroup)
            {
                Console.WriteLine($"\t{student.LastName} {student.FirstName}");
            }
        }

        Console.WriteLine("\nSorting by percentile");

        var groupByPercentileQuery =
            from student in students
            let percentile = GetPercentile(student)
            group new
            {
                student.FirstName,
                student.LastName
            } by percentile into percentGroup
            orderby percentGroup.Key
            select percentGroup;


        foreach (var studentGroup in groupByPercentileQuery)
        {
            Console.WriteLine($"Key: {studentGroup.Key * 10}");

            foreach (var student in studentGroup)
            {
                Console.WriteLine($"\t{student.LastName} {student.FirstName}");
            }
        }

        Console.WriteLine("\nSorting by average");

        var groupByHighAverageQuery =
            from student in students
            group new
            {
                student.FirstName,
                student.LastName
            } by student.ExamScores.Average() > 75 into studentGroup
            select studentGroup;

        foreach (var studentGroup in groupByHighAverageQuery)
        {
            Console.WriteLine($"Key: {studentGroup.Key}");

            foreach (var student in studentGroup)
            {
                Console.WriteLine($"\t{student.LastName} {student.FirstName}");
            }
        }

        Console.WriteLine("\nSorting by anonymous type");

        var groupByCompoundKey =
            from student in students
            group student by new
            {
                FirstLetter = student.LastName[0],
                IsScoreOver85 = student.ExamScores[0] > 85
            } into studentGroup
            orderby studentGroup.Key.FirstLetter
            select studentGroup;

        foreach (var scoreGroup in groupByCompoundKey)
        {
            string s = scoreGroup.Key.IsScoreOver85 == true ? "more than 85" : "less than 85";

            Console.WriteLine($"Name starts with {scoreGroup.Key.FirstLetter} who scored {s}");

            foreach (var item in scoreGroup)
            {
                Console.WriteLine($"\t{item.FirstName} {item.LastName}");
            }
        }
    }

    private static int GetPercentile(Student s)
    {
        double avg = s.ExamScores.Average();
        return avg > 0 ? (int)avg / 10 : 0;
    }
}
