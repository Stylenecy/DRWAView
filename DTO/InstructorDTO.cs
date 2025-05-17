using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRESTApi.DTO
{
    public class InstructorDTO
    {
        public int InstructorId { get; set; }
        public string InstructorName { get; set; } = null!; 
    }
}