using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NLog.Extensions.Logging;

namespace Bim
{
    public class Startup
    {
        public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services
              .AddEndpointsApiExplorer()
              .AddLogging(x =>
              {
                  x.ClearProviders();
                  x.SetMinimumLevel(LogLevel.Trace);
                  x.AddNLog();
              });


            services
                .AddControllers()
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy { ProcessDictionaryKeys = false }
                    };
                    opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    opt.SerializerSettings.Converters.Add(new StringEnumConverter());
                });


            services.AddSwaggerGen(opt =>
             {
                 opt.UseAllOfForInheritance();
             })
                 .AddSwaggerGenNewtonsoftSupport();


            services.AddCors(opt =>
             {
                 opt.AddPolicy("CorsPolicy", corsBuilder => corsBuilder
                 .AllowAnyHeader()
                 .AllowAnyMethod()
                 .AllowCredentials()
                 .WithOrigins(configuration.GetSection("CorsOrigins").Get<string[]>()));
             });

        }
    }
}
