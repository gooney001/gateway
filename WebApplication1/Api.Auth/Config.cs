using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Auth
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            List<ApiResource> resources = new List<ApiResource>();
            resources.Add(new ApiResource("API","接口API"));
            return resources;
        }
        public static IEnumerable<Client> GetClients()
        {
            List<Client> clients = new List<Client>();
            clients.Add(new Client {
                ClientId="clientId",
                AllowedGrantTypes=GrantTypes.ClientCredentials,
                ClientSecrets = {
                    //SHA 安全散列算法 是一个密码散列函数家族，是FIPS所认证的安全散列算法。能计算出一个数字消息所对应到的，长度固定的字符串（又称消息摘要）的算法。且若输入的消息不同，它们对应到不同字符串的机率很高。
                    new Secret("123321".Sha256(),"用户秘钥")//秘钥
                },
                //AllowedScopes 定义当秘钥验证通过后允许访问的资源
                //多个使用 逗号隔开 如 AllowedScopes = { "API","API1","API1" }
                AllowedScopes = { "API" }
            });
            return clients;
        }
    }
}
