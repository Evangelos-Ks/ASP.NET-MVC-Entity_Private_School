using Assignment2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment2.Web.Models
{
    public class Methods
    {
        //Create Select list methods
        public static IEnumerable<SelectListItem> CreateSelectListOfStudents(IEnumerable<Student> students)
        {
            var selectlist = students.Select(s =>
                                new SelectListItem()
                                {
                                    Value = s.StudentId.ToString(),
                                    Text = string.Format(s.FirstName + " " + s.LastName)
                                })
                                 .OrderBy(s => s.Text)
                                 .ToList();

            return selectlist;
        }

        public static IEnumerable<SelectListItem> CreateSelectListOfTrainers(IEnumerable<Trainer> trainers)
        {
            var selectlist = trainers.Select(t =>
                                new SelectListItem()
                                {
                                    Value = t.TrainerId.ToString(),
                                    Text = string.Format(t.FirstName + " " + t.LastName)
                                })
                                 .OrderBy(t => t.Text)
                                 .ToList();

            return selectlist;
        }

        public static IEnumerable<SelectListItem> CreateSelectListOfCourses(IEnumerable<Course> Courses)
        {
            var selectList = Courses.Select(c => new SelectListItem
            {
                Value = c.CourseId.ToString(),
                Text = c.Title
            }).OrderBy(o => o.Text);

            return selectList;
        }
    }
}