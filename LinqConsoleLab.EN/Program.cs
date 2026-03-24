using LinqConsoleLab.EN.Data;
using LinqConsoleLab.EN.Exercises;
UniversityData.Seed();
var exercises = new LinqExercises();
while (true)
{
    Console.Clear();
    Console.WriteLine("=== UNIVERSITY DATA ANALYSIS (LINQ) ===");
    Console.WriteLine("0. Data Overview");
    Console.WriteLine("1. Students from Warsaw");
    Console.WriteLine("2. Student email addresses");
    Console.WriteLine("3. Students sorted alphabetically");
    Console.WriteLine("4. First Analytics course");
    Console.WriteLine("5. Inactive enrollments check");
    Console.WriteLine("13. Group enrollments by course");
    Console.WriteLine("17. [CHALLENGE] More than one active course");
    Console.WriteLine("18. [CHALLENGE] April 2026 courses without grades");
    Console.WriteLine("q. Exit");
    Console.Write("\nSelect option: ");
    var input = Console.ReadLine()?.ToLower();
    if (input == "q") break;
    IEnumerable<string> results = input switch
    {
        "0" => UniversityData.GetOverview(),
        "1" => exercises.Task01_StudentsFromWarsaw(),
        "2" => exercises.Task02_StudentEmailAddresses(),
        "3" => exercises.Task03_StudentsSortedAlphabetically(),
        "4" => exercises.Task04_FirstAnalyticsCourse(),
        "5" => exercises.Task05_IsThereAnyInactiveEnrollment(),
        "13" => exercises.Task13_GroupEnrollmentsByCourse(),
        "17" => exercises.Challenge01_StudentsWithMoreThanOneActiveCourse(),
        "18" => exercises.Challenge02_AprilCoursesWithoutFinalGrades(),
        _ => new[] { "Invalid option or not implemented yet." }
    };
    Console.WriteLine("\n--- RESULTS ---");
    foreach (var line in results) Console.WriteLine(line);
    
    Console.WriteLine("\nPress any key to return to menu...");
    Console.ReadKey();
}