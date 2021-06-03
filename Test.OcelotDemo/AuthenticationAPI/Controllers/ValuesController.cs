using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Test.Common;

namespace AuthenticationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        [AllowAnonymous]
        [HttpPost]
        public ActionResult<ApiResult> AccountLogin([FromBody] dynamic obj)
        {

            //AuthService ser = new AuthService();
            //ApiResult result = ser.AccessAuthorization(obj);


            //if (result.StatusCode != 200)
            //{
            //    return ReturnOperterResult(result.StatusCode, result.Msg, null, result.Msg);
            //}
            //else
            //{
            //    string username = ((AccessInfo)result.Data).UserName;
            //    string userid = ((AccessInfo)result.Data).UserId.ToString();
            //    string OrgId = ((AccessInfo)result.Data).OrgId.ToString();
            //    string OrgName = ((AccessInfo)result.Data).OrgName.ToString();
            //    string IsAdmin = ((AccessInfo)result.Data).IsAdmin.ToString();
            //    string ClientType = ((AccessInfo)result.Data).ClientType.ToString();
            //    //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
            //    var claims = new Claim[] {
            //        new Claim("userName", username),
            //        new Claim("clientType",ClientType.ToString()),
            //        new Claim("userid",userid),
            //        new Claim("jti",Guid.NewGuid().ToString()),
            //        new Claim("orgId",OrgId),
            //        new Claim("orgName",OrgName),
            //        new Claim("isAdmin",IsAdmin)
            //    };
            //    //用户标识
            //    var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            //    identity.AddClaims(claims);
            //    var token = JwtToken.BuildJwtToken(claims, _requirement, (AuthClientType)(Convert.ToInt32(ClientType)));
            //    return ReturnOperterResult(200, "认证成功",
            //        new
            //        {
            //            Token = token,
            //            UserId = Convert.ToInt32(userid),
            //            UserName = username,
            //            OrgId = Convert.ToInt32(OrgId),
            //            OrgName = OrgName,
            //            IsAdmin = Convert.ToBoolean(IsAdmin),
            //            ClientType = Convert.ToInt32(ClientType)
            //        });
            //}

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var _requirement = new PermissionRequirement("Benjamin", "everone", creds, "True") { };

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
        

            return new ApiResult { code = 200, msg = "ok",data = new
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
    }
}
