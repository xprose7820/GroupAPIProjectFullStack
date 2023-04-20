using Microsoft.AspNetCore.Authorization;

namespace GroupAPIProject.WebAPI.Controllers
{
    [Authorize("Roles=AdminEntity")]
    public class ProductController
    {
    }
}
