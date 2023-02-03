using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace granito.bootstrapper.Configurations.Performace.Filters;

public class PerformaceFilters : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var stop = new Stopwatch();

        stop.Start();

        var resultContext = await next().ConfigureAwait(false);

        stop.Stop();

        if (resultContext.Result is ObjectResult view)
        {
            var item = view.Value;

            if (item.GetType().GetProperty("TimerProcessing") != null)
                item.GetType().GetProperty("TimerProcessing")?.SetValue(item, Convert.ToInt32(stop.Elapsed.TotalMilliseconds));
            view.Value = item;
        }
    }
}