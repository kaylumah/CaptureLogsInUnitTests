// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Test.Utilities.Logging.Xunit;
using Xunit.Abstractions;

namespace Microsoft.Extensions.Logging;

public static class XunitLoggingBuilderExtensions
{
    public static ILoggingBuilder AddXunit(this ILoggingBuilder builder, ITestOutputHelper testOutputHelper)
    {
        builder.AddConfiguration();

        builder.Services.TryAddSingleton(testOutputHelper);

        builder.Services.TryAddEnumerable(
            ServiceDescriptor.Singleton<ILoggerProvider, XunitLoggerProvider>());

        LoggerProviderOptions.RegisterProviderOptions
            <XunitLoggerConfiguration, XunitLoggerProvider>(builder.Services);

        return builder;
    }

    public static ILoggingBuilder AddXunit(this ILoggingBuilder builder, ITestOutputHelper testOutputHelper, Action<XunitLoggerConfiguration> configure)
    {
        builder.AddXunit(testOutputHelper);
        builder.Services.Configure(configure);

        return builder;
    }
}
