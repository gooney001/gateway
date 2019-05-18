using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Model
{
    public class Permission
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Predicate { get; set; }
    }
}
