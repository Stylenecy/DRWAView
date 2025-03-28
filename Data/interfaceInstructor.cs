using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public interface interfaceInstructor
    {
        public List<Instructor> GetInstructors();
        public Instructor GetInstructorById(int id);
        public Instructor AddInstructorAsync(Instructor instructor);
        public Instructor UpdateInstructor(Instructor instructor);
        public void DeleteInstructor(int instructorId);
    }
}