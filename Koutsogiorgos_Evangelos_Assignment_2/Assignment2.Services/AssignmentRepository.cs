using System;
using System.Collections.Generic;
using System.Linq;
using Assignment2.Database;
using Assignment2.Entities;
using System.Data.Entity;


namespace Assignment2.Services
{
    public class AssignmentRepository
    {
        MyDatabase db = new MyDatabase();

        //============================ Get All =========================================
        public IEnumerable<Assignment> GetAll()
        {
            return db.Assignments.Include(x => x.Course).ToList();
        }

        //============================ Get GetById =========================================
        public Assignment GetById(int? id)
        {
            return db.Assignments.Include(x => x.Course).FirstOrDefault(x => x.AssignmentId == id);
        }

        //============================ Insert =========================================
        public void Insert(Assignment assignment)
        {
            db.Entry(assignment).State = EntityState.Added;
            db.SaveChanges();
        }

        //============================ Update =========================================
        public void Update(Assignment assignment)
        {
            db.Entry(assignment).State = EntityState.Modified;
            db.SaveChanges();
        }

        //============================ Delete =========================================
        public void Delete(Assignment assignment)
        {
            db.Entry(assignment).State = EntityState.Deleted;
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
