using LinqConsoleLab.EN.Data;
using LinqConsoleLab.EN.Exercises;
UniversityData.Seed();
var exercises = new LinqExercises();

while (true)
{
    Console.Clear();
    Console.WriteLine("   UNIVERSITY DATA ANALYSIS SYSTEM (LINQ)      ");
    Console.WriteLine("===============================================");
    Console.WriteLine("0.  Data Overview (Check Seeded Data)");
    Console.WriteLine("1.  Students from Warsaw");
    Console.WriteLine("2.  Student email addresses");
    Console.WriteLine("3.  Students sorted alphabetically (Last, First)");
    Console.WriteLine("4.  First Analytics course (FirstOrDefault)");
    Console.WriteLine("5.  Check for any inactive enrollments (Any)");
    Console.WriteLine("6.  Check if all lecturers have departments (All)");
    Console.WriteLine("7.  Count total active enrollments (Count)");
    Console.WriteLine("8.  Distinct list of student cities (Distinct)");
    Console.WriteLine("9.  Three newest enrollments (Take)");
    Console.WriteLine("10. Second page of courses (Skip/Take)");
    Console.WriteLine("11. Students joined with enrollment dates");
    Console.WriteLine("12. Student-Course pairs (SelectMany style)");
    Console.WriteLine("13. Enrollment count per course (GroupBy)");
    Console.WriteLine("14. Average final grade per course");
    Console.WriteLine("15. Lecturers and their assigned course counts");
    Console.WriteLine("16. Highest final grade for each student");
    Console.WriteLine("17. [CHALLENGE] Students with >1 active course");
    Console.WriteLine("18. [CHALLENGE] April 2026 courses with no grades");
    Console.WriteLine("19. [CHALLENGE] Lecturer avg grade across all courses");
    Console.WriteLine("20. [CHALLENGE] Cities ranked by active enrollments");
    Console.WriteLine("q.  Exit Application");
    Console.Write("\nSelect an option (0-20): ");

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
        "6" => exercises.Task06_DoAllLecturersHaveDepartment(),
        "7" => exercises.Task07_CountActiveEnrollments(),
        "8" => exercises.Task08_DistinctStudentCities(),
        "9" => exercises.Task09_ThreeNewestEnrollments(),
        "10" => exercises.Task10_SecondPageOfCourses(),
        "11" => exercises.Task11_JoinStudentsWithEnrollments(),
        "12" => exercises.Task12_StudentCoursePairs(),
        "13" => exercises.Task13_GroupEnrollmentsByCourse(),
        "14" => exercises.Task14_AverageGradePerCourse(),
        "15" => exercises.Task15_LecturersAndCourseCounts(),
        "16" => exercises.Task16_HighestGradePerStudent(),
        "17" => exercises.Challenge01_StudentsWithMoreThanOneActiveCourse(),
        "18" => exercises.Challenge02_AprilCoursesWithoutFinalGrades(),
        "19" => exercises.Challenge03_LecturersAndAverageGradeAcrossTheirCourses(),
        "20" => exercises.Challenge04_CitiesAndActiveEnrollmentCounts(),
    };
    Console.WriteLine("\n>>> RESULTS:");
    foreach (var line in results) 
    {
        Console.WriteLine(line);
    }
    Console.WriteLine("\nPress any key to return to the menu...");
    Console.ReadKey();
}