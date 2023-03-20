using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

public class Student
{
    public int StudentID { get; set; }
    public string StudentName { get; set; }
    public int Age { get; set; }
    public string Major { get; set; }
    public double Tuition { get; set; }
}
public class StudentClubs
{
    public int StudentID { get; set; }
    public string ClubName { get; set; }
}
public class StudentGPA
{
    public int StudentID { get; set; }
    public double GPA { get; set; }
}
public class StudentComparerRow : IEqualityComparer<Student>
{
    public bool Equals(Student x, Student y)
    {
        if (x.StudentID == y.StudentID
                && x.Tuition == y.Tuition)
            return true;

        return false;
    }

    public int GetHashCode(Student obj)
    {
        return obj.StudentID.GetHashCode();
    }
}

public class Program
{
    public static void Main()
    {

        // Student collection
        IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "Frank Furter", Age = 55, Major="Hospitality", Tuition=3500.00} ,
                new Student() { StudentID = 1, StudentName = "Gina Host", Age = 21, Major="Hospitality", Tuition=4500.00 } ,
                new Student() { StudentID = 2, StudentName = "Cookie Crumb",  Age = 21, Major="CIT", Tuition=2500.00 } ,
                new Student() { StudentID = 3, StudentName = "Ima Script",  Age = 48, Major="CIT", Tuition=5500.00 } ,
                new Student() { StudentID = 3, StudentName = "Cora Coder",  Age = 35, Major="CIT", Tuition=1500.00 } ,
                new Student() { StudentID = 4, StudentName = "Ura Goodchild" , Age = 40, Major="Marketing", Tuition=500.00} ,
                new Student() { StudentID = 5, StudentName = "Take Mewith" , Age = 29, Major="Aerospace Engineering", Tuition=5500.00 }
        };
        // Student GPA Collection
        IList<StudentGPA> studentGPAList = new List<StudentGPA>() {
                new StudentGPA() { StudentID = 1,  GPA=4.0} ,
                new StudentGPA() { StudentID = 2,  GPA=3.5} ,
                new StudentGPA() { StudentID = 3,  GPA=2.0 } ,
                new StudentGPA() { StudentID = 4,  GPA=1.5 } ,
                new StudentGPA() { StudentID = 5,  GPA=4.0 } ,
                new StudentGPA() { StudentID = 6,  GPA=2.5} ,
                new StudentGPA() { StudentID = 7,  GPA=1.0 }
            };
        // Club collection
        IList<StudentClubs> studentClubList = new List<StudentClubs>() {
            new StudentClubs() {StudentID=1, ClubName="Photography" },
            new StudentClubs() {StudentID=1, ClubName="Game" },
            new StudentClubs() {StudentID=2, ClubName="Game" },
            new StudentClubs() {StudentID=5, ClubName="Photography" },
            new StudentClubs() {StudentID=6, ClubName="Game" },
            new StudentClubs() {StudentID=7, ClubName="Photography" },
            new StudentClubs() {StudentID=3, ClubName="PTK" },
        };

 //a) Group by GPA and display the student's IDs

        var groupedGPA = studentGPAList.GroupBy(s => s.GPA);

        foreach (var GPAGroup in groupedGPA)
        {
            Console.WriteLine("GPA Group: " + GPAGroup.Key);

            foreach (StudentGPA s in GPAGroup)
            {
                Console.WriteLine("Student ID: " + s.StudentID);
            }
        }
            Console.WriteLine();

 //b) Sort by Club, then group by Club and display the student's IDs

            var sortClub = studentClubList.OrderBy(o => o.ClubName).GroupBy(c => c.ClubName);

            foreach (var clubGroup in sortClub)
            {
                Console.WriteLine("Club Group: " + clubGroup.Key);

                foreach (StudentClubs c in clubGroup)
                {
                    Console.WriteLine("Student ID: " + c.StudentID);
                }
            }
                Console.WriteLine();

// c) Count the number of students with a GPA between 2.5 and 4.0
                var countGPA = studentGPAList.Where(g => g.GPA > 2.5).Where (g => g.GPA < 4.0);
                var countGPA1 = countGPA.Count();
                Console.WriteLine("Number of students with a GPA between 2.5 and 4.0: " + countGPA1);
        Console.WriteLine();

//d) Average all student's tuition
        var avgTuition = studentList.Average(s => s.Tuition);
        Console.WriteLine($"Average Tuition: " + avgTuition);
        Console.WriteLine();

//e) Find the student paying the most tuition and display their name, major and tuition. HINT: You will need to retrieve
//and store the highest tuition, then use a foreach loop to iterate through the studentList comparing each student's
// tuition to the highest. If they are equal, print out the data.

        var maxTuition = studentList.Max(s => s.Tuition);
        var distinctStudents = studentList.Distinct(new StudentComparerRow());
        foreach (var std in distinctStudents)
            Console.WriteLine("Name=" + std.StudentName + "  Major=" + std.Major + "   Tuition=" + maxTuition);
        Console.WriteLine();

//f) Join the student list and student GPA list on student ID and display the student's name, major and gpa

        var innerJoin = studentList.Join(studentGPAList,
                               student => student.StudentID,
                               gpa => gpa.StudentID,
                               (student, gpa) => new
                               {
                                   StudentName = student.StudentName,
                                   Major = student.Major,
                                   GPA = gpa.GPA
                               });


        Console.WriteLine("Student Info");
        Console.WriteLine();
        foreach (var s in innerJoin)
        {
            Console.WriteLine($"Name: {s.StudentName}   \t\tMajor: {s.Major}\t\tGPA: {s.GPA}");
            Console.WriteLine();
        }
        Console.WriteLine();

 //g) Join the student list and student club list. Display the names of only those students who are in the Game club.

        var listJoin = studentList.Join(studentClubList,
                                student => student.StudentID,
                                club => club.StudentID,
                                (student, club) => new
                                {
                                    StudentName = student.StudentName,
                                    ClubName = club.ClubName="Game"
                                                
                                });


        Console.WriteLine("Students who belong to the game club");
        Console.WriteLine();
        foreach (var s in listJoin)
        {
            Console.WriteLine($"Name: {s.StudentName}");
            Console.WriteLine();
        }
        Console.WriteLine();

    }
}
    

    
