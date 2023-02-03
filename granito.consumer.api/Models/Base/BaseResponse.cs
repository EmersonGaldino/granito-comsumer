
public abstract class BaseResponse
{
    protected BaseResponse(bool success)
    {
        Success = success;
    }

    public bool Success { get; private set; }
}