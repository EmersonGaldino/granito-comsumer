using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

public abstract class ApiBaseController : ControllerBase
{
    protected TService GetService<TService>() => (TService)HttpContext.RequestServices.GetService(typeof(TService));
    protected IActionResult Success(object data = null) => Ok(new SuccessResponse<object>(data));
    protected IActionResult Error(string message) => BadRequest(new BadResponse(message) );

    protected IActionResult NotAuthorization() => BadRequest(new BadResponse("Your profile is not qualified to use this method."));
    public string? ApplicationName => User.Identity.Name;

    public string? ApplicationId => User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

    private readonly ICollection<KeyValuePair<string, int>> errors = new List<KeyValuePair<string, int>>();

    private void AddErrors(string message, int statusCode)
    {
        errors.Add(new KeyValuePair<string, int>(message, statusCode));
    }
    protected async Task<IActionResult> AutoResult<T>(Func<Task<T>> func)
    {
        try
        {
            return Ok(await func().ConfigureAwait(false));
        }
        catch (ValidationException valid)
        {
            var erroBuilder = new StringBuilder();
            AddErrors(erroBuilder.ToString(), 400);
            return BadRequest(erroBuilder.ToString());
        }
        catch (WebException ex)
        {
            AddErrors(ex.Message, (int)ex.Status);
            return BadRequest(new
            {
                errors
            });
        }
        catch (RequestException e)
        {
            AddErrors(e.Message, 500);
            NotAuthorization();
            return BadRequest(new
            {
                errors
            });
        }
    }
}