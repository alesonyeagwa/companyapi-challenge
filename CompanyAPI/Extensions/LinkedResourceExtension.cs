using CompanyAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Extensions
{
    public static class LinkedResourceExtension
    {
        public static void AddResourceLink(this ILinkedResource resources, LinkedResourceType resourceType, string routeUrl)
        {
            resources.Links ??= new Dictionary<LinkedResourceType, object>();
            resources.Links[resourceType] = routeUrl;
        }

        public static void SetLinksResourceLinks(this ILinkedResource resources, List<object> links)
        {
            resources.Links ??= new Dictionary<LinkedResourceType, object>();
            resources.Links[LinkedResourceType.All] = links;
        }
    }

    public record LinkedResource(object url);

    public enum LinkedResourceType
    {
        None,
        Prev,
        Next,
        All
    }
}
