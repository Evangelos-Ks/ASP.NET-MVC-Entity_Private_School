using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment2.Services;
using Assignment2.Entities;

namespace Assignment2.Web.Controllers
{
    public class TrainerController : Controller
    {
        // GET: Trainer
        public ActionResult AllTrainers()
        {
            TrainerRepository trainerRepository = new TrainerRepository();
            var trainers = trainerRepository.GetAll();
            trainerRepository.Dispose();

            trainers = trainers.OrderBy(x => x.FirstName);

            return View(trainers);
        }

        // GET: TestTrainer/Details/5
        public ActionResult DetailsTrainer(int? id)
        {
            TrainerRepository trainerRepository = new TrainerRepository();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = trainerRepository.GetById(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            //TrainerRepository.Dispose();

            return View(trainer);

        }

        // GET: TestTrainer/Edit/5
        public ActionResult EditTrainer(int? id)
        {
            TrainerRepository trainerRepository = new TrainerRepository();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = trainerRepository.GetById(id);
            trainerRepository.Dispose();

            if (trainer == null)
            {
                return HttpNotFound();
            }

            return View(trainer);
        }

        // POST: TestTrainer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTrainer([Bind(Include = "TrainerId,FirstName,LastName,DateOfBirth,PhotoUrl")] Trainer trainer)
        {
            if (ModelState.IsValid)
            {
                TrainerRepository trainerRepository = new TrainerRepository();
                trainerRepository.Update(trainer);
                trainerRepository.Dispose();
                return RedirectToAction("AllTrainers");
            }
            return View(trainer);
        }

        // GET: TestTrainer/Create
        public ActionResult CreateTrainer()
        {
            return View();
        }

        // POST: TestTrainer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTrainer([Bind(Include = "TrainerId,FirstName,LastName,DateOfBirth,PhotoUrl")] Trainer trainer)
        {
            if (ModelState.IsValid)
            {
                TrainerRepository trainerRepository = new TrainerRepository();
                trainerRepository.Insert(trainer);
                trainerRepository.Dispose();
                return RedirectToAction("AllTrainers");
            }

            return View(trainer);
        }

        // GET: TestTrainer/Delete/5
        public ActionResult DeleteTrainer(int? id)
        {
            TrainerRepository trainerRepository = new TrainerRepository();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = trainerRepository.GetById(id);
            trainerRepository.Dispose();
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // POST: TestTrainer/Delete/5
        [HttpPost, ActionName("DeleteTrainer")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrainerRepository trainerRepository = new TrainerRepository();

            Trainer trainer = trainerRepository.GetById(id);
            trainerRepository.Delete(trainer);
            trainerRepository.Dispose();
            return RedirectToAction("AllTrainers");
        }
    }

}