using CompanyAPI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Interfaces
{
    public interface ILinkedResource
    {
        public IDictionary<LinkedResourceType, object> Links { get; set; }
    }
}
