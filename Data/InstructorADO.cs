using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class InstructorADO : IInstructor
    {
        private readonly string _connectionString;

        public InstructorADO(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public Instructor AddInstructor(Instructor instructor)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string strSql = @"INSERT INTO Instructors (InstructorName, InstructorEmail, InstructorPhone, InstructorAddress, InstructorCity) 
                                VALUES (@InstructorName, @InstructorEmail, @InstructorPhone, @InstructorAddress, @InstructorCity);
                                SELECT SCOPE_IDENTITY()";

                using (SqlCommand cmd = new SqlCommand(strSql, conn))
                {
                    cmd.Parameters.AddWithValue("@InstructorName", instructor.InstructorName);
                    cmd.Parameters.AddWithValue("@InstructorEmail", instructor.InstructorEmail);
                    cmd.Parameters.AddWithValue("@InstructorPhone", instructor.InstructorPhone);
                    cmd.Parameters.AddWithValue("@InstructorAddress", instructor.InstructorAddress);
                    cmd.Parameters.AddWithValue("@InstructorCity", instructor.InstructorCity);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    instructor.InstructorId = (result != DBNull.Value) ? Convert.ToInt32(result) : 0;
                    return instructor;
                }
            }
        }

        public void DeleteInstructor(int instructorId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string strSql = "DELETE FROM Instructors WHERE InstructorId = @InstructorId";
                using (SqlCommand cmd = new SqlCommand(strSql, conn))
                {
                    cmd.Parameters.AddWithValue("@InstructorId", instructorId);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {
                        throw new Exception("Instructor not found");
                    }
                }
            }
        }

        public Instructor GetInstructorById(int instructorId)
        {
            Instructor instructor = new();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string strSql = "SELECT * FROM Instructors WHERE InstructorId = @InstructorId";
                using (SqlCommand cmd = new SqlCommand(strSql, conn))
                {
                    cmd.Parameters.AddWithValue("@InstructorId", instructorId);
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            instructor.InstructorId = Convert.ToInt32(dr["InstructorId"]);
                            instructor.InstructorName = dr["InstructorName"].ToString();
                            instructor.InstructorEmail = dr["InstructorEmail"].ToString();
                            instructor.InstructorPhone = dr["InstructorPhone"].ToString();
                            instructor.InstructorAddress = dr["InstructorAddress"].ToString();
                            instructor.InstructorCity = dr["InstructorCity"].ToString();
                        }
                        else
                        {
                            throw new Exception("Instructor not found");
                        }
                    }
                }
            }
            return instructor;
        }

        public IEnumerable<Instructor> GetInstructors()
        {
            List<Instructor> instructors = new List<Instructor>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string strSql = "SELECT * FROM Instructors ORDER BY InstructorName";
                using (SqlCommand cmd = new SqlCommand(strSql, conn))
                {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            instructors.Add(new Instructor
                            {
                                InstructorId = Convert.ToInt32(dr["InstructorId"]),
                                InstructorName = dr["InstructorName"].ToString(),
                                InstructorEmail = dr["InstructorEmail"].ToString(),
                                InstructorPhone = dr["InstructorPhone"].ToString(),
                                InstructorAddress = dr["InstructorAddress"].ToString(),
                                InstructorCity = dr["InstructorCity"].ToString()
                            });
                        }
                    }
                }
            }
            return instructors;
        }

        public Instructor UpdateInstructor(Instructor instructor)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string strSql = @"UPDATE Instructors 
                                SET InstructorName = @InstructorName, 
                                    InstructorEmail = @InstructorEmail, 
                                    InstructorPhone = @InstructorPhone, 
                                    InstructorAddress = @InstructorAddress, 
                                    InstructorCity = @InstructorCity
                                WHERE InstructorId = @InstructorId";
                using (SqlCommand cmd = new SqlCommand(strSql, conn))
                {
                    cmd.Parameters.AddWithValue("@InstructorName", instructor.InstructorName);
                    cmd.Parameters.AddWithValue("@InstructorEmail", instructor.InstructorEmail);
                    cmd.Parameters.AddWithValue("@InstructorPhone", instructor.InstructorPhone);
                    cmd.Parameters.AddWithValue("@InstructorAddress", instructor.InstructorAddress);
                    cmd.Parameters.AddWithValue("@InstructorCity", instructor.InstructorCity);
                    cmd.Parameters.AddWithValue("@InstructorId", instructor.InstructorId);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {
                        throw new Exception("Instructor not found");
                    }
                    return instructor;
                }
            }
        }
    }
}