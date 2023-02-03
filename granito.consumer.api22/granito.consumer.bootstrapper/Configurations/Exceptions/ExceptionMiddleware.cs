using Microsoft.AspNetCore.Http;


public class ExceptionMiddleware
{
    private readonly RequestDelegate next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        this.next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        BaseException ex;
        if (exception is BaseException baseException)
            ex = baseException;
        else
            ex = new BaseException(exception);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)ex.StatusCode;
        return context.Response.WriteAsync(ex.ToString());
    }
}