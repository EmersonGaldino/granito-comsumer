using granito.consumer.api.Models.ModelView;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


[Route("api/[controller]")]
[ApiController]
public class ShowMeTheCodeController : ApiBaseController
{
    [HttpGet("showmethecode")]
    [SwaggerOperation(Summary = "Show Me the code",
        Description = "Devolve os links para os repositorios.")]
    [SwaggerResponse(200, "Links encontrados com sucesso.", typeof(SuccessResponse<BaseModelView<List<ShowMeTheCodeModelView>>>))]
    [SwaggerResponse(400, "Não foi possível devolver os links.", typeof(BadResponse))]
    [SwaggerResponse(500, "Erro no rastreamento da pilha.", typeof(BadResponse))]
    public async Task<IActionResult> Get() => await AutoResult(async () => new BaseModelView<List<ShowMeTheCodeModelView>>
    {
        Data = new List<ShowMeTheCodeModelView>
        {
            new()
            {
                Link = "https://github.com/EmersonGaldino/granito-comsumer",
                Description = "Esse repositorio chama a api principal, apos resposta calcula o juros"
            },
            new()
            {
                Link = "https://github.com/EmersonGaldino/granito-producer",
                Description = "Esse repositorio gera token de acesso e devolve a taxa de juros para o consumer"
            }
        },
        Message = "Links encontrados com sucesso.",
        Success = true
    });
    
}