using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{

    public class PermissionRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// 请求路径
        /// </summary>
        public string LoginPath { get; set; } = "/api/Auth/Login";
        /// <summary>
        /// 发行人
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 订阅人
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 签名验证
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }

        /// <summary>
        /// 是否启用jwt验证
        /// </summary>
        public bool OpenJwt { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="issuer">发行人</param>
        /// <param name="audience">订阅人</param>
        /// <param name="signingCredentials">签名验证实体</param>
        public PermissionRequirement(string issuer, string audience, SigningCredentials signingCredentials, bool openJWT)
        {
            Issuer = issuer;
            Audience = audience;
            SigningCredentials = signingCredentials;
            OpenJwt = openJWT;
        }
    }

}
