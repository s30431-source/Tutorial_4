using LinqConsoleLab.EN.Data;

namespace LinqConsoleLab.EN.Exercises;

public sealed class LinqExercises
{
    public IEnumerable<string> Task01_StudentsFromWarsaw()
    {
        return UniversityData.Students
            .Where(s => s.City == "Warsaw")
            .Select(s => $"{s.IndexNumber}: {s.FirstName} {s.LastName} ({s.City})");
    }
    public IEnumerable<string> Task02_StudentEmailAddresses()
    {
        return UniversityData.Students
            .Select(s => s.Email);
    }
    
    public IEnumerable<string> Task03_StudentsSortedAlphabetically()
    {
        return UniversityData.Students
            .OrderBy(s => s.LastName)
            .ThenBy(s => s.FirstName)
            .Select(s => $"{s.IndexNumber}: {s.LastName}, {s.FirstName}");
    }
    
    public IEnumerable<string> Task04_FirstAnalyticsCourse()
    {
        var course = UniversityData.Courses
            .FirstOrDefault(c => c.Category == "Analytics");

        return course != null 
            ? new[] { $"Found: {course.Title} (Starts: {course.StartDate:yyyy-MM-dd})" }
            : new[] { "No Analytics course found." };
    }
    public IEnumerable<string> Task05_IsThereAnyInactiveEnrollment()
    {
        bool exists = UniversityData.Enrollments.Any(e => !e.IsActive);
        return new[] { exists ? "Yes, there are inactive enrollments." : "No, all enrollments are active." };
    }
    public IEnumerable<string> Task06_DoAllLecturersHaveDepartment()
    {
        bool allSet = UniversityData.Lecturers.All(l => !string.IsNullOrWhiteSpace(l.Department));
        return new[] { allSet ? "True: All lecturers have departments." : "False: Some departments are missing." };
    }


    public IEnumerable<string> Task07_CountActiveEnrollments()
    {
        int count = UniversityData.Enrollments.Count(e => e.IsActive);
        return new[] { $"Total active enrollments: {count}" };
    }
    public IEnumerable<string> Task08_DistinctStudentCities()
    {
        return UniversityData.Students
            .Select(s => s.City)
            .Distinct()
            .OrderBy(city => city);
    }
    
    public IEnumerable<string> Task09_ThreeNewestEnrollments() 
    {
        return UniversityData.Enrollments
            .OrderByDescending(e => e.EnrollmentDate)
            .Take(3)
            .Select(e => $"Date: {e.EnrollmentDate:yyyy-MM-dd} | Student: {e.StudentId} | Course: {e.CourseId}");
    }
    
