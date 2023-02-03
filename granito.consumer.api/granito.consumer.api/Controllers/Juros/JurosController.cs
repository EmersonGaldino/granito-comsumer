using AutoMapper;
using granito.consumer.api.Models.ModelView;
using granito.consumer.api.Models.ViewModel;
using granito.consumer.domain.Entity;
using granito.consumer.domain.Interface.Juros;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace granito.consumer.api.Controllers.Juros;

[Route("api/[controller]")]
[ApiController]
public class JurosController : ApiBaseController
{
   private  IJurosService service => GetService<IJurosService>();
   private IMapper Mapper => GetService<IMapper>();
   
   [HttpPost]
   [SwaggerOperation(Summary = "Calculo",
      Description = "Calculo o juros de acordo com os messes passado.")]
   [SwaggerResponse(200, "Juros calculado.", typeof(SuccessResponse<BaseModelView<JurosModelView>>))]
   [SwaggerResponse(400, "Não foi possível calcular o juros.", typeof(BadResponse))]
   [SwaggerResponse(500, "Erro no rastreamento da pilha.", typeof(BadResponse))]
   public async Task<IActionResult> Get([FromBody] JurosViewModel model) => await AutoResult(async () => new BaseModelView<JurosModelView>
   {
      Data = Mapper.Map<JurosModelView>(await service.Post(Mapper.Map<JurosEntity>(model))),
      Message = "Calculo de juros efetuado com sucesso.",
      Success = true
   });
}