using System;
using Microsoft.AspNetCore.Mvc;
using UGEvacuationCommon.Enums;
using UGEvacuationCommon.Models;

namespace UGEvacuationBackend.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult GetStatusCodeFromException(Exception exception)
        {
            var ugEvacuationException = exception as UGEvacuationException;
            var errorType = ugEvacuationException?.Type ?? ErrorType.Unspecified;

            switch (errorType)
            {
                case ErrorType.AccessDenied:
                    return StatusCode(403);
                case ErrorType.InvalidArgument:
                    return BadRequest(); 
                case ErrorType.NoPath:
                    return StatusCode(433);
            }
            return new StatusCodeResult((int)System.Net.HttpStatusCode.InternalServerError);
        }
    }
}