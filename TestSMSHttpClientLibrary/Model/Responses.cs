namespace TestSMSHttpClientLibrary.Model;

public class OrderResponse
{
    public string Command { get; set; }
    public bool Success { get; set; } = true;
    public string ErrorMessage { get; set; } = System.String.Empty;
}

public class DishResponse : OrderResponse
{
    public Data Data { get; set; } = new();
}

public class Data
{
    public List<Dish> MenuItems { get; set; } = new List<Dish>
    {
        new Dish{
            Id = "5979224",
            Article = "A1004292",
            Name = "Каша гречневая",
            Price = 50,
            IsWeighted = false,
            FullPath = "ПРОИЗВОДСТВО\\Гарниры",
            Barcodes = new List<string>{"57890975627974236429"}
        },
        new Dish{
            Id = "9084246",
            Article = "A1004293",
            Name = "Конфеты Коровка",
            Price = 300,
            IsWeighted = true,
            FullPath = "ДЕСЕРТЫ\\Развес",
            Barcodes = new List<string>()
        }        
    };
}
