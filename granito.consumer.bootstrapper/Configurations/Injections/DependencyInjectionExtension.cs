using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using granito.bootstrapper.Configurations.Performace.Filters;
using granito.consumer.domain.Configuration.Service;
using granito.consumer.domain.Interface.Http;
using granito.consumer.domain.Interface.Juros;
using granito.consumer.domain.Service.Http;
using granito.consumer.domain.Service.Juros;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;


public static class DependencyInjectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        #region .::Set config host service

        var serviceConfig = new ServiceConfig();
        new ConfigureFromConfigurationOptions<ServiceConfig>(configuration.GetSection("ServiceConfig"))
            .Configure(serviceConfig);
        services.AddSingleton(serviceConfig);

        #endregion
        
        #region .:: Configuration filter performace

        services.AddTransient<PerformaceFilters>();
        services.AddMvc(options => options.Filters.AddService<PerformaceFilters>())
            .AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true)
            .SetCompatibilityVersion(CompatibilityVersion.Latest);
        #endregion


        #region .::Services
        services.AddScoped<IJurosService, JurosService>();
        #endregion
        
        #region .:: Polly HttpClient injection
        var timeout = TimeSpan.FromSeconds(50);
        var retry = TimeSpan.FromMilliseconds(600000);
        services.AddHttpClient<IWebRequestService, WebRequestService>()
            .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(2, _ => retry))
            .AddPolicyHandler(request => Policy.TimeoutAsync<HttpResponseMessage>(timeout))
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { ServerCertificateCustomValidationCallback = AcceptAllCertifications });
        #endregion

        return services;
    }  
    private static bool AcceptAllCertifications(HttpRequestMessage httpRequestMessage, X509Certificate2? x509Certificate2, X509Chain? chain,
        SslPolicyErrors sslPolicyErrors)
    {

        return true;
    } 
    
    
}