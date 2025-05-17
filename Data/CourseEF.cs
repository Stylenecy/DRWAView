using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleRESTApi.Data;
using SimpleRESTApi.Models;
using SimpleRESTApi.DTO;
namespace SimpleRESTApi.Data
{
    public class CourseEF : ICourses
    {
        private readonly ApplicationDBContext _context;
        public CourseEF(ApplicationDBContext context)
        {
            _context = context;
        }
        public SimpleRESTApi.Models.Course AddCourse(SimpleRESTApi.Models.Course course)
        {
            try
            {
                if (course == null)
                {
                    throw new ArgumentNullException(nameof(course), "Course cannot be null");
                }
                _context.Courses.Add(course);
                _context.SaveChanges();

                var createdCourse = _context.Courses
                .Include(c => c.Category)
                .Include(c => c.Instructor)
                .FirstOrDefault(c => c.CourseId == course.CourseId);

                return createdCourse!;
            }
            catch (DbUpdateException dbex)
            {
                throw new Exception("An error occurred while adding the course", dbex);
            }
            catch (System.Exception ex)
            {
                throw new Exception("An unecpected error occurred", ex);
            }
        }

        public void DeleteCourse(int courseId)
        {
            try
            {
                var course = _context.Courses.Find(courseId);
                if (course == null)
                {
                    throw new Exception("Course not found");
                }
                _context.Courses.Remove(course);
                _context.SaveChanges();
            }
            catch (DbUpdateException dbex)
            {
                throw new Exception("An error occurred while deleting the course", dbex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public ViewCourseWithCategory GetCourseById(int courseId)
        {
            var course = _context.Courses.FirstOrDefault(c => c.CourseId == courseId);
            if (course == null)
            {
                throw new Exception("Course not found");
            }
            return new ViewCourseWithCategory
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                CategoryId = course.CategoryId,
                InstructorId = course.InstructorId
            };
        }

        public Course GetCourseByIdCourse(int courseId)
        {
            var course = _context.Courses.Include(c => c.Category).Include(c => c.Instructor).FirstOrDefault(c => c.CourseId == courseId);
            if (course == null)
            {
                throw new Exception("Course not found");
            }
            return course;
        }

        public IEnumerable<ViewCourseWithCategory> GetCourses()
        {
            var courses = _context.Courses
        .Include(c => c.Category)
        .Include(c => c.Instructor)
        .OrderByDescending(c => c.CourseId)
        .ToList();

            var viewCourses = courses.Select(c => new ViewCourseWithCategory
            {
                CourseId = c.CourseId,
                CourseName = c.CourseName,
                CourseDescription = c.CourseDescription,
                Duration = c.Duration,
                CategoryId = c.CategoryId,
                CategoryName = c.Category.CategoryName,
                InstructorId = c.InstructorId,
            }).ToList();

            return viewCourses;
        }

        public Course UpdateCourse(Course course)
        {
            try
            {
                if (course == null)
                {
                    throw new ArgumentNullException(nameof(course), "Course cannot be null");
                }

                var existingCourse = _context.Courses.Find(course.CourseId);
                if (existingCourse == null)
                {
                    throw new Exception("Course not found");
                }
                existingCourse.CourseName = course.CourseName;
                existingCourse.CategoryId = course.CategoryId;
                existingCourse.InstructorId = course.InstructorId;
                existingCourse.CourseDescription = course.CourseDescription;
                existingCourse.Duration = course.Duration;
                _context.SaveChanges();
                var updatedCourse = _context.Courses
                .Include(c => c.Category)
                .Include(c => c.Instructor)
                .FirstOrDefault(c => c.CourseId == course.CourseId);
                if (updatedCourse == null)
                {
                    throw new Exception("Course not found after update");
                }

                return updatedCourse;
            }
            catch (DbUpdateException dbex)
            {
                throw new Exception("An error occurred while updating the course", dbex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating course", ex);
            }
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return _context.Courses
                .Include(c => c.Category)
                .Include(c => c.Instructor)
                .ToList();
        }

        public IEnumerable<Course> GetCoursesByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}