namespace TestSMSHttpClientLibrary.Interface;
public interface IRequest<T>
{
    public string Command { get; set; }
    public T CommandParameters { get; set; }
}