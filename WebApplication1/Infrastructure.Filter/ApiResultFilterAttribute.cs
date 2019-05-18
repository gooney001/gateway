using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
namespace Infrastructure.Filters
{
    public class ApiResultFilterAttribute:ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            PathString path = context.HttpContext.Request.Path;
            if (!path.HasValue)
                return;
            if (context.Result is FileResult ||
                context.Result is EmptyResult)
                return;
            ResponseApi<object> responseApi = new ResponseApi<object>();
            if(context.Result is ObjectResult)
            {
                ObjectResult result = context.Result as ObjectResult;
                responseApi.Code = context.HttpContext.Response.StatusCode;
                if(result.DeclaredType!=null & result.DeclaredType.Name.Contains("ApiOperationResult"))
                {
                    responseApi.Data = result.Value;
                }
                else
                {
                    string str = string.Empty;
                    SerializableError serialzableError = result.Value as SerializableError;
                    if (serialzableError != null)
                    {
                        foreach(string key in serialzableError.Keys)
                        {
                            str = str + key + ":" + string.Join(",", (string[])((Dictionary<string, object>)serialzableError)[key]) + ";";
                        }
                        responseApi.Data = (object)new ApiOperationResult<object>() {
                            Message = str
                        };
                    }
                    else
                    {
                        responseApi.Data = (object)new ApiOperationResult<object>()
                        {
                            Result = result.Value
                        };
                    }
                    responseApi.Message = context.HttpContext.Response.StatusCode.ToString();
                    context.Result=new ObjectResult(responseApi);
                }
            }
        }
    }
}
