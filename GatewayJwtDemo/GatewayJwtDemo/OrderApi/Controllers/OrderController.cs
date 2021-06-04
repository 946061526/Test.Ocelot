using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        [Route("auth")]
        [Authorize("AuthJWT")]
        [HttpGet]
        public ActionResult<ApiResult> auth()
        {
            return new ApiResult { code = 200, msg = "GetByAuth Success!", data = null };
        }

        //[AllowAnonymous]
        [HttpGet]
        [Route("noAuth")]
        public ActionResult<ApiResult> noAuth()
        {
            return new ApiResult { code = 200, msg = "GetWithOutAuth1 Success!", data = null };
        }
    }
}
