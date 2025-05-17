// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using SimpleRESTApi.Models;

// namespace SimpleRESTApi.Data
// {
//     public class DataAccessLayerCategory : Icategory
//     {
//         private List<Category> _categories = new List<Category>();
//         public DataAccessLayerCategory()
//         {
//             _categories.Add(new Category { CategoryId = 1, CategoryName = "ASP.NET Core" });
//             _categories.Add(new Category { CategoryId = 2, CategoryName = "ASP.NET Core MVC" });
//             _categories.Add(new Category { CategoryId = 3, CategoryName = "ASP.NET Web API" });
//             _categories.Add(new Category { CategoryId = 4, CategoryName = "Blazor" });
//             _categories.Add(new Category { CategoryId = 5, CategoryName = "Xamarin" });
//             _categories.Add(new Category { CategoryId = 6, CategoryName = "Azure" });
//             _categories.Add(new Category { CategoryId = 7, CategoryName = "Windows" });
//             _categories.Add(new Category { CategoryId = 8, CategoryName = "Python" });
//         }
//         public Category AddCategory(Category category)
//         {
//             _categories.Add(category);
//             return category;
//         }

//         public void DeleteCategory(int CategoryId)
//         {
//             var category = _categories.FirstOrDefault(c => c.CategoryId == CategoryId);
//             if(category == null)
//             {
//                 throw new Exception("Category not found");
//             }
//             _categories.Remove(category);
//         }

//         public List<Category> GetCategories()
//         {
//             return _categories;
//         }

//         public Category GetCategoryById(int id)
//         {
//             var category = _categories.FirstOrDefault(c => c.CategoryId == id);
//             if(category == null)
//             {
//                 throw new Exception("Category not found");
//             }
//             return category;
//         }

//         public Category UpdateCategory(Category category)
//         {
//             var existingCategory = GetCategoryById(category.CategoryId);
//             if(existingCategory == null)
//             {
//                 throw new Exception("Category not found");
//             }
//             existingCategory.CategoryName = category.CategoryName;
//             return category;
//         }

//         IEnumerable<Category> Icategory.GetCategories()
//         {
//             return GetCategories();
//         }
//     }
// }