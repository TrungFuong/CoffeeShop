using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CoffeeShop.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected Guid UserID => Guid.Parse(FindClaim(ClaimTypes.Actor));
        protected string UserName => FindClaim(ClaimTypes.NameIdentifier).ToString();
        protected string CurrentToken => HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        private string FindClaim(string claimName)
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity?.FindFirst(claimName);

            return claim?.Value;
        }
    }

}
