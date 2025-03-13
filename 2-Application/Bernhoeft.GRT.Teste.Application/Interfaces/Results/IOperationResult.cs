public interface IOperationResult<T>
{
    bool IsSuccess { get; }
    IEnumerable<string> Errors { get; }
    T Data { get; }
}
