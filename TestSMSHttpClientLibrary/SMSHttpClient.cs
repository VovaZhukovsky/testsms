using TestSMSHttpClientLibrary.Interface;
using TestSMSHttpClientLibrary.Model;
using System.Text;

namespace TestSMSHttpClientLibrary;

public class SMSHttpClient
{
    HttpClient _httpClient;
    public SMSHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public DishResponse GetMenu(DishRequest dishRequest)
    {
        string errorMessage  = GetErrorFromRequest(dishRequest, "GetMenu");
        var response = new DishResponse();

        if (errorMessage != System.String.Empty)
        {
            response.Success = false;
            response.ErrorMessage = errorMessage;
        }

        return response;
    }

    public OrderResponse SendOrder(OrderRequest orderRequest)
    {

        string errorMessage = GetErrorFromRequest(orderRequest, "SendOrder");
        var response = new OrderResponse();

        if (errorMessage != System.String.Empty)
        {
            response.Success = false;
            response.ErrorMessage = errorMessage;
        }

        return response;

    }

    private string GetErrorFromRequest<T>(IRequest<T> request, string Command)
    {
        string errorMessage = String.Empty;
        switch (true)
        {
            case true when Encoding.UTF8.GetString(Convert.FromBase64String(_httpClient.DefaultRequestHeaders.Authorization.Parameter)) != "admin:admin":
                errorMessage = "Login or password is wrong";
                break;
            case true when request.Command != Command:
                errorMessage = "Command request is wrong";
                break;
            case true when _httpClient.BaseAddress.ToString() != "http://example.com/":
                errorMessage = "Server not found";
                break;
            default:
                break;
        }

        return errorMessage;
    }

}