using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniApp.Models;

namespace UniApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {

            context.Database.EnsureCreated();

            if (context.Students.Any())
            {
                return;
            }

            var students = new Student[]
            {
                new Student {FirstName = "Carson", LastName = "Alexander", EnrollmentDate = DateTime.Parse("2010-09-01")},
                new Student {FirstName = "Meredith", LastName = "Alosno", EnrollmentDate = DateTime.Parse("2012-09-01")},
                new Student {FirstName = "Arturo", LastName = "Anand", EnrollmentDate = DateTime.Parse("2013-09-01")},
                new Student {FirstName = "Gytis", LastName = "Barzdukas", EnrollmentDate = DateTime.Parse("2012-09-01")},
                new Student {FirstName = "Yan", LastName = "Li", EnrollmentDate = DateTime.Parse("2012-09-01")},
                new Student {FirstName = "Peggy", LastName = "Justice", EnrollmentDate = DateTime.Parse("2011-09-01")},
                new Student {FirstName = "Laura", LastName = "Norman", EnrollmentDate = DateTime.Parse("2013-09-01")},
                new Student {FirstName = "Nino", LastName = "Olivetto", EnrollmentDate = DateTime.Parse("2005-09-01")}
            };

            foreach (Student s in students)
            {
                context.Students.Add(s);
            }

            context.SaveChanges();

            var instructors = new Instructor[]
            {
                new Instructor{ FirstName = "Kim", LastName = "Abercrombie", HireDate = DateTime.Parse("1995-03-11")},
                new Instructor{ FirstName = "Fadi", LastName = "Fakhouri", HireDate = DateTime.Parse("2020-07-06")},
                new Instructor{ FirstName = "Roger", LastName = "Harui", HireDate = DateTime.Parse("1998-07-01")},
                new Instructor{ FirstName = "Candace", LastName = "Kapoor", HireDate = DateTime.Parse("2001-01-15")},
                new Instructor{ FirstName = "Roger", LastName = "Zheng", HireDate = DateTime.Parse("2004-02-12")}
            };

            foreach (Instructor i in instructors)
            {
                context.Instructors.Add(i);
            }

            context.SaveChanges();

            var departments = new Department[]
            {
                new Department{ Name = "English", Budget = 350000, StartDate = DateTime.Parse("2007-09-01"), 
                    InstructorID = instructors.Single( i => i.LastName == "Abercrombie").ID },
                new Department{ Name = "Methametics", Budget = 100000, StartDate = DateTime.Parse("2007-09-01"), 
                    InstructorID = instructors.Single( i => i.LastName == "Fakhouri").ID },
                new Department{ Name = "Engineering", Budget = 350000, StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID = instructors.Single( i => i.LastName == "Harui").ID },
                new Department{ Name = "Economics", Budget = 100000, StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID = instructors.Single( i => i.LastName == "Kapoor").ID }
             };

            foreach (Department d in departments)
            {
                context.Departments.Add(d);
            }

            context.SaveChanges();

            var courses = new Course[]
            {
                new Course{ CourseID = 1050, Title = "Chemistry", Credits = 3, 
                    DepartmentID = departments.Single( d => d.Name =="Engineering").DepartmentID },
                new Course{ CourseID = 4022, Title = "Microeconomics", Credits = 3, 
                    DepartmentID = departments.Single( d => d.Name =="Economics").DepartmentID },
                new Course{ CourseID = 4041, Title = "Macroeconomics", Credits = 3, 
                    DepartmentID = departments.Single( d => d.Name =="Economics").DepartmentID },
                new Course{ CourseID = 1045, Title = "Calculas", Credits = 4, 
                    DepartmentID = departments.Single( d => d.Name =="Methametics").DepartmentID },
                new Course{ CourseID = 3141, Title = "Trigonometry", Credits = 4, 
                    DepartmentID = departments.Single( s => s.Name == "Methametics").DepartmentID},
                new Course{ CourseID = 2021, Title = "Composition", Credits = 3, 
                    DepartmentID = departments.Single( s => s.Name == "English").DepartmentID},
                new Course{ CourseID = 2042, Title = "Literature", Credits = 4, 
                    DepartmentID = departments.Single( s => s.Name == "English").DepartmentID}
            };

            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            
            context.SaveChanges();

            var officeAssignments = new OfficeAssignment[]
            {
                new OfficeAssignment{ InstructorID = instructors.Single(i => i.LastName == "Fakhouri").ID,  Location ="Smith 17"},
                new OfficeAssignment{ InstructorID = instructors.Single(i => i.LastName == "Hauri").ID, Location ="Gowan 27"},
                new OfficeAssignment{ InstructorID = instructors.Single(i => i.LastName == "Kapoor").ID, Location ="Thompson 304"},
            };

            foreach (OfficeAssignment o in officeAssignments)
            {
                context.OfficeAssignments.Add(o);
            }

            context.SaveChanges();

            var courseInstructors = new CourseAssignment[]
            {
                new CourseAssignment{ CourseID = courses.Single( c => c.Title == "Chemistry" ).CourseID,
                    InstructorID = instructors.Single( i => i.LastName == "Kapoor").ID },

                new CourseAssignment{ CourseID = courses.Single( c => c.Title == "Chemistry" ).CourseID,
                    InstructorID = instructors.Single( i => i.LastName == "Harui").ID }, 

                new CourseAssignment{ CourseID = courses.Single( c => c.Title == "Microeconomics" ).CourseID,
                    InstructorID = instructors.Single( i => i.LastName == "Zheng").ID },

                new CourseAssignment{ CourseID = courses.Single( c => c.Title == "Macroeconomics" ).CourseID,
                    InstructorID = instructors.Single( i => i.LastName == "Zheng").ID },

                new CourseAssignment{ CourseID = courses.Single( c => c.Title == "Calculus" ).CourseID,
                    InstructorID = instructors.Single( i => i.LastName == "Fakhouri").ID },
                
                new CourseAssignment{ CourseID = courses.Single( c => c.Title == "Trigonometry" ).CourseID,
                    InstructorID = instructors.Single( i => i.LastName == "Harui").ID }, 
                
                new CourseAssignment{ CourseID = courses.Single( c => c.Title == "Composition" ).CourseID,
                    InstructorID = instructors.Single( i => i.LastName == "Harui").ID },

                new CourseAssignment{ CourseID = courses.Single( c => c.Title == "Composition").CourseID,
                    InstructorID = instructors.Single( i => i.LastName == "Abercrombie").ID},

                new CourseAssignment{ CourseID = courses.Single( c => c.Title == "Literature").CourseID,
                    InstructorID = instructors.Single( i => i.LastName == "Abercrombie").ID},
            };

            foreach (CourseAssignment ci in courseInstructors)
            {
                context.CourseAssignments.Add(ci);
            }

            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
                new Enrollment{ StudentID = students.Single( s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry").CourseID, Grade = Grade.A},

                new Enrollment { StudentID = students.Single( s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single( c => c.Title == "Microeconomics").CourseID, Grade = Grade.C
                },

                new Enrollment{ StudentID = students.Single( s => s.LastName == "Alexander").ID, 
                    CourseID = courses.Single( c => c.Title == "Macroeconomics").CourseID, Grade = Grade.B
                },
                
                new Enrollment{ StudentID = students.Single( s => s.LastName == "Alosno").ID, 
                    CourseID = courses.Single( c => c.Title == "Calculas").CourseID, Grade = Grade.B
                },

                new Enrollment{ StudentID = students.Single( s => s.LastName == "Alosno").ID, 
                    CourseID = courses.Single(c => c.Title == "Trigonometry").CourseID, Grade = Grade.B
                },

                new Enrollment{ StudentID = students.Single( s => s.LastName == "Alosno").ID, 
                    CourseID = courses.Single( c => c.Title == "Composition").CourseID, Grade = Grade.B
                },

                new Enrollment { StudentID = students.Single( s => s.LastName == "Anand").ID,
                    CourseID = courses.Single( c => c.Title == "Chemistry").CourseID, Grade = Grade.B
                },

                new Enrollment { StudentID = students.Single( s => s.LastName == "Li").ID,
                    CourseID = courses.Single( c => c.Title == "Composition").CourseID, Grade = Grade.B
                },

                new Enrollment { StudentID = students.Single ( s => s.LastName =="Justice").ID,
                    CourseID = courses.Single ( s => s.Title == "Literature").CourseID, Grade = Grade.B
                }
            };

            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDatabase = context.Enrollments.Where(
                        s =>
                            s.Student.ID == e.StudentID && 
                            s.Course.CourseID == e.CourseID).SingleOrDefault();

                if (enrollmentInDatabase == null)
                {
                    context.Enrollments.Add(e);
                }
            }

            context.SaveChanges();


        }
    }
}
