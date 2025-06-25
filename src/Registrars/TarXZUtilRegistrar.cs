using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Compression.Tar.Registrars;
using Soenneker.Compression.Tar.XZ.Abstract;
using Soenneker.Compression.XZ.Registrars;
using Soenneker.Utils.Directory.Registrars;
using Soenneker.Utils.FileSync.Registrars;

namespace Soenneker.Compression.Tar.XZ.Registrars;

/// <summary>
/// A utility library dealing with Tar and XZ (tar.xz) extraction/archiving and (de)compression
/// </summary>
public static class TarXZUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="ITarXZUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddTarXZUtilAsSingleton(this IServiceCollection services)
    {
        services.AddDirectoryUtilAsSingleton()
                .AddFileUtilSyncAsSingleton()
                .AddXzUtilAsSingleton()
                .AddTarUtilAsSingleton()
                .TryAddSingleton<ITarXZUtil, TarXZUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="ITarXZUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddTarXZUtilAsScoped(this IServiceCollection services)
    {
        services.AddDirectoryUtilAsScoped().AddFileUtilSyncAsScoped().AddXzUtilAsScoped().AddTarUtilAsScoped().TryAddScoped<ITarXZUtil, TarXZUtil>();

        return services;
    }
}