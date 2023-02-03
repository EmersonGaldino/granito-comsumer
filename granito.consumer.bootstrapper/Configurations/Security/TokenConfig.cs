
namespace granito.bootstrapper.Configurations.Security;

public class TokenConfig
{
    public string? Issuer { get; set; } = new RouterEnvironments().GetEnvByName("Issuer") ;

    public string? Audience { get; set; } = new RouterEnvironments().GetEnvByName("Audience") ;
    public int ExpireIn { get; set; } = Convert.ToInt32(new RouterEnvironments().GetEnvByName("ExpireIn") );

    public string? SigningKey { get; set; } =
        new RouterEnvironments().GetEnvByName("SigningKey") ;
}