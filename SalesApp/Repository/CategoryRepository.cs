using Microsoft.EntityFrameworkCore;
using SalesApp.Context;
using SalesApp.Model;
using SalesApp.ViewModel;
using SalesApp.Repository.Interface;

namespace SalesApp.Repository
{
    public class CategoryRepository : ICatgeoryRespository
    {
        private readonly MyContext context;

        public CategoryRepository(MyContext context)
        {
            this.context = context; //this.context sama seperti perintah _context
        }
        public int Delete(int Id)
        {
            var findData = context.Categories.Find(Id);
            if (findData != null)
            {
                context.Categories.Remove(findData);
            }
            var save = context.SaveChanges();
            return save;
        }

        public IEnumerable<Category> Get()
        {
            return context.Categories.ToList();
        }

        public Category Get(int Id)
        {
            var data = context.Categories.Find(Id);
            return data;
        }

        public int Insert(CategoryVm category)
        {
            var categoryData = new Category
            {
                Name = category.Name
            };

            context.Categories.Add(categoryData);
            var save = context.SaveChanges();

            return save;
        }

        public int Update(CategoryVm category)
        {
            var data = context.Categories.Find(category.Id);
            data.Name = category.Name;
            var result = context.SaveChanges();
            return result;
        }
    }
}
