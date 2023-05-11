namespace Orders.Host.Models.Requests;

public class RemoveItemRequest<T>
{
    public T Id { get; set; } = default(T) !;
}