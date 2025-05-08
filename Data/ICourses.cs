using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simple_API.models;

namespace Simple_API.Data
{
    public interface ICourses
    {
        IEnumerable<Course> GetCourses();
        Course GetCourse(int courseId);
        Course AddCourse(Course course);
        Course UpdateCourse(Course course);
        void DeleteCourse(int courseId);
    }
}