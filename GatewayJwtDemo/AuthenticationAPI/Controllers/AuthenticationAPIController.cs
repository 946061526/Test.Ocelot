﻿using Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationAPIController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<AuthenticationAPIController> _logger;

        public AuthenticationAPIController(ILogger<AuthenticationAPIController> logger)
        {
            _logger = logger;
        }

        [Route("get")]
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }



        [Route("login")]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult<ApiResult> login([FromQuery] Req obj)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var _requirement = new PermissionRequirement("Vim", "everyone", creds, "True") { };

            string username = obj.UserName.ToString();
            string userid = obj.UserId.ToString();
            string OrgId = obj.OrgId.ToString();
            string OrgName = obj.OrgName.ToString();
            string IsAdmin = obj.IsAdmin.ToString();
            string ClientType = obj.ClientType.ToString();
            //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
            var claims = new Claim[] {
                    new Claim("userName", username),
                    new Claim("clientType",ClientType.ToString()),
                    new Claim("userid",userid),
                    new Claim("jti",Guid.NewGuid().ToString()),
                    new Claim("orgId",OrgId),
                    new Claim("orgName",OrgName),
                    new Claim("isAdmin",IsAdmin)
                };
            //用户标识
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            identity.AddClaims(claims);
            var token = JwtToken.BuildJwtToken(claims, _requirement, (AuthClientType)(Convert.ToInt32(ClientType)));


            return new ApiResult
            {
                code = 200,
                msg = "ok",
                data = new
                {
                    Token = token,
                    UserId = Convert.ToInt32(userid),
                    UserName = username,
                    OrgId = Convert.ToInt32(OrgId),
                    OrgName = OrgName,
                    IsAdmin = Convert.ToBoolean(IsAdmin),
                    ClientType = Convert.ToInt32(ClientType)
                }
            };
        }

        [Route("login2")]
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<ApiResult> login([FromQuery] string userName, string pwd, int ClientType)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var _requirement = new PermissionRequirement("Vim", "everyone", creds, "True") { };

            var claims = new Claim[] {
                    new Claim("userName", userName),
                    new Claim("clientType",ClientType.ToString()),
                    new Claim("pwd",pwd),
                    new Claim("jti",Guid.NewGuid().ToString()),
                };
            //用户标识
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            identity.AddClaims(claims);
            var token = JwtToken.BuildJwtToken(claims, _requirement, (AuthClientType)(Convert.ToInt32(ClientType)));


            return new ApiResult
            {
                code = 200,
                msg = "ok",
                data = new
                {
                    Token = token,
                    UserName = userName,
                    ClientType = ClientType
                }
            };
        }
    }

    public class Req
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int OrgId { get; set; }
        public string OrgName { get; set; }
        public string IsAdmin { get; set; }
        public int ClientType { get; set; }
    }
}
