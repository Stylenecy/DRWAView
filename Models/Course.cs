using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRESTApi.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = null!;
        public string CourseDescription { get; set; } = null!;
        public double Duration { get; set; }
        public int CategoryId { get; set; }
        public int InstructorId { get; set; }
        // Navigation Properties (penting untuk .Include dan DTO mapping)
        public Category Category { get; set; } = null!;
        public Instructor Instructor { get; set; } = null!;
    }
}