﻿using System;
using System.Collections.Generic;
using System.Linq;
using Assignment2.Database;
using Assignment2.Entities;
using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;

namespace Assignment2.Services
{
    public class TrainerRepository
    {
        MyDatabase db = new MyDatabase();

        //============================ Get All =========================================
        public IEnumerable<Trainer> GetAll()
        {
            return db.Trainers.ToList();
        }

        //============================ Get GetById =========================================
        public Trainer GetById(int? id)
        {
            return db.Trainers.Include(x => x.TrainerCourses.Select(course => course.Course)).FirstOrDefault(x => x.TrainerId == id);
        }

        //============================ Insert =========================================
        public void Insert(Trainer trainer)
        {
            db.Entry(trainer).State = EntityState.Added;
            db.SaveChanges();
        }

        //============================ Update =========================================
        public void Update(Trainer trainer)
        {
            db.Entry(trainer).State = EntityState.Modified;
            db.SaveChanges();
        }

        //============================ Delete =========================================
        public void Delete(Trainer trainer)
        {
            db.Entry(trainer).State = EntityState.Deleted;
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
