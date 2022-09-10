using System.Reflection;

namespace UltimoLeague.Minimal.WebAPI.Mapping
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton(config);
            services.AddScoped<MapsterMapper.IMapper, MapsterMapper.ServiceMapper>();
            return services;
        }
    }
}