    public IEnumerable<string> Task10_SecondPageOfCourses()
    {
        return UniversityData.Courses
            .OrderBy(c => c.Title)
            .Skip(2)
            .Take(2)
            .Select(c => $"{c.Title} ({c.Category})");
    }
    public IEnumerable<string> Task11_JoinStudentsWithEnrollments()
    {
        return UniversityData.Students
            .Join(UniversityData.Enrollments,
                s => s.Id,
                e => e.StudentId,
                (s, e) => $"{s.FirstName} {s.LastName} enrolled on {e.EnrollmentDate:yyyy-MM-dd}");
    }
    public IEnumerable<string> Task12_StudentCoursePairs()
    {
        return UniversityData.Enrollments
            .Join(UniversityData.Students, e => e.StudentId, s => s.Id, (e, s) => new { e, s })
            .Join(UniversityData.Courses, combined => combined.e.CourseId, c => c.Id, 
                (combined, c) => $"{combined.s.FirstName} {combined.s.LastName} -> {c.Title}");
    }
    public IEnumerable<string> Task13_GroupEnrollmentsByCourse()
    {
        return UniversityData.Enrollments
            .Join(UniversityData.Courses, e => e.CourseId, c => c.Id, (e, c) => c.Title)
            .GroupBy(title => title)
            .Select(g => $"Course: {g.Key} | Enrollments: {g.Count()}");
    }
    public IEnumerable<string> Task14_AverageGradePerCourse()
    {
        return UniversityData.Enrollments
            .Where(e => e.FinalGrade.HasValue)
            .Join(UniversityData.Courses, e => e.CourseId, c => c.Id, (e, c) => new { c.Title, e.FinalGrade })
            .GroupBy(x => x.Title)
            .Select(g => $"Course: {g.Key} | Avg Grade: {g.Average(x => x.FinalGrade):F2}");
    }
    public IEnumerable<string> Task15_LecturersAndCourseCounts()
    {
        return UniversityData.Lecturers
            .GroupJoin(UniversityData.Courses,
                l => l.Id,
                c => c.LecturerId,
                (l, courses) => $"{l.FirstName} {l.LastName} | Courses: {courses.Count()}");
    }
    public IEnumerable<string> Task16_HighestGradePerStudent()
    {
        return UniversityData.Enrollments
            .Where(e => e.FinalGrade.HasValue)
            .Join(UniversityData.Students, e => e.StudentId, s => s.Id, (e, s) => new { s.FirstName, s.LastName, e.FinalGrade })
            .GroupBy(x => new { x.FirstName, x.LastName })
            .Select(g => $"{g.Key.FirstName} {g.Key.LastName} | Best Grade: {g.Max(x => x.FinalGrade)}");
    }
    public IEnumerable<string> Challenge01_StudentsWithMoreThanOneActiveCourse()
    {
        return UniversityData.Students
            .Join(UniversityData.Enrollments, s => s.Id, e => e.StudentId, (s, e) => new { s, e })
            .Where(x => x.e.IsActive)
            .GroupBy(x => new { x.s.FirstName, x.s.LastName })
            .Where(g => g.Count() > 1)
            .Select(g => $"{g.Key.FirstName} {g.Key.LastName} | Active Courses: {g.Count()}");
    }

    public IEnumerable<string> Challenge02_AprilCoursesWithoutFinalGrades()
    {
        return UniversityData.Courses
            .Where(c => c.StartDate.Month == 4 && c.StartDate.Year == 2026)
            .GroupJoin(UniversityData.Enrollments, 
                c => c.Id, 
                e => e.CourseId, 
                (c, enrollments) => new { c.Title, enrollments })
            .Where(x => x.enrollments.All(e => e.FinalGrade == null))
            .Select(x => $"Course: {x.Title} (No grades assigned yet)");
    }

    public IEnumerable<string> Challenge03_LecturersAndAverageGradeAcrossTheirCourses()
    {
        return UniversityData.Lecturers
            .Join(UniversityData.Courses, l => l.Id, c => c.LecturerId, (l, c) => new { l, c })
            .Join(UniversityData.Enrollments, combined => combined.c.Id, e => e.CourseId, (combined, e) => new { combined.l, e.FinalGrade })
            .Where(x => x.FinalGrade.HasValue)
            .GroupBy(x => new { x.l.FirstName, x.l.LastName })
            .Select(g => $"Lecturer: {g.Key.FirstName} {g.Key.LastName} | Total Avg Grade: {g.Average(x => x.FinalGrade):F2}");
    }

    public IEnumerable<string> Challenge04_CitiesAndActiveEnrollmentCounts()
    {
        return UniversityData.Students
            .Join(UniversityData.Enrollments, s => s.Id, e => e.StudentId, (s, e) => new { s.City, e.IsActive })
            .Where(x => x.IsActive)
            .GroupBy(x => x.City)
            .Select(g => new { City = g.Key, Count = g.Count() })
            .OrderByDescending(res => res.Count)
            .Select(res => $"City: {res.City} | Active Enrollments: {res.Count}");
    }

    private static NotImplementedException NotImplemented(string methodName)
    {
        return new NotImplementedException(
            $"Complete method {methodName} in Exercises/LinqExercises.cs and run the command again.");
    }
}