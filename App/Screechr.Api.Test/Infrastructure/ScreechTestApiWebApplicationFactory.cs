using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Screechr.Core.Data.Entities;

namespace Screechr.Api.Test.Infrastructure
{
    public class ScreechTestApiWebApplicationFactory : WebApplicationFactory<Startup>
    {
    }
}
