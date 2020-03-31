namespace Assignment2.Database.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Assignment2.Entities;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<Assignment2.Database.MyDatabase>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Assignment2.Database.MyDatabase context)
        {
            //==================== Seeding Courses ===========================================
            Course c1 = new Course() { Title = "Phisics", Stream = "Full time", Type = "Science" , StartDate = DateTime.Parse("2019/10/1"), EndDate = DateTime.Parse("2020/6/16") };
            Course c2 = new Course() { Title = "Chemistry", Stream = "Full time", Type = "Science" , StartDate = DateTime.Parse("2019/10/1"), EndDate = DateTime.Parse("2020/6/11") };
            Course c3 = new Course() { Title = "Biology", Stream = "Full time", Type = "Science" , StartDate = DateTime.Parse("2019/10/1"), EndDate = DateTime.Parse("2020/7/1") };
            Course c4 = new Course() { Title = "Mathematics", Stream = "Full time", Type = "Science" , StartDate = DateTime.Parse("2019/10/1"), EndDate = DateTime.Parse("2020/5/30") };
            Course c5 = new Course() { Title = "Information Technology", Stream = "Full time", Type = "Computer science" , StartDate = DateTime.Parse("2019/10/1"), EndDate = DateTime.Parse("2020/6/2") };


            //==================== Seeding Trainers ===========================================
            Trainer t1 = new Trainer() { FirstName = "Albert" };
            Trainer t2 = new Trainer() { FirstName = "Richard" };

            //==================== Seeding Assignments ===========================================
            Assignment a1 = new Assignment() { Title = "MecPhys" };
            Assignment a2 = new Assignment() { Title = "OptPhys" };
            Assignment a3 = new Assignment() { Title = "Phys3" };

            //==================== Seeding Students ===========================================
            Student s1 = new Student() { FirstName= "Maria"};
            Student s2 = new Student() { FirstName = "Evangelos" };
            Student s3 = new Student() { FirstName = "Panagiotis" };
            Student s4 = new Student() { FirstName = "Ioannis" };
            Student s5 = new Student() { FirstName = "Sophia" };
            Student s6 = new Student() { FirstName = "Eleni" };

            //==================== Seeding StudentAssignment ===========================================
            StudentAssignment sa1 = new StudentAssignment() { Assignment = a1, Student = s1 };
            StudentAssignment sa2 = new StudentAssignment() { Assignment = a1, Student = s2 };
            StudentAssignment sa3 = new StudentAssignment() { Assignment = a2, Student = s3 };
            StudentAssignment sa4 = new StudentAssignment() { Assignment = a2, Student = s4 };
            StudentAssignment sa5 = new StudentAssignment() { Assignment = a3, Student = s5 };
            StudentAssignment sa6 = new StudentAssignment() { Assignment = a3, Student = s6 };

            //==================== Seeding StudentCourse ===========================================
            StudentCourse sc1 = new StudentCourse() { Course = c1, Student = s1 };
            StudentCourse sc2 = new StudentCourse() { Course = c1, Student = s2 };
            StudentCourse sc3 = new StudentCourse() { Course = c1, Student = s3 };
            StudentCourse sc4 = new StudentCourse() { Course = c1, Student = s4 };
            StudentCourse sc5 = new StudentCourse() { Course = c1, Student = s5 };
            StudentCourse sc6 = new StudentCourse() { Course = c1, Student = s6 };

            //=================== Asign Assignments and Trainers to Courses ==========================
            c1.Assignments = new List<Assignment>() { a1, a2, a3 };
            c1.Trainers = new List<Trainer>() { t1, t2 };

            //=================== Asign Courses to Trainers =========================================
            t1.Courses = new List<Course>() { c1 };
            t2.Courses = new List<Course>() { c1 };

            //=================== Insert data to the database ========================================
            context.Assignments.AddOrUpdate(x => x.Title, a1, a2, a3);
            context.Courses.AddOrUpdate(x => x.Title, c1, c2, c3, c4, c5);
            context.Students.AddOrUpdate(x => x.FirstName, s1, s2, s3, s4, s5, s6);
            context.Trainers.AddOrUpdate(x => x.FirstName, t1, t2);
            context.StudentsAssignments.AddOrUpdate(sa1, sa2, sa3, sa4, sa5, sa6);
            context.StudentsCourses.AddOrUpdate(x => new { x.CourseId, x.StudentId }, sc1, sc2, sc3, sc4, sc5, sc6);

            context.SaveChanges();
        }
    }
}
