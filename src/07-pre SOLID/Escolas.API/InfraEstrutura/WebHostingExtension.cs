using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Data.SqlClient;

namespace Escolas.API.InfraEstrutura
{
    public static class WebHostingExtension
    {
        public static IHost ExecutarMigracoesDbContext<TContext>(this IHost webHost) where TContext : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();
                try
                {
                    logger.LogInformation("Migrando database para contexto {DbContextName}", typeof(TContext).Name);
                    var retries = 10;
                    var retry = Policy
                        .Handle<SqlException>()
                        .WaitAndRetry(
                            retryCount: retries,
                            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                            onRetry: (exception, timeSpan, retry, ctx) =>
                            {
                                logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", nameof(TContext), exception.GetType().Name, exception.Message, retry, retries);
                            });
                    retry.Execute(() => 
                    {
                        //context.Database.EnsureCreated();
                        context.Database.Migrate();
                    });
                    logger.LogInformation("Migração concluída para contexto {DbContextName}", typeof(TContext).Name);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Erro ao executar migração para contexto {DbContextName}", typeof(TContext).Name);
                }
            }
            return webHost;
        }

    }
}
