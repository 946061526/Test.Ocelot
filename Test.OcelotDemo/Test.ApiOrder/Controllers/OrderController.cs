using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.Common;

namespace Test.ApiOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [Authorize("AuthJWT")]
        [HttpGet]
        public ActionResult<ApiResult> GetByAuth()
        {
            return new ApiResult { code = 200, msg = "GetByAuth Success!", data = null };
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<ApiResult> GetWithOutAuth1()
        {
            return new ApiResult { code = 200, msg = "GetWithOutAuth1 Success!", data = null };
        }

        [Route("AddOrder")]
        [HttpPost]
        public int AddOrder([FromBody]AddOrder request)
        {
            return 1;
        }

        [Route("GetOrder")]
        [HttpGet]
        public OrderInfo GetOrder(string id, string name)
        {
            return new OrderInfo() { id = id, name = name };
        }

        [Route("GetOrderList")]
        [HttpGet]
        public List<OrderInfo> GetOrderList(string id, string name)
        {
            return new List<OrderInfo>() { new OrderInfo() { id = id, name = name } };
        }

        [Route("GetOrderList2")]
        [HttpPost]
        public List<OrderInfo> GetOrderList2([FromBody]OrderInfo OrderInfo)
        {
            return new List<OrderInfo>() { OrderInfo };
        }
    }

    public class AddOrder
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class OrderInfo
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}