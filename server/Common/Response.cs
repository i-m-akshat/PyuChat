

public class Response<T>
{
    public bool isSuccess { get; set; }
    public T Data { get; set; }
    public string? Error { get; set; }
    public string? Message { get; set; }
    public Response(bool isSuccess, T Data, string? error, string? message)
    {
        this.isSuccess = isSuccess;
        this.Data = Data;
        this.Error = error;
        this.Message = message;
    }
    public static Response<T> Success(T Data, string? message = "") => new(true, Data, null, message);
    public static Response<T> Failure(string error) => new(false, default!, error, null);
}