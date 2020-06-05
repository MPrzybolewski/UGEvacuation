using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UGEvacuationBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AgusiaController : Controller
    {
        [HttpGet]
        [Authorize]
        public string Index()
        {
            return "<3 <3 <3 <3 <3 <3 <3 <3";
        }
    }
}