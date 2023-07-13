namespace DC.Lab;

public class NestedGroup
{
    public static void Execute()
    {
        var students = Student.students;
        var nestedGroupQuery =
            from student in students
            group student by student.Year into newGroup1
            from newGroup2 in (
                from student in newGroup1
                group student by student.LastName
            )
            group newGroup2 by newGroup1.Key;


        foreach (var outerGroup in nestedGroupQuery)
        {
            Console.WriteLine($"DataClass.Student Level = {outerGroup.Key}");

            foreach (var innerGroup in outerGroup)
            {
                Console.WriteLine($"\tNames that begin with: {innerGroup.Key}");

                foreach (var innerGroupElement in innerGroup)
                {
                    Console.WriteLine($"\t\t{innerGroupElement.LastName} {innerGroupElement.FirstName}");
                }
            }
        }
    }
}
