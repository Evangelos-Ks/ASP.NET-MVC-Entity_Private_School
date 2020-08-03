﻿using System.Linq;
using System.Web.Mvc;
using Assignment2.Services;
using Assignment2.Entities;
using System.Net;
using System.Security.Policy;
using PagedList;
using System.Collections.Generic;
using System;
using Assignment2.Web.Models;

namespace Assignment2.Web.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult AllCourses(string sort, string search, string currentFilter, int? page, int? pageSize)
        {
            CourseRepository courseRepository = new CourseRepository();
            var courses = courseRepository.GetAll();
            courseRepository.Dispose();


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
                courses = courses.Where(c => c.Title.ToUpper().Contains(search.ToUpper()) || c.Type.ToUpper().Contains(search.ToUpper()));
            }

            //============================================== sorting =======================================================
            ViewBag.CourseTitle = string.IsNullOrEmpty(sort) ? "courseTitleDesc" : "";
            ViewBag.Stream = sort == "streamAsc" ? "streamDesc" : "streamAsc";
            ViewBag.Type = sort == "typeAsc" ? "typeDesc" : "typeAsc";
            ViewBag.StartDate = sort == "startDateAsc" ? "startDateDesc" : "startDateAsc";
            ViewBag.EndDate = sort == "endDateAsc" ? "endDateDesc" : "endDateAsc";

            switch (sort)
            {
                case "courseTitleDesc":
                    courses = courses.OrderByDescending(x => x.Title);
                    break;
                case "streamAsc":
                    courses = courses.OrderBy(x => x.Stream);
                    break;
                case "streamDesc":
                    courses = courses.OrderByDescending(x => x.Stream);
                    break;
                case "typeAsc":
                    courses = courses.OrderBy(x => x.Type);
                    break;
                case "typeDesc":
                    courses = courses.OrderByDescending(x => x.Type);
                    break;
                case "startDateAsc":
                    courses = courses.OrderBy(x => x.StartDate);
                    break;
                case "startDateDesc":
                    courses = courses.OrderByDescending(x => x.StartDate);
                    break;
                case "endDateAsc":
                    courses = courses.OrderBy(x => x.EndDate);
                    break;
                case "endDateDesc":
                    courses = courses.OrderByDescending(x => x.EndDate);
                    break;
                default:
                    courses = courses.OrderBy(x => x.Title);
                    break;
            }

            ViewBag.CurrentSort = sort;
            ViewBag.CurrentFilter = search;
            ViewBag.CurrentPageSize = pSize;

            return View(courses.ToPagedList(pageNumber, pSize));
        }

        // GET: TestCourse/Details/5
        public ActionResult DetailsCourse(int? id)
        {
            CourseRepository courseRepository = new CourseRepository();
            Course course = courseRepository.GetById(id);
            courseRepository.Dispose();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (course == null)
            {
                return HttpNotFound();
            }

            return View(course);

        }

        // GET: TestCourse/Edit/5
        public ActionResult EditCourse(int? id)
        {
            CourseRepository courseRepository = new CourseRepository();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = courseRepository.GetById(id);
            courseRepository.Dispose();

            if (course == null)
            {
                return HttpNotFound();
            }

            return View(course);
        }

        // POST: TestCourse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourse([Bind(Include = "CourseId,Title,Stream,Type,StartDate,EndDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                CourseRepository courseRepository = new CourseRepository();
                courseRepository.Update(course);
                courseRepository.Dispose();
                return RedirectToAction("AllCourses");
            }
            return View(course);
        }

        // GET: TestCourse/Create
        public ActionResult CreateCourse()
        {
            StudentRepository studentRepository = new StudentRepository();
            var students = studentRepository.GetAll();
            studentRepository.Dispose();

            CourseViewModel curseViewModel = new CourseViewModel();
            curseViewModel.Students = students.Select(x =>
               new SelectListItem
               {
                   Value = x.StudentId.ToString(),
                   Text = string.Format(x.FirstName + " " + x.LastName)
               })
                .OrderBy(s => s.Text)
                .ToList();

            return View(curseViewModel);
        }

        // POST: TestCourse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourse([Bind(Include = "CourseId,Title,Stream,Type,StartDate,EndDate,Students")] CourseViewModel courseViewModel)
        {
            Course course = new Course()
            {
                CourseId = courseViewModel.CourseId,
                Title = courseViewModel.Title,
                Stream = courseViewModel.Stream,
                Type = courseViewModel.Type,
                StartDate = courseViewModel.StartDate,
                EndDate = courseViewModel.EndDate
            };

            if (ModelState.IsValid)
            {
                CourseRepository courseRepository = new CourseRepository();
                courseRepository.Insert(course);
                courseRepository.Dispose();
                return RedirectToAction("AllCourses");
            }

            return View(courseViewModel);
        }

        // GET: TestCourse/Delete/5
        public ActionResult DeleteCourse(int? id)
        {
            CourseRepository courseRepository = new CourseRepository();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = courseRepository.GetById(id);
            courseRepository.Dispose();
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: TestCourse/Delete/5
        [HttpPost, ActionName("DeleteCourse")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseRepository courseRepository = new CourseRepository();

            Course course = courseRepository.GetById(id);
            courseRepository.Delete(course);
            courseRepository.Dispose();
            return RedirectToAction("AllCourses");
        }
    }
}