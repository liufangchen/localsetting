using Microsoft.Extensions.DependencyInjection;

namespace LocalSettingService.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLocalSetting(this IServiceCollection services)
        => services.AddSingleton<ILocalSetting, LocalSetting>();

    public static IServiceCollection AddLocalSetting(this IServiceCollection services,string appName)
        => services.AddSingleton<ILocalSetting>(sp => new LocalSetting(appName));

    public static IServiceCollection AddLocalSetting(this IServiceCollection services, string appName, string settingsType)
        => services.AddSingleton<ILocalSetting>(sp => new LocalSetting(appName,settingsType));
}