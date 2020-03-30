using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Assignment2.Entities;

namespace Assignment2.Database
{
    class MyDatabase : DbContext
    {
        public MyDatabase() : base("Connection") {}

        //public DbSet<Student> Students { get; set; }
        //public DbSet<Course> Courses { get; set; }
        //public DbSet<Trainer> Trainers { get; set; }
        //public DbSet<Assignment> Assignments { get; set; }
        //public DbSet<CourseTrainer> CourseTrainers { get; set; }
        //public DbSet<StudentAssignment> StudentAssignments { get; set; }
        //public DbSet<StudentCourse> studentCourses { get; set; }
    }
}
