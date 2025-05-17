using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class DataAccessLayerInstructor : interfaceInstructor
    {
        private List<Instructor> _instructors = new List<Instructor>();
        public DataAccessLayerInstructor()
        {
            _instructors.AddRange(
                new Instructor 
                { 
                    InstructorId = 1, 
                    InstructorName = "John Doe", 
                    InstructorEmail = "john.doe@gmail.com",
                    InstructorPhone = "123-456-7890",
                    InstructorAddress = "123 Main St",
                    InstructorCity = "New York"
                },
                new Instructor 
                { 
                    InstructorId = 2, 
                    InstructorName = "Jane Geez", 
                    InstructorEmail = "jane.geez@gmail.com",
                    InstructorPhone = "135-246-7890",
                    InstructorAddress = "456 Main St",
                    InstructorCity = "Los Angeles"
                },
                new Instructor 
                { 
                    InstructorId = 3, 
                    InstructorName = "Jack White", 
                    InstructorEmail = "jack.white@gmail.com",
                    InstructorPhone = "987-654-3210",
                    InstructorAddress = "789 Main St",
                    InstructorCity = "Chicago"
                },
                new Instructor
                {
                    InstructorId = 4,
                    InstructorName = "Jill Bub",
                    InstructorEmail = "jill.bub@gmail.com",
                    InstructorPhone = "975-864-3210",
                    InstructorAddress = "987 Main St",  
                    InstructorCity = "Houston"
                },
                new Instructor
                {
                    InstructorId = 5,
                    InstructorName = "Idris Elba",
                    InstructorEmail = "idris.elba@gmail.com",
                    InstructorPhone = "333-444-5555",
                    InstructorAddress = "123 Elm St",
                    InstructorCity = "London"
                },
                new Instructor
                {
                    InstructorId = 6,
                    InstructorName = "Tomb Ngeheng",
                    InstructorEmail = "tomb.ngeheng@gmail.com",
                    InstructorPhone = "666-777-8888",
                    InstructorAddress = "456 Elm St",
                    InstructorCity = "Paris"
                },
                new Instructor
                {
                    InstructorId = 7,
                    InstructorName = "Dave Eros",
                    InstructorEmail = "dave.eros@gmail.com",
                    InstructorPhone = "999-000-1111",
                    InstructorAddress = "789 Elm St",
                    InstructorCity = "Berlin"
                });
        }
        public Instructor AddInstructorAsync(Instructor instructor)
        {
            _instructors.Add(instructor);
            return instructor;
        }

        public void DeleteInstructor(int id)
        {
            var instructor = GetInstructorById(id);
            _instructors.Remove(instructor);
        }

        public Instructor GetInstructorById(int id)
        {
            var instructor = _instructors.FirstOrDefault(i => i.InstructorId == id);
            if(instructor == null)
            {
                throw new Exception("Instructor not found");
            }
            return instructor;
        }

        public List<Instructor> GetInstructors()
        {
            return _instructors;
        }

        public Instructor UpdateInstructor(Instructor instructor)
        {
            var existingInstructor = GetInstructorById(instructor.InstructorId);
            if(existingInstructor == null)
            {
                throw new Exception("Instructor not found");
            }
            existingInstructor.InstructorName = instructor.InstructorName;
            existingInstructor.InstructorEmail = instructor.InstructorEmail;
            existingInstructor.InstructorPhone = instructor.InstructorPhone;
            existingInstructor.InstructorAddress = instructor.InstructorAddress;
            existingInstructor.InstructorCity = instructor.InstructorCity;
            return instructor;
        }
    }
}