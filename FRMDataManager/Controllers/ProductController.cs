using FRMDataManager.Library.DataAccess;
using FRMDataManager.Library.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace FRMDataManager.Controllers
{
    [Authorize(Roles = "Cashier,Admin")]
    public class ProductController : ApiController
    {
        public List<ProductModel> Get()
        {
            ProductData data = new ProductData();

            return data.GetProducts();
        }
    }
}
