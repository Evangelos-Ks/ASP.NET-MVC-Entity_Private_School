using System.Linq;
using System.Net;
using System.Web.Mvc;
using Assignment2.Services;
using Assignment2.Entities;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using PagedList;

namespace Assignment2.Web.Controllers
{
    public class TrainerController : Controller
    {
        // GET: Trainer
        public ActionResult AllTrainers(string sort, string search, string currentFilter, int? page, int? pageSize)
        {
            TrainerRepository trainerRepository = new TrainerRepository();
            var trainers = trainerRepository.GetAll();
            trainerRepository.Dispose();

            //============================================== Paging ========================================================
            if (!string.IsNullOrWhiteSpace(search) || search == "")
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }
            int pSize = pageSize ?? 3;
            int pageNumber = page ?? 1;

            ViewBag.PageSize = new List<SelectListItem>()
            {
             new SelectListItem() { Value="3", Text= "3" },
             new SelectListItem() { Value="5", Text= "5" },
             new SelectListItem() { Value="10", Text= "10" },
             new SelectListItem() { Value="15", Text= "15" },
             new SelectListItem() { Value="25", Text= "25" },
             new SelectListItem() { Value="50", Text= "50" }
            };

            //============================================== searching =====================================================
            if (!string.IsNullOrEmpty(search))
            {
                trainers = trainers.Where(x => x.FirstName.ToUpper().Contains(search.ToUpper()) ||
                x.LastName.ToUpper().Contains(search.ToUpper()) ||
                x.Subject.ToUpper().Contains(search.ToUpper()));
            }

            //============================================== sorting =======================================================
            ViewBag.FirstName = string.IsNullOrEmpty(sort) ? "firstNameDesc" : "";
            ViewBag.LastName = sort == "lastNameAsc" ? "lastNameDesc" : "lastNameAsc";
            ViewBag.Subject = sort == "subjectAsc" ? "subjectDesc" : "subjectAsc";


            switch (sort)
            {
                case "firstNameDesc":
                    trainers = trainers.OrderByDescending(x => x.FirstName);
                    break;
                case "lastNameAsc":
                    trainers = trainers.OrderBy(x => x.LastName);
                    break;
                case "lastNameDesc":
                    trainers = trainers.OrderByDescending(x => x.LastName);
                    break;
                case "subjectAsc":
                    trainers = trainers.OrderBy(x => x.Subject);
                    break;
                case "subjectDesc":
                    trainers = trainers.OrderByDescending(x => x.Subject);
                    break;
                default:
                    trainers = trainers.OrderBy(x => x.FirstName);
                    break;
            }

            ViewBag.CurrentFilter = search;
            ViewBag.CurrentSort = sort;
            ViewBag.CurrentPageSize = pSize;

            return View(trainers.ToPagedList(pageNumber, pSize));
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
            trainerRepository.Dispose();

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
        public ActionResult EditTrainer([Bind(Include = "TrainerId,FirstName,LastName,Subject,PhotoUrl")] Trainer trainer)
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
        public ActionResult CreateTrainer([Bind(Include = "TrainerId,FirstName,LastName,Subject,PhotoUrl")] Trainer trainer)
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