public class OperationResult<T> : IOperationResult<T>
{
    public bool IsSuccess { get; private set; }
    public IEnumerable<string> Errors { get; private set; }
    public T Data { get; private set; }

    private OperationResult(bool isSuccess, T data, IEnumerable<string> errors)
    {
        IsSuccess = isSuccess;
        Data = data;
        Errors = errors;
    }

    public static OperationResult<T> ReturnOk(T data)
    {
        return new OperationResult<T>(true, data, Enumerable.Empty<string>());
    }

    public static OperationResult<T> ReturnNotFound()
    {
        return new OperationResult<T>(false, default, new[] { "Not found" });
    }

    public static OperationResult<T> ReturnNoContent()
    {
        return new OperationResult<T>(true, default, Enumerable.Empty<string>());
    }

    public static OperationResult<T> ReturnBadRequest(IEnumerable<string> errors)
    {
        return new OperationResult<T>(false, default, errors);
    }
}
