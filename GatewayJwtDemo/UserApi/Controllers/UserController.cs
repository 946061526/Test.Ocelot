using Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Serilog;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger; // <-添加此行
        public UserController(ILogger<UserController> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        [Route("auth")]
        [Authorize("AuthJWT")]
        [HttpGet]
        public ActionResult<ApiResult> auth()
        {

            string userName = HttpContext.AuthenticateAsync().Result.Principal.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier))?.Value;

            return new ApiResult { code = 200, msg = "GetByAuth Success!", data = new { userName = userName } };
        }

        //[AllowAnonymous]
        [HttpGet]
        [Route("noAuth")]
        public ActionResult<ApiResult> noAuth()
        {
            logger.LogInformation("asfasfdsf");
            return new ApiResult { code = 200, msg = "GetWithOutAuth1 Success!", data = null };
        }

        [HttpPost]
        [Route("post")]
        public ActionResult<ApiResult> post()
        {
            return new ApiResult { code = 200, msg = "post Success!", data = null };
        }
    }
}
