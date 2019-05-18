using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Filters
{
    public sealed class ResponseApi<T>
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 响应数据
        /// </summary>
        public T Data { get; set; }
    }
}
