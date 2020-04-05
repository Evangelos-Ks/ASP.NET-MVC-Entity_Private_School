using System;
using System.Collections.Generic;
using System.Linq;
using Assignment2.Database;
using Assignment2.Entities;
using System.Data.Entity;


namespace Assignment2.Services
{
    public class StudentCourseRepository
    {
        MyDatabase db = new MyDatabase();

        //============================ Get All =========================================
        public IEnumerable<StudentCourse> GetAll()
        {
            //return db.StudentsCourses;
            return db.StudentsCourses.Include(x => x.Course).Include(y => y.Student).ToList();
        }

        //============================ Get GetById =========================================
        public StudentCourse GetById(int? id)
        {
            return db.StudentsCourses.Find(id);
        }

        //============================ Insert =========================================
        public void Insert(StudentCourse studentCourse)
        {
            db.Entry(studentCourse).State = EntityState.Added;
            db.SaveChanges();
        }

        //============================ Update =========================================
        public void Update(StudentCourse studentCourse)
        {
            db.Entry(studentCourse).State = EntityState.Modified;
            db.SaveChanges();
        }

        //============================ Delete =========================================
        public void Delete(StudentCourse studentCourse)
        {
            db.Entry(studentCourse).State = EntityState.Deleted;
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
