using granito.consumer.domain.Entity;

namespace granito.consumer.domain.Interface.Juros;

public interface IJurosService
{
    Task<JurosEntity> Post(JurosEntity map);
}