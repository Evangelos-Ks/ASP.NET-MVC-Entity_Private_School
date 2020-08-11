using System;
using Assignment2.Services;

namespace Assignment2.Desktop
{
    class Program
    {
        static void Main(string[] args)
        {
            CourseRepository courseRepository = new CourseRepository();

            var courses = courseRepository.GetAll();

            foreach (var course in courses)
            {
                Console.WriteLine(course.Title);
                foreach (var assignment in course.Assignments)
                {
                    Console.WriteLine("\t" + assignment.Title);
                }
                foreach (var studentCourse in course.StudentCourses)
                {
                    Console.WriteLine("\t\t" + studentCourse.Student.FirstName);
                }
            }
            courseRepository.Dispose();

        }
    }
}
