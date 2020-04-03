using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Assignment2.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;

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

       
    }
}
