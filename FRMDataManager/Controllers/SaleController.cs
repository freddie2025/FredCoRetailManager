using FRMDataManager.Library.DataAccess;
using FRMDataManager.Library.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace FRMDataManager.Controllers
{
    [Authorize]
    public class SaleController : ApiController
    {
        public void Post(SaleModel sale)
        {
            SaleData data = new SaleData();
            string userId = RequestContext.Principal.Identity.GetUserId();

            data.SaveSale(sale, userId);
        }
    }
}
