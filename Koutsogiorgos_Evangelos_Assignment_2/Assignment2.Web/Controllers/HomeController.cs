using System.Web.Mvc;

namespace Assignment2.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AboutCourses()
        {
            return View();
        }
    }
}