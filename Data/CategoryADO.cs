using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SimpleRESTApi.Data;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class CategoryADO : ICategory
    {
        private readonly IConfiguration _configuration;
        private string connstr = string.Empty;
        public CategoryADO(IConfiguration configuration)
        {
            _configuration = configuration;
            connstr = _configuration.GetConnectionString("DefaultConnection");

        }
        public Category AddCategory(Category category)
        {
            using(SqlConnection conn = new SqlConnection(connstr))
            {
                string strsql = @"INSERT INTO categories (CategoryName) 
                                  VALUES (@CategoryName);
                                  SELECT SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                try
                {
                    
                    cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    conn.Open();
                    int CategoryId = Convert.ToInt32(cmd.ExecuteScalar());
                    category.CategoryId = CategoryId;
                    //throw new Exception("No records found");
                    //category.CategoryId = id;
                    return category;
                }
                catch (Exception ex) 
                {
                    throw new Exception("Error  category: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void DeleteCategory(int id)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                string strsql = @"DELETE FROM Categories WHERE CategoryID = @CategoryID";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                cmd.Parameters.AddWithValue("@CategoryID", id);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception("No records found");
                }
            }
        }

        public List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();
            using (SqlConnection con = new SqlConnection(connstr))
            {
                string strsql = @"SELECT * FROM Categories ORDER BY CategoryName";
                SqlCommand cmd = new SqlCommand(strsql, con);
                Category category = new();
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while(dr.Read()){
                    
                    category.CategoryId = Convert.ToInt32(dr["CategoryID"]);
                    category.CategoryName = dr["CategoryName"].ToString();
                    categories.Add(category);
                    
                    }
                    
                }
                else
                {
                    throw new Exception("No records found");
                }
                    dr.Close();     
                    cmd.Dispose();
                    con.Close();

            }
            return categories;
        }

        public Category GetCategory(int id)
        {
            Category category = new();
            using(SqlConnection conn = new SqlConnection(connstr))
            {
                string strsql = @"SELECT * FROM Categories WHERE CategoryID = @CategoryID";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                cmd.Parameters.AddWithValue("@CategoryID", id);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    category.CategoryId = Convert.ToInt32(dr["CategoryID"]);
                    category.CategoryName = dr["CategoryName"].ToString();
                    
                } 
                else
                {
                    throw new Exception("No records found");
                }
            }
            return category;
        }

        public Category UpdateCategory(Category category)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                string strsql = @"UPDATE Categories SET CategoryName = @CategoryName WHERE CategoryID = @CategoryID";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                try{
                    cmd.Parameters.AddWithValue("@CategoryID", category.CategoryId);
                    cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {
                        throw new Exception("No records found");
                    }
                    return category;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error  category: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}