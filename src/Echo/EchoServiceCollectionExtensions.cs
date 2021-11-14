// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

using Kaylumah.CaptureLogsInUnitTests.Echo;

namespace Microsoft.Extensions.DependencyInjection;

public static class EchoServiceCollectionExtensions
{
    public static IServiceCollection AddEcho(this IServiceCollection services, IConfiguration configuration)
    {
        _ = configuration ?? throw new ArgumentNullException(nameof(configuration));
        services.AddTransient<IEchoService, EchoService>();
        return services;
    }
}
