using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using System;
using System.Net.Http;
using Anc.Certification.Api.Automation.Tests.Models;

public class HttpClientUtil
{
    private static IServiceProvider serviceProvider { get; set; }

    public static void Initial(IServiceProvider Provider, PollySetting pollySetting)
    {
        if (Provider == null)
        {
            IHostBuilder builder = Host.CreateDefaultBuilder();
            builder.ConfigureServices(services =>
            {
                services.AddHttpClient();
                var jitterer = new Random();
                pollySetting = new PollySetting();
                // Add resilient http client
                services.AddHttpClient("PollyClient")
                        .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(pollySetting.MaxRetries, retryAttempt =>
                                                                TimeSpan.FromMilliseconds(pollySetting.InitialWait * retryAttempt) +
                                                                TimeSpan.FromMilliseconds(jitterer.Next(0, pollySetting.MaxJitter))));
            });
            serviceProvider = builder.Build().Services;
        }
        else
        {
            serviceProvider = Provider;
        }
    }

    public static IHttpClientFactory GetHttpClientFactory(PollySetting pollySetting)
    {
        if (serviceProvider == null)
        {
            Initial(serviceProvider, pollySetting);
        }
        return serviceProvider.GetService<IHttpClientFactory>();
    }

}