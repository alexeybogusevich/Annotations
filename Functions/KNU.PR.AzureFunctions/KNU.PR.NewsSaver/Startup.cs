using KNU.PR.DbManager.Connections;
using KNU.PR.NewsManager.Constants;
using KNU.PR.NewsManager.Servcies.ApiHandler;
using KNU.PR.NewsManager.Servcies.DbSaver;
using KNU.PR.NewsManager.Servcies.EntityConverter;
using KNU.PR.NewsManager.Servcies.Filter;
using KNU.PR.NewsManager.Servcies.TagService;
using KNU.PR.NewsManager.Servcies.VectorModelBuilder;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(KNU.PR.NewsManager.Startup))]
namespace KNU.PR.NewsManager
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
            builder.Services.AddScoped<IFilter, StopWordsFilter>();
            builder.Services.AddScoped<IFilter, PorterStemmerFilter>();
            builder.Services.AddScoped<IVectorModelBuilder, VectorModelBuilder>();
        }
    }
}
