using System.Linq;
using System.Net;
using System.Web.Mvc;
using Assignment2.Services;
using Assignment2.Entities;


namespace Assignment2.Web.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult AllStudents(string sort)
        {
            StudentRepository studentRepository = new StudentRepository();
            var students = studentRepository.GetAll();
            studentRepository.Dispose();

            ViewBag.FirstName = string.IsNullOrEmpty(sort) ? "firstNameDesc" : "";
            ViewBag.LastName = sort == "lastNameAsc" ? "lastNameDesc" : "lastNameAsc";

            switch (sort)
            {
                case "firstNameDesc":
                    students = students.OrderByDescending(x => x.FirstName);
                    break;
                case "lastNameAsc":
                    students = students.OrderBy(x => x.LastName);
                    break;
                case "lastNameDesc":
                    students = students.OrderByDescending(x => x.LastName);
                    break;
                default:
                    students = students.OrderBy(x => x.FirstName);
                    break;
            }

            return View(students);
        }

        // GET: TestStudent/Details/5
        public ActionResult DetailsStudent(int? id)
        {
            StudentRepository studentRepository = new StudentRepository();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = studentRepository.GetById(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            //studentRepository.Dispose();

            return View(student);

        }

        // GET: TestStudent/Edit/5
        public ActionResult EditStudent(int? id)
        {
            StudentRepository studentRepository = new StudentRepository();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = studentRepository.GetById(id);
            studentRepository.Dispose();

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        // POST: TestStudent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStudent([Bind(Include = "StudentId,FirstName,LastName,DateOfBirth,PhotoUrl")] Student student)
        {
            if (ModelState.IsValid)
            {
                StudentRepository studentRepository = new StudentRepository();
                studentRepository.Update(student);
                studentRepository.Dispose();
                return RedirectToAction("AllStudents");
            }
            return View(student);
        }

        // GET: TestStudent/Create
        public ActionResult CreateStudent()
        {
            return View();
        }

        // POST: TestStudent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStudent([Bind(Include = "StudentId,FirstName,LastName,DateOfBirth,PhotoUrl")] Student student)
        {
            if (ModelState.IsValid)
            {
                StudentRepository studentRepository = new StudentRepository();
                studentRepository.Insert(student);
                studentRepository.Dispose();
                return RedirectToAction("AllStudents");
            }

            return View(student);
        }

        // GET: TestStudent/Delete/5
        public ActionResult DeleteStudent(int? id)
        {
            StudentRepository studentRepository = new StudentRepository();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = studentRepository.GetById(id);
            studentRepository.Dispose();
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: TestStudent/Delete/5
        [HttpPost, ActionName("DeleteStudent")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentRepository studentRepository = new StudentRepository();

            Student student = studentRepository.GetById(id);
            studentRepository.Delete(student);
            studentRepository.Dispose();
            return RedirectToAction("AllStudents");
        }
    }
}