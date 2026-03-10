namespace Eventify.Backend.Common.Attributes;

using Microsoft.AspNetCore.Mvc;

[AttributeUsage(AttributeTargets.Class)]
public class ApiRouteAttribute : RouteAttribute
{
    private const string Prefix = "api/";
    public ApiRouteAttribute(string template) : base(Prefix + template.TrimStart('/')) { }
}
