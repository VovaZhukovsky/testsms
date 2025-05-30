using TestSMSHttpClientLibrary.Interface;

namespace TestSMSHttpClientLibrary.Model;
public class DishRequest : IRequest<DishParams>
{
    public string Command { get; set; }
    public DishParams CommandParameters { get; set; } = new();
}

public class DishParams
{
    public bool WithPrice { get; set; } = true;
}

public class OrderRequest: IRequest<OrderParams>
{
    public string Command { get; set; }
    public OrderParams CommandParameters { get; set; } = new();
}

public class OrderParams
{
    public string OrderId { get; set; } = "62137983-1117-4D10-87C1-EF40A4348250";
    public List<Order> MenuItems { get; set; } = new();
}

