using System;
using System.Collections.Generic;
using System.Linq;
using Assignment2.Database;
using Assignment2.Entities;
using System.Data.Entity;


namespace Assignment2.Services
{
    public class StudentAssignmentRepository
    {
        MyDatabase db = new MyDatabase();

        //============================ Get All =========================================
        public IEnumerable<StudentAssignment> GetAll()
        {
            return db.StudentsAssignments.ToList();
        }

        //============================ Get GetById =========================================
        public StudentAssignment GetById(int? id)
        {
            return db.StudentsAssignments.Find(id);
        }

        //============================ Insert =========================================
        public void Insert(StudentAssignment studentAssignment)
        {
            db.Entry(studentAssignment).State = EntityState.Added;
            db.SaveChanges();
        }

        //============================ Update =========================================
        public void Update(StudentAssignment studentAssignment)
        {
            db.Entry(studentAssignment).State = EntityState.Modified;
            db.SaveChanges();
        }

        //============================ Delete =========================================
        public void Delete(StudentAssignment studentAssignment)
        {
            db.Entry(studentAssignment).State = EntityState.Deleted;
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
