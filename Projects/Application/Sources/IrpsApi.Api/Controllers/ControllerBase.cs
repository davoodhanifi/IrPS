using System;
using IrpsApi.Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IrpsApi.Api.Controllers
{
    public abstract class ControllerBase<T> : Controller
    {
        #region HTTP Status Codes

        protected UnprocessableEntityObjectResult UnprocessableEntity(ModelStateDictionary modelState)
        {
            return new UnprocessableEntityObjectResult(modelState);
        }

        #endregion
    }
}
