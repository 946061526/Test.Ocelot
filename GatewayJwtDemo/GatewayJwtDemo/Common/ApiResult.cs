﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class ApiResult
    {
        public int code { get; set; }
        public string msg { get; set; }
        public object data { get; set; }
    }
}
