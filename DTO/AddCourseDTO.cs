using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRESTApi.DTO
{
    public class AddCourseDTO
    {
        public string CourseName { get; set; } = null!;
        public string? CourseDescription { get; set; }
        public double Duration { get; set; }
        public int categoryId { get; set; }
        public int InstructorId { get; set; }
    
    }
}