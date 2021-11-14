// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using FluentAssertions;
using Kaylumah.CaptureLogsInUnitTests.Echo;
using Xunit;

namespace Test.Unit;

public class UnitTest1
{
    [Fact]
    public async Task Test_DependencyInjection_EmptyLoggingBuilder()
    {
        var configuration = new ConfigurationBuilder().Build();
        var serviceProvider = new ServiceCollection()
            .AddLogging() // could also be part of AddEcho to make sure ILogger is available outside ASP.NET runtime
            .AddEcho(configuration)
            .BuildServiceProvider();
        var sut = serviceProvider.GetRequiredService<IEchoService>();
        var testInput = "Scenario: empty logging builder";
        var testResult = await sut.Echo(testInput).ConfigureAwait(false);
        testResult.Should().Be(testInput, "the input should have been returned");
    }

    [Fact]
    public async Task Test_DependencyInjection_ConsoleLoggingBuilder()
    {
        var configuration = new ConfigurationBuilder().Build();
        var serviceProvider = new ServiceCollection()
            .AddLogging(loggingBuilder => {
                loggingBuilder.AddConsole();
            })
            .AddEcho(configuration)
            .BuildServiceProvider();
        var sut = serviceProvider.GetRequiredService<IEchoService>();
        var testInput = "Scenario: console logging builder";
        var testResult = await sut.Echo(testInput).ConfigureAwait(false);
        testResult.Should().Be(testInput, "the input should have been returned");
    }
}
