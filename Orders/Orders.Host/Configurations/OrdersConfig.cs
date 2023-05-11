namespace Orders.Host.Configurations;

public class OrdersConfig
{
    public string CdnHost { get; set; } = null!;
    public string ImgUrl { get; set; } = null!;
    public string CatalogUrl { get; set; } = null!;
    public string ConnectionString { get; set; } = null!;
}