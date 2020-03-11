using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Test.ApiUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [Route("AddUser")]
        [HttpPost]
        public int AddUser([FromBody]AddUser request)
        {
            return 1;
        }

        [Route("GetUser")]
        [HttpGet]
        public UserInfo GetUser(string name, string pwd)
        {
            return new UserInfo() { Name = name, Pwd = pwd };
        }

        [Route("GetUserList")]
        [HttpGet]
        public List<UserInfo> GetUserList(string name, string pwd)
        {
            return new List<UserInfo>() { new UserInfo() { Name = name, Pwd = pwd } };
        }

        [Route("GetUserList2")]
        [HttpPost]
        public List<UserInfo> GetUserList2([FromBody]UserInfo userInfo)
        {
            return new List<UserInfo>() { userInfo };
        }
    }

    public class AddUser
    {
        public string Name { get; set; }
        public string Pwd { get; set; }
    }
    public class UserInfo
    {
        public string Name { get; set; }
        public string Pwd { get; set; }
    }
}