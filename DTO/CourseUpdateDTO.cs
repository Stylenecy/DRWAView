using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTApi.DTO;

namespace Simple_API.DTO
{
    public class CourseUpdateDTO
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = null!;
        public string? CourseDescription { get; set; }
        public double Duration { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public int InstructorId { get; set; } // Relasi ke Instructor
        public InstructorDTO Instructor { get; set; } = null!;
    }
}