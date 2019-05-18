using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Filters
{
    public sealed class ApiOperationResult<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string MsgCode { get; set; }
        public T Result { get; set; }
    }
}
