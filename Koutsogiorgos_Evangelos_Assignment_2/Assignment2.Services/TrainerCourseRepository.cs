using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Assignment2.Database;
using Assignment2.Entities;

namespace Assignment2.Services
{
    public class TrainerCourseRepository
    {
        MyDatabase db = new MyDatabase();

        //============================ Get All =========================================
        public IEnumerable<TrainerCourse> GetAll()
        {
            return db.TrainerCourses.Include(x => x.Course).Include(y => y.Trainer).ToList();
        }

        //============================ Get GetById =========================================
        public TrainerCourse GetById(int? id)
        {
            return db.TrainerCourses.Find(id);
        }

        //============================ Insert =========================================
        public void Insert(TrainerCourse trainerCourse)
        {
            db.Entry(trainerCourse).State = EntityState.Added;
            db.SaveChanges();
        }

        //============================ Update =========================================
        public void Update(TrainerCourse trainerCourse)
        {
            db.Entry(trainerCourse).State = EntityState.Modified;
            db.SaveChanges();
        }

        //============================ Delete =========================================
        public void Delete(TrainerCourse trainerCourse)
        {
            db.Entry(trainerCourse).State = EntityState.Deleted;
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
