using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTApi.DTO;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.DTO
{
    public class CourseDTO
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = null!;
        public string? CourseDescription { get; set; } = null!;
        public double Duration { get; set; }
        public CategoryDTO? Category { get; set; }
        public InstructorDTO? Instructor { get; set; }
    }
}