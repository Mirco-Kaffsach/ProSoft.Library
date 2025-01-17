using System.Diagnostics.CodeAnalysis;
using ProSoft.Library.Data.Contracts;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ProSoft.Library.Data.Postgres.Engines;

namespace ProSoft.Library.Api.UnitTests;

[ExcludeFromCodeCoverage]
public class ProgramTests
{
    [Theory]
    [InlineData(typeof(ITagEngine), typeof(TagEngine))]
    public void CheckDependencyInjection(Type requestedInterface, Type expectedImplementation)
    {
        using (var app = new WebApplicationFactory<Program>())
        using (var scope = app.Services.CreateScope())
        {
            var returnedImplementation = scope.ServiceProvider.GetService(requestedInterface);

            Assert.NotNull(returnedImplementation);
            Assert.IsType(expectedImplementation, returnedImplementation);
        }
    }
}
