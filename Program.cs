using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HierarchyStructure
{

    public class Category
    {

        public long CategoryId { get; set; }
        public string Name { get; set; }
        public long ParentCategoryId { get; set; }

    }

    public class CategoryHierarchy
    {
        public Category Category { get; set; }
        public List<CategoryHierarchy> Children { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Get Category List
            var categoryList = GetCategoriesFromJsonFile();

            var hierarchy = GenerateHierarchy(categoryList);

            Console.ReadLine();
        }

        /// <summary>
        /// Generates Parent-Child Hierarchy and prints on console before returning 
        /// </summary>
        /// <param name="categoryList">List of Category Objects</param>
        /// <param name="parentId">Parent Category Id default(0) for Main Categories</param>
        /// <param name="indent">string to represent separation between different level</param>
        /// <returns>Category Hierarchy List</returns>
        public static List<CategoryHierarchy> GenerateHierarchy(List<Category> categoryList, long parentId = 0, string indent = "")
        {
            //use to store current level categories
            var catHierarchy = new List<CategoryHierarchy>();

            //Get current Level categories
            var currentLevelCategories = categoryList.Where(c => c.ParentCategoryId == parentId).ToList();

            //Loop through current level categories 
            foreach (var cat in currentLevelCategories)
            {
                Console.WriteLine($"{indent}{cat.Name}");

                catHierarchy.Add(new CategoryHierarchy()
                {
                    Category = cat,
                    //Get Child hierarchy for current category i.e cat
                    Children = GenerateHierarchy(categoryList, cat.CategoryId, string.Concat(indent, "--")) // Calling self function

                });
            }

            return catHierarchy;
        }
        /// <summary>
        /// Create List of Category from JSON file
        /// </summary>
        /// <returns></returns>
        public static List<Category> GetCategoriesFromJsonFile()
        {
            using (var reader = new StreamReader("C:/Users/Administrador/Downloads/ejercicios METROCARRIER/ejercicio_3/HierarchyStructure/data/category.json"))
            {
                var categoryData = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Category>>(categoryData);

            }
        }
    }
}