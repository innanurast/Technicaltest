using SalesApp.Model;
using SalesApp.ViewModel;

namespace SalesApp.Repository.Interface
{
    public interface IProductRepository
    {
        IEnumerable<ProductAndCategoryVm> Get();
        ProductAndCategoryVm Get(string ProductId);
        int Insert(ProductVm productVm);
        int Update(string ProductId, ProductVm productVm);
        int Delete(string ProductId);

    }
}
