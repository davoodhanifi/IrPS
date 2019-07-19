using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IrpsApi.Api.Controllers.Index
{
    public class IndexController : Controller
    {
        /// <summary>
        /// Index page.
        /// </summary>
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
    }
}
