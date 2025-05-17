using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTApi.Data;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public interface ICategory
    {
        public List<Category> GetCategories();
        public Category GetCategory(int id);
        public Category AddCategory(Category category);
        public Category UpdateCategory(Category category);
        public void DeleteCategory(int id);
    }
}