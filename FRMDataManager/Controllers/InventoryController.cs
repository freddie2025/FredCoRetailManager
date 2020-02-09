using FRMDataManager.Library.DataAccess;
using FRMDataManager.Library.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace FRMDataManager.Controllers
{
    //[Authorize]
    public class InventoryController : ApiController
    {
        public List<InventoryModel> Get()
        {
            InventoryData data = new InventoryData();
            return data.GetInventory();
        }

        public void Post(InventoryModel item)
        {
            InventoryData data = new InventoryData();
            data.SaveInventoryRecord(item);
        }
    }
}
