using System.Text;
using granito.bootstrapper.Configurations.Constants;
using granito.bootstrapper.Configurations.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace granito.bootstrapper.Configurations.Auth;

public static class AuthConfiguration
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        var signConfiguration = new SignConfigurationToken();
        services.AddSingleton(signConfiguration);

        var tokenConfigure = new TokenConfig();

        new ConfigureFromConfigurationOptions<TokenConfig>(
                configuration.GetSection(nameof(TokenConfig)))
            .Configure(tokenConfigure);

        services.AddSingleton(tokenConfigure);

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ValidateActor = true;
                paramsValidation.ValidateAudience = true;
                paramsValidation.ValidAudience = tokenConfigure.Audience;
                paramsValidation.ValidIssuer = tokenConfigure.Issuer;
                paramsValidation.ClockSkew = TimeSpan.Zero;

                if (tokenConfigure.SigningKey != null)
                    paramsValidation.IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigure.SigningKey));
            });

        services.AddAuthorization(auth =>
        {
            auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build());

            auth.AddPolicy("Granito", policy =>
            {
                policy.RequireAssertion(context =>
                    context.User.HasClaim(c => c.Type == Constant.ID));
            });
        });
        services.AddMemoryCache();
    }
}