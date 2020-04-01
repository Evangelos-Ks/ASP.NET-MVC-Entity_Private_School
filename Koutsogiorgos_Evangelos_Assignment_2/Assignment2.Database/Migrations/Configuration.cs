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
            Trainer t1 = new Trainer() { FirstName = "Albert", LastName = "Einstain", Subject = "Phisics"};
            Trainer t2 = new Trainer() { FirstName = "Richard", LastName = "Faynman", Subject = "Phisics" };
            Trainer t3 = new Trainer() { FirstName = "Marie", LastName = "Curie", Subject = "Chemistry" };
            Trainer t4 = new Trainer() { FirstName = "Luis", LastName = "Pasteur", Subject = "Chemistry" };
            Trainer t5 = new Trainer() { FirstName = "Francis", LastName = "Crick", Subject = "Biology" };
            Trainer t6 = new Trainer() { FirstName = "James", LastName = "Watson", Subject = "Biology" };
            Trainer t7 = new Trainer() { FirstName = "Leonhard", LastName = "Euler", Subject = "Mathematics" };
            Trainer t8 = new Trainer() { FirstName = "Carl", LastName = "Gauss", Subject = "Mathematics" };
            Trainer t9 = new Trainer() { FirstName = "Alan", LastName = "Turing", Subject = "Computer science" };
            Trainer t10 = new Trainer() { FirstName = "Dennis", LastName = "Ritchie", Subject = "Computer science" };


            //==================== Seeding Assignments ===========================================
            Assignment a1 = new Assignment() { Title = "PhysMec", Description = "Mechanics", SubDateTime= DateTime.Parse("2020/2/10") };
            Assignment a2 = new Assignment() { Title = "PhysOpt", Description = "Optics", SubDateTime = DateTime.Parse("2020/3/20") };
            Assignment a3 = new Assignment() { Title = "PhysWav", Description = "Waves", SubDateTime = DateTime.Parse("2020/4/15") };

            Assignment a4 = new Assignment() { Title = "ChemInorg1", Description = "Inorganic Chemistry I", SubDateTime = DateTime.Parse("2020/2/15") };
            Assignment a5 = new Assignment() { Title = "ChemInorg2", Description = "Inorganic Chemistry II", SubDateTime = DateTime.Parse("2020/3/25") };
            Assignment a6 = new Assignment() { Title = "ChemOrg", Description = "Organic Chemistry I", SubDateTime = DateTime.Parse("2020/4/15") };

            Assignment a7 = new Assignment() { Title = "BioC", Description = "Cell Theory", SubDateTime = DateTime.Parse("2020/3/15") };
            Assignment a8 = new Assignment() { Title = "BioEv", Description = "Evolution", SubDateTime = DateTime.Parse("2020/4/10") };
            Assignment a9 = new Assignment() { Title = "BioGen", Description = "Gene theory", SubDateTime = DateTime.Parse("2020/5/30") };

            Assignment a10 = new Assignment() { Title = "MathAl", Description = "Algebra", SubDateTime = DateTime.Parse("2020/3/12") };
            Assignment a11 = new Assignment() { Title = "MathCal", Description = "Calculus", SubDateTime = DateTime.Parse("2020/4/25") };
            Assignment a12 = new Assignment() { Title = "MathGT", Description = "Geometry and topology", SubDateTime = DateTime.Parse("2020/5/20") };

            Assignment a13 = new Assignment() { Title = "ITAl", Description = "Algorithms", SubDateTime = DateTime.Parse("2020/2/20") };
            Assignment a14 = new Assignment() { Title = "ITCD", Description = "Computer design", SubDateTime = DateTime.Parse("2020/3/26") };
            Assignment a15 = new Assignment() { Title = "ITPM", Description = "Programming methodology", SubDateTime = DateTime.Parse("2020/5/25") };


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
            c2.Assignments = new List<Assignment>() { a4, a5, a6 };
            c3.Assignments = new List<Assignment>() { a7, a8, a9 };
            c4.Assignments = new List<Assignment>() { a10, a11, a12 };
            c5.Assignments = new List<Assignment>() { a13, a14, a15 };

            c1.Trainers = new List<Trainer>() { t1, t2 };
            c2.Trainers = new List<Trainer>() { t3, t4 };
            c3.Trainers = new List<Trainer>() { t5, t6 };
            c4.Trainers = new List<Trainer>() { t7, t8 };
            c5.Trainers = new List<Trainer>() { t9, t10 };
            

            //=================== Asign Courses to Trainers =========================================
            t1.Courses = new List<Course>() { c1 };
            t2.Courses = new List<Course>() { c1 };
            t3.Courses = new List<Course>() { c2 };
            t4.Courses = new List<Course>() { c2 };
            t5.Courses = new List<Course>() { c3 };
            t6.Courses = new List<Course>() { c3 };
            t7.Courses = new List<Course>() { c4 };
            t8.Courses = new List<Course>() { c4 };
            t9.Courses = new List<Course>() { c5 };
            t10.Courses = new List<Course>() { c5 };


            //=================== Insert data to the database ========================================
            context.Assignments.AddOrUpdate(x => x.Title, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15);
            context.Courses.AddOrUpdate(x => x.Title, c1, c2, c3, c4, c5);
            context.Students.AddOrUpdate(x => x.FirstName, s1, s2, s3, s4, s5, s6);
            context.Trainers.AddOrUpdate(x => new { x.FirstName, x.LastName }, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10);
            context.StudentsAssignments.AddOrUpdate(sa1, sa2, sa3, sa4, sa5, sa6);
            context.StudentsCourses.AddOrUpdate(x => new { x.CourseId, x.StudentId }, sc1, sc2, sc3, sc4, sc5, sc6);

            context.SaveChanges();
        }
    }
}
