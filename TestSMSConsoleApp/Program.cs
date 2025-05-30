using TestSMSHttpClientLibrary;
using TestSMSHttpClientLibrary.Model;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using static System.Console;
using Serilog;
using Microsoft.Extensions.Configuration;
using TestSMSConsoleApp;
using System.IO;
using System;
using Npgsql;
using Microsoft.EntityFrameworkCore;

var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
            
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

using var httpClient = new HttpClient();
var byteArray = Encoding.ASCII.GetBytes($"{configuration["HttpClientUserName"]}:{configuration["HttpClientUserPassword"]}");
httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
httpClient.BaseAddress = new Uri(configuration["HttpClientBaseAddress"]);

SMSHttpClient smsHttpClient = new(httpClient);

var dishResponse = smsHttpClient.GetMenu(new DishRequest() { Command = "GetMenu"});

if (!dishResponse.Success)
{
    Log.Error(dishResponse.ErrorMessage);
    return;
}

using (ApplicationContext db = new ApplicationContext(configuration["DBConnectrionString"]))
{

    foreach (var item in dishResponse.Data.MenuItems)
    {
        db.Dishes.Add(item);
        Log.Information($"{item.Name}-{item.Article}-{item.Price}");
    }
    try
    {
        db.SaveChanges();
    }
    catch (DbUpdateException ex)
    {
        Log.Error(ex.Message);
    }
}

OrderRequest orderRequest = new(){Command = "SendOrder"};

while (true)
{
    Write("Сделайте заказ: ");
    try
    {
        string dishes = ReadLine();
        foreach (var dish in dishes.Split(";"))
        {
            var dishInfo = dish.Split(":");
            Int32.Parse(dishInfo[0]);

            if (Int32.Parse(dishInfo[1]) <= 0)
                throw new Exception();

            orderRequest.CommandParameters.MenuItems.Add(new Order { Id = dishInfo[0].ToString(), Quantity = dishInfo[1].ToString() });
        }
        break;
    }
    catch
    {
        Log.Error("Заказ неверный");
    }
}

var orderResponse = smsHttpClient.SendOrder(orderRequest);

if (orderResponse.Success)
{
    Log.Information("УСПЕХ");
}
else
{
    Log.Error(orderResponse.ErrorMessage);
}



