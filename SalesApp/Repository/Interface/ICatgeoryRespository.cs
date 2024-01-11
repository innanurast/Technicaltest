using SalesApp.Model;
using SalesApp.ViewModel;

namespace SalesApp.Repository.Interface
{
    public interface ICatgeoryRespository
    {
        IEnumerable<Category> Get();
        Category Get(int Id);
        int Insert(CategoryVm category);
        int Update(CategoryVm category);
        int Delete(int Id);
    }
}
