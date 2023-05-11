namespace Orders.Host.Models.Response;

public class GetItemResponse<T>
{
    public T Data { get; set; } = default(T) !;
}