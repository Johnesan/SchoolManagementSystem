namespace SchoolManagementSystem.Migrations
{
    using SchoolManagementSystem.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using SchoolManagementSystem.Data;

    internal sealed class Configuration : DbMigrationsConfiguration<SchoolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SchoolContext context)
        {
            var students = new List<Student>
            {
                new Student { FirstMidName = "John",   LastName = "Esan", 
                    EnrollmentDate = DateTime.Parse("2018-04-11") },
                new Student { FirstMidName = "Will", LastName = "Smith",    
                    EnrollmentDate = DateTime.Parse("2017-01-01") },
                new Student { FirstMidName = "Owon",   LastName = "Can",     
                    EnrollmentDate = DateTime.Parse("2016-09-13") },
                new Student { FirstMidName = "David",    LastName = "Morah", 
                    EnrollmentDate = DateTime.Parse("2018-09-08") },
                new Student { FirstMidName = "Lukaku",      LastName = "Tyrion",        
                    EnrollmentDate = DateTime.Parse("2017-03-16") },
                new Student { FirstMidName = "Henry",    LastName = "Ford",   
                    EnrollmentDate = DateTime.Parse("2017-03-03") },
                new Student { FirstMidName = "Abraham",    LastName = "Fish",    
                    EnrollmentDate = DateTime.Parse("2018-02-21") }
            };


            students.ForEach(s => context.Students.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var instructors = new List<Instructor>
            {
                new Instructor { FirstMidName = "Abu",     LastName = "Bakar", 
                    HireDate = DateTime.Parse("2008-07-15") },
                new Instructor { FirstMidName = "Jon",    LastName = "Bellion",    
                    HireDate = DateTime.Parse("2015-02-14") },
                new Instructor { FirstMidName = "Osas",   LastName = "Harry",       
                    HireDate = DateTime.Parse("2008-09-06") },
                new Instructor { FirstMidName = "Ruth", LastName = "Johnson",      
                    HireDate = DateTime.Parse("2012-11-23") },
                new Instructor { FirstMidName = "Dickson",   LastName = "Justice",      
                    HireDate = DateTime.Parse("2004-06-28") }
            };
            instructors.ForEach(s => context.Instructors.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var departments = new List<Department>
            {
                new Department { Name = "Electrical Engineering",     Budget = 350000, 
                    StartDate = DateTime.Parse("2007-09-01"), 
                    InstructorID  = instructors.Single( i => i.LastName == "Bakar").ID },
                new Department { Name = "Computer Science", Budget = 100000, 
                    StartDate = DateTime.Parse("2007-09-01"), 
                    InstructorID  = instructors.Single( i => i.LastName == "Harry").ID },
                new Department { Name = "Chemistry", Budget = 350000, 
                    StartDate = DateTime.Parse("2007-09-01"), 
                    InstructorID  = instructors.Single( i => i.LastName == "Justice").ID },
                new Department { Name = "Agriculture",   Budget = 100000, 
                    StartDate = DateTime.Parse("2007-09-01"), 
                    InstructorID  = instructors.Single( i => i.LastName == "Bellion").ID }
            };
            departments.ForEach(s => context.Departments.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course {CourseID = 112, Title = "Power",      Credits = 3,
                  DepartmentID = departments.Single( s => s.Name == "Electrical Engineering").DepartmentID,
                  Instructors = new List<Instructor>() 
                },
                new Course {CourseID = 101, Title = "Algorithms", Credits = 4,
                  DepartmentID = departments.Single( s => s.Name == "Computer Science").DepartmentID,
                  Instructors = new List<Instructor>() 
                },
                new Course {CourseID = 121, Title = "Data Processing", Credits = 3,
                  DepartmentID = departments.Single( s => s.Name == "Computer Science").DepartmentID,
                  Instructors = new List<Instructor>() 
                },
                new Course {CourseID = 103, Title = "Organic Chemistry",       Credits = 2,
                  DepartmentID = departments.Single( s => s.Name == "Chemistry").DepartmentID,
                  Instructors = new List<Instructor>() 
                },
                new Course {CourseID = 111, Title = "Fishery",   Credits = 4,
                  DepartmentID = departments.Single( s => s.Name == "Agriculture").DepartmentID,
                  Instructors = new List<Instructor>() 
                },
                new Course {CourseID = 131, Title = "Farming",    Credits = 3,
                  DepartmentID = departments.Single( s => s.Name == "Agriculture").DepartmentID,
                  Instructors = new List<Instructor>() 
                },
                new Course {CourseID = 122, Title = "Electronics",     Credits = 4,
                  DepartmentID = departments.Single( s => s.Name == "Electrical Engineering").DepartmentID,
                  Instructors = new List<Instructor>() 
                },
            };
            courses.ForEach(s => context.Courses.AddOrUpdate(p => p.CourseID, s));
            context.SaveChanges();

            var officeAssignments = new List<OfficeAssignment>
            {
                new OfficeAssignment { 
                    InstructorID = instructors.Single( i => i.LastName == "Bellion").ID, 
                    Location = "Location 1" },
                new OfficeAssignment { 
                    InstructorID = instructors.Single( i => i.LastName == "Harry").ID, 
                    Location = "Location 2" },
                new OfficeAssignment { 
                    InstructorID = instructors.Single( i => i.LastName == "Justice").ID, 
                    Location = "Location 3" },
            };
            officeAssignments.ForEach(s => context.OfficeAssignments.AddOrUpdate(p => p.InstructorID, s));
            context.SaveChanges();

            AddOrUpdateInstructor(context, "Power", "Bakar");
            AddOrUpdateInstructor(context, "Algorithm", "Bellion");
            AddOrUpdateInstructor(context, "Fishery", "Justice");
            AddOrUpdateInstructor(context, "Electronics", "Harry");
            AddOrUpdateInstructor(context, "Farming", "Bakar");

            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment { 
                    StudentID = students.Single(s => s.LastName == "Esan").ID, 
                    CourseID = courses.Single(c => c.Title == "Organic Chemistry" ).CourseID, 
                    Grade = Grade.D 
                },
                 new Enrollment { 
                    StudentID = students.Single(s => s.LastName == "Smith").ID,
                    CourseID = courses.Single(c => c.Title == "Algorithm" ).CourseID, 
                    Grade = Grade.F 
                 },                            
                 new Enrollment { 
                    StudentID = students.Single(s => s.LastName == "Can").ID,
                    CourseID = courses.Single(c => c.Title == "Data Processing" ).CourseID, 
                    Grade = Grade.A
                 },
                 new Enrollment { 
                     StudentID = students.Single(s => s.LastName == "Can").ID,
                    CourseID = courses.Single(c => c.Title == "Algorithm" ).CourseID, 
                    Grade = Grade.B 
                 },
                 new Enrollment { 
                     StudentID = students.Single(s => s.LastName == "Smith").ID,
                    CourseID = courses.Single(c => c.Title == "Power" ).CourseID, 
                    Grade = Grade.B 
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Morah").ID,
                    CourseID = courses.Single(c => c.Title == "Organic Chemistry" ).CourseID, 
                    Grade = Grade.B 
                 },
                 new Enrollment { 
                    StudentID = students.Single(s => s.LastName == "Tyrion").ID,
                    CourseID = courses.Single(c => c.Title == "Farming" ).CourseID
                 },
                 new Enrollment { 
                    StudentID = students.Single(s => s.LastName == "Ford").ID,
                    CourseID = courses.Single(c => c.Title == "Electronics").CourseID,
                    Grade = Grade.C         
                 },
                new Enrollment { 
                    StudentID = students.Single(s => s.LastName == "Fish").ID,
                    CourseID = courses.Single(c => c.Title == "Farming").CourseID,
                    Grade = Grade.D         
                 },
                 new Enrollment { 
                    StudentID = students.Single(s => s.LastName == "Fish").ID,
                    CourseID = courses.Single(c => c.Title == "Fishery").CourseID,
                    Grade = Grade.A         
                 },
                 new Enrollment { 
                    StudentID = students.Single(s => s.LastName == "Morah").ID,
                    CourseID = courses.Single(c => c.Title == "Power").CourseID,
                    Grade = Grade.B         
                 }
            };

            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments.Where(
                    s =>
                         s.Student.ID == e.StudentID &&
                         s.Course.CourseID == e.CourseID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
            }
            context.SaveChanges();
        }

        void AddOrUpdateInstructor(SchoolContext context, string courseTitle, string instructorName)
        {
            var crs = context.Courses.SingleOrDefault(c => c.Title == courseTitle);
            var inst = crs.Instructors.SingleOrDefault(i => i.LastName == instructorName);
            if (inst == null)
                crs.Instructors.Add(context.Instructors.Single(i => i.LastName == instructorName));
        }
    }
}
