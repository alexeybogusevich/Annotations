using KNU.PR.DbManager.Connections;
using KNU.PR.NewsSaver.Constants;
using KNU.PR.NewsSaver.Servcies.ApiHandler;
using KNU.PR.NewsSaver.Servcies.DbSaver;
using KNU.PR.NewsSaver.Servcies.EntityConverter;
using KNU.PR.NewsSaver.Servcies.TagService;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(KNU.PR.NewsSaver.Startup))]
namespace KNU.PR.NewsSaver
{
    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var connectionString = Environment.GetEnvironmentVariable(EnvironmentVariablesConstants.DbConnection,
                    EnvironmentVariableTarget.Process);
            builder.Services.AddDbContext<AzureSqlDbContext>(options => options.UseSqlServer(connectionString)
                    , ServiceLifetime.Scoped);

            builder.Services.AddHttpClient<IApiHandler, ApiHandler>();

            builder.Services.AddScoped<IDbSaver, DbSaver>();
            builder.Services.AddScoped<ITagService, TagService>();
            builder.Services.AddScoped<IApiHandler, ApiHandler>();
            builder.Services.AddScoped<IEntityConverter, EntityConverter>();
        }
    }
}
