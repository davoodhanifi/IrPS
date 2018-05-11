using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IrpsApi.Api.Controllers.System
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class PingController : Controller
    {
        [HttpGet]
        public void TestConnection()
        {
        }
    }
}