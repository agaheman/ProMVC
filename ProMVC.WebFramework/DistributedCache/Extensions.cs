using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProMVC.WebFramework.DistributedCache
{
    public static class Extensions
    {
        private static readonly string RedisCacheSectionName = "redis";
        private static readonly string SqlServerCacheSectionName = "sqlserverCache";

        public static IServiceCollection AddRedisDistributedCache(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            services.Configure<RedisOptions>(configuration.GetSection(RedisCacheSectionName));
            var options = configuration.GetOptions<RedisOptions>(RedisCacheSectionName);
            services.AddDistributedRedisCache(o => 
            {
                o.Configuration = options.ConnectionString;
                o.InstanceName = options.Instance;
            });

            return services;
        }
        public static IServiceCollection AddSQLServerDistributedCache(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            services.Configure<SQLServerOptions>(configuration.GetSection(SqlServerCacheSectionName));
            var options = configuration.GetOptions<SQLServerOptions>(SqlServerCacheSectionName);
            services.AddDistributedSqlServerCache(o =>
            {
                o.ConnectionString = options.ConnectionString;
                o.SchemaName = options.SchemaName;
                o.TableName = options.TableName;
            });

            return services;
        }
    }
}