using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterfaceProject.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Values")]
    public class ValuesController : Controller
    {
    }
}