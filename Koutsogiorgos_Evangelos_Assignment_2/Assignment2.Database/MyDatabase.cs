using System.Data.Entity;
using Assignment2.Entities;

namespace Assignment2.Database
{
    public class MyDatabase : DbContext
    {
        public MyDatabase() : base("Connection") {}

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<StudentAssignment> StudentsAssignments { get; set; }
        public DbSet<StudentCourse> StudentsCourses { get; set; }
        public DbSet<TrainerCourse> TrainerCourses { get; set; }



    }
}
