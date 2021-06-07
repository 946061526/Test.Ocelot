using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 权限授权Handler
    /// </summary>
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        /// <summary>
        /// 验证方案提供对象
        /// </summary>
        public IAuthenticationSchemeProvider Schemes { get; set; }
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="schemes"></param>
        public PermissionHandler(IAuthenticationSchemeProvider schemes)
        {
            Schemes = schemes;
        }
        ///// <summary>
        ///// 构造
        ///// </summary>
        ///// <param name="schemes"></param>
        //public PermissionHandler(IAuthenticationSchemeProvider schemes, IHttpContextAccessor httpContextAccessor)
        //{
        //    Schemes = schemes;
        //    this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        //}



        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            try
            {
                //是否启用jwt验证
                if (!requirement.OpenJwt)
                {
                    context.Succeed(requirement);
                    return;
                }
                if (context.User == null || !context.User.Identity.IsAuthenticated)
                {
                    context.Fail();
                    return;
                }

                //string loggedInAdminId =
                //context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

                //string adminIdBeingEdited = httpContextAccessor.HttpContext.Request.Query["UserId"].ToString();

                ////从AuthorizationHandlerContext转成HttpContext，以便取出表求信息
                //var httpContext = (context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext).HttpContext;
                ////请求Url
                //var questUrl = httpContext.Request.Path.Value.ToLower();
                ////判断请求是否停止
                //var handlers = httpContext.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
                //foreach (var scheme in await Schemes.GetRequestHandlerSchemesAsync())
                //{
                //    var handler = await handlers.GetHandlerAsync(httpContext, scheme.Name) as IAuthenticationRequestHandler;
                //    if (handler != null && await handler.HandleRequestAsync())
                //    {
                //        context.Fail();
                //        return;
                //    }
                //}
                ////判断请求是否拥有凭据，即有没有登录
                //var defaultAuthenticate = await Schemes.GetDefaultAuthenticateSchemeAsync();
                //if (defaultAuthenticate != null)
                //{
                //    var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
                //    //result?.Principal不为空即登录成功
                //    if (result?.Principal != null)
                //    {
                //        httpContext.User = result.Principal;

                //        //var jwtToken = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                //        //var playload = JsonConvert.DeserializeObject<dynamic>(EncrypHelper.DeCodeBase64("utf-8", jwtToken.Split('.')[1]));
                //        //string identity = playload["identity"];
                //        //string clientType = playload["clientType"];
                //        //string username = playload["userName"];
                //        //double exp = playload["exp"];
                //        string userid = "";
                //        string clientType = "";
                //        string username = "";
                //        double exp = 0;
                //        string isAdmin = "";
                //        foreach (var a in httpContext.User.Claims)
                //        {
                //            switch (a.Type)
                //            {
                //                case "userid":
                //                    userid = a.Value;
                //                    break;
                //                case "clientType":
                //                    clientType = a.Value;
                //                    break;
                //                case "userName":
                //                    username = a.Value;
                //                    break;
                //                case "exp":
                //                    exp = Convert.ToDouble(a.Value);
                //                    break;
                //                case "isAdmin":
                //                    isAdmin = a.Value;
                //                    break;
                //                default:
                //                    break;
                //            }
                //        }


                //        System.DateTime startTime = Convert.ToDateTime(new System.DateTime(1970, 1, 1)); // 当地时区
                //        DateTime expDate = startTime.AddSeconds(exp).AddHours(8);

                //        if (string.IsNullOrEmpty(userid) || string.IsNullOrEmpty(clientType))
                //        {
                //            context.Fail();
                //            return;
                //        }

                //        //根据clientType判断是否需要校验url权限  saas平台的需要校验url权限 
                //        //todo 根据自己的权限逻辑编写验证代码  

                //        //判断过期时间  
                //        if (expDate >= DateTime.Now)
                //        {
                //            context.Succeed(requirement);
                //        }
                //        else
                //        {
                //            context.Fail();
                //        }
                //        return;
                //    }
                //}
                ////判断没有登录时，是否访问登录的url,并且是Post请求，并且是form表单提交类型，否则为失败
                //if (!questUrl.Equals(requirement.LoginPath.ToLower(), StringComparison.Ordinal) && (!httpContext.Request.Method.Equals("POST")
                //|| !httpContext.Request.HasFormContentType))
                //{
                //    context.Fail();
                //    return;
                //}
                //context.Succeed(requirement);

                //从AuthorizationHandlerContext转成HttpContext，以便取出表求信息
                AuthorizationFilterContext filterContext = context.Resource as AuthorizationFilterContext;
                HttpContext httpContext = filterContext.HttpContext;
                AuthenticateResult result = await httpContext.AuthenticateAsync(Schemes.GetDefaultAuthenticateSchemeAsync().Result.Name);
                //如果没登录result.Succeeded为false
                if (result.Succeeded)
                {
                    httpContext.User = result.Principal;
                    //当前访问的Controller
                    string controllerName = filterContext.RouteData.Values["Controller"].ToString();//通过ActionContext类的RouteData属性获取Controller的名称：Home
                                                                                                    //当前访问的Action
                    string actionName = filterContext.RouteData.Values["Action"].ToString();//通过ActionContext类的RouteData属性获取Action的名称：Index
                    string name = httpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Name)?.Value;

                    context.Succeed(requirement);

                    //string perData = await _cacheService.GetStringAsync("perm" + name);
                    //List<PermissionItem> lst = JsonConvert.DeserializeObject<List<PermissionItem>>(perData);
                    //if (lst.Where(w => w.controllerName == controllerName && w.actionName == actionName).Count() > 0)
                    //{
                    //    //如果在配置的权限表里正常走
                    //    context.Succeed(requirement);
                    //}
                    //else
                    {
                        //不在权限配置表里 做错误提示
                        //如果是AJAX请求 (包含了VUE等 的ajax)
                        string requestType = filterContext.HttpContext.Request.Headers["X-Requested-With"];
                        if (!string.IsNullOrEmpty(requestType) && requestType.Equals("XMLHttpRequest", StringComparison.CurrentCultureIgnoreCase))
                        {
                            //ajax 的错误返回
                            //filterContext.Result = new StatusCodeResult(499); //自定义错误号 ajax请求错误 可以用来错没有权限判断 也可以不写 用默认的
                            context.Fail();
                        }
                        else
                        {
                            //普通页面错误提示 就是跳转一个页面
                            //httpContext.Response.Redirect("/Home/visitDeny");//第一种方式跳转
                            filterContext.Result = new RedirectToActionResult("visitDeny", "Home", null);//第二种方式跳转
                            context.Fail();
                        }
                    }
                }
                else
                {
                    context.Fail();
                }
            }
            catch (Exception ex)
            {

            }
        }


    }
}
