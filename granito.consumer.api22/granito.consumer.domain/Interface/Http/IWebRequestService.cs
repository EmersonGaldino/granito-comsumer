using granito.consumer.domain.Enum;

namespace granito.consumer.domain.Interface.Http;

public interface IWebRequestService
{
    Task<T> RequestJsonSerialize<T>(
        string url,
        object jsonData,
        ETypeMethods method,
        string token = null,
        List<KeyValuePair<string, string>>? headers = null,
        IEnumerable<KeyValuePair<string, string>> parameters = null) where T : class;
}