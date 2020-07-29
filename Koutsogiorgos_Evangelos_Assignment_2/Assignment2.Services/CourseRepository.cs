using System;
using System.Collections.Generic;
using System.Linq;
using Assignment2.Database;
using Assignment2.Entities;
using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using System.Security.Policy;

namespace Assignment2.Services
{
    public class CourseRepository
    {
        MyDatabase db = new MyDatabase();

        //============================ Get All =========================================
        public IEnumerable<Course> GetAll()
        {
            return db.Courses.Include(x => x.Assignments).Include(y => y.StudentCourses.Select(k => k.Student)).ToList();
        }

        //============================ Get GetById =========================================
        public Course GetById(int? id)
        {
            return db.Courses.Include(x => x.Assignments).Include(y => y.StudentCourses.Select(k => k.Student)).FirstOrDefault(z => z.CourseId == id);
        }

        //============================ Insert =========================================
        public void Insert(Course course)
        {
            db.Entry(course).State = EntityState.Added;
            db.SaveChanges();
        }

        //============================ Update =========================================
        public void Update(Course course)
        {
            db.Entry(course).State = EntityState.Modified;
            db.SaveChanges();
        }

        //============================ Delete =========================================
        public void Delete(Course course)
        {
            db.Entry(course).State = EntityState.Deleted;
            db.SaveChanges();
        }

        //============================ Dispose =========================================
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
