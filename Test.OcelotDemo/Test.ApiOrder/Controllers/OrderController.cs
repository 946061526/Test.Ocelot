using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Test.ApiOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
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