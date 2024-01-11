using SalesApp.ViewModel;

namespace SalesApp.Repository.Interface
{
    public interface ISaleproductRepository
    {
        IEnumerable<SaleproductAndProductVm> Get();
        SaleproductAndProductVm Get(string saleProductId);
        int Insert(SaleproductVm saleproduct);
        int Update(string saleProductId, SaleproductVm saleproduct);
        int Delete(string saleProductId);
    }
}
