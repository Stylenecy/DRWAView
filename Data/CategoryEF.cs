using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SimpleRESTApi.Data;
using SimpleRESTApi.Models;

namespace SimpleRESTAPI.Data
{
    public class CategoryEF : ICategory
    {
        private readonly ApplicationDBContext _context;
        public CategoryEF(ApplicationDBContext context)
        {
            _context = context;
        }
        public Category AddCategory(Category category)
        {
            try
            {
                _context.Add(category);
                _context.SaveChanges();
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding category", ex);
            }
        }

        public void DeleteCategory(int categoryId)
        {
            var category = GetCategoryById(categoryId);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            try
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting category", ex);
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            //var categories = _context.Categories.OrderByDescending(c => c.categoryName).ToList();
            //linq
            var categories = from c in _context.Categories
                              orderby c.CategoryName descending
                              select c;
            return categories;
        }

        public Category GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Category GetCategoryById(int categoryId)
        {
            //var category = _context.Categories.FirstOrDefault(c => c.categoryId == categoryId);
            var category = (from c in _context.Categories
                            where c.CategoryId == categoryId
                            select c).FirstOrDefault();
            if (category == null)
            {
                throw new Exception("Category not found");
            }   
            return category;
        }

        public Category UpdateCategory(Category category)
        {
            var existingCategory = GetCategoryById(category.CategoryId);
            if (existingCategory == null)
            {
                throw new Exception("Category not found");
            }
            try
            {
                existingCategory.CategoryName = category.CategoryName;
                _context.SaveChanges();
                return existingCategory;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating category", ex);
            }
        }

        List<Category> ICategory.GetCategories()
        {
            throw new NotImplementedException();
        }
    }
}