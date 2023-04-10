using System.Globalization;
using System.Runtime.CompilerServices;
using granito.consumer.domain.Configuration.Service;
using granito.consumer.domain.Entity;
using granito.consumer.domain.Enum;
using granito.consumer.domain.Interface.Http;
using granito.consumer.domain.Interface.Juros;

namespace granito.consumer.domain.Service.Juros;

public class JurosService : IJurosService
{
    private readonly IWebRequestService webRequestService;
    private readonly ServiceConfig config;

    public JurosService(IWebRequestService webRequestService, ServiceConfig config)
    {
        this.webRequestService = webRequestService;
        this.config = config;
    }

    public async Task<JurosEntity> Post(JurosEntity model)
    {
        var juros = await CallServiceFees();
       

        return Calculate(model, juros.data.value);
    }

    public JurosEntity Calculate(JurosEntity model, int juros)
    {
        var taxa = Convert.ToDouble(juros) / 100;
        var total = (model.ValueInitial * Math.Pow((1 + taxa), model.Months) / model.Months) * model.Months;
        var result = total.ToString("N2");
        return new JurosEntity
        {
            ValuePortion = Convert.ToDecimal(result),
            ValueTotal =  Convert.ToDecimal((model.Months * total).ToString("N2")),
            ValueInitial = model.ValueInitial,
            Months = model.Months
        };
    }

    private async Task<JurosResponse> CallServiceFees() =>
        await webRequestService.RequestJsonSerialize<JurosResponse>($"{config.Host}/Fees", null,
            ETypeMethods.GET, await CallServiceToToken());


    private async Task<string> CallServiceToToken()
    {
        var response = await webRequestService.RequestJsonSerialize<TokenResponse>($"{config.Host}/Auth", new
        {
            email = config.User,
            password = config.Password
        }, ETypeMethods.POST);
        return response.data.token;
    }

    
}