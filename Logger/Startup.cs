using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;

namespace Logger
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddDebug();
                builder.AddConsole();
                builder.AddEventSourceLogger();
                builder.AddEventLog();

                builder.AddFilter("Console", LogLevel.Information)
                        .AddFilter<EventLogLoggerProvider>("Microsoft", LogLevel.Warning);
            });

            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));
            var logger = loggerFactory.CreateLogger("FileLogger");


            app.Run(async context =>
            {
                logger.LogInformation($"LogInformation {context.Request.Path}");
                logger.LogDebug($"LogDebug {context.Request.Path}");
                logger.LogTrace($"LogTrace {context.Request.Path}");
                logger.LogCritical($"LogCritical {context.Request.Path}");
                logger.LogError($"LogError {context.Request.Path}");

                await context.Response.WriteAsync("Hello world");
            });
        }
    }
}
