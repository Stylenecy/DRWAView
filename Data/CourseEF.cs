using System;
using System.Collections.Generic;
using System.Linq;
using Simple_API.Data;
using Simple_API.models;
using SimpleRESTApi.Data;

namespace Simple_API.data
{
    public class CourseEF : ICourses
    {
        private readonly ApplicationDBContext _context ;
        public CourseEF(ApplicationDBContext context)
        {
            _context = context;
        }
        public Course AddCourse(Course course)
        {
            try
            {
                _context.Courses.Add(course);
                _context.SaveChanges();
                return course;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding course", ex);
            }
        }

        public void DeleteCourse(int CourseId)
        {
            var course = GetCourse(CourseId);
            if (course != null)
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Course not found");
            }
        }

        public Course GetCourse(int CourseId)
        {
            var course = _context.Courses.FirstOrDefault(c => c.CourseId == CourseId);
            if (course != null)
            {
                return course;
            }
            try
            {
                throw new Exception("Course not found");
            }
            catch (Exception ex)
            {
                throw new Exception("Error in SQL: " + ex.Message);
            }
        }

        public IEnumerable<Course> GetCourses()
        {
            var course = _context.Courses.OrderByDescending(c => c.CourseId).ToList();
            return course;
        }

        public Course UpdateCourse(Course course)
        {
            var existingCourse = GetCourse(course.CourseId);
            if (existingCourse != null)
            {
                existingCourse.CourseName = course.CourseName;
                existingCourse.CourseId = course.CourseId;
                existingCourse.CourseDescription = course.CourseDescription;
                existingCourse.Duration = course.Duration;
                existingCourse.CategoryId = course.CategoryId;
                _context.Courses.Update(existingCourse);
                _context.SaveChanges();
                // Ensure the Description column exists in the database before using it
                // existingCourse.Description = course.Description;
                return existingCourse;
            }
            else 
            {
                throw new Exception("Course not found");
            }
        }
    }
}