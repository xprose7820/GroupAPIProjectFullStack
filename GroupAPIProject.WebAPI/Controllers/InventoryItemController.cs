using Microsoft.AspNetCore.Authorization;


namespace GroupAPIProject.WebAPI.Controllers
{ [Authorize("Roles=RetailerEntity")]
    public class InventoryItemController
    {
    }
}
