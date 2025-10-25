namespace TareasApi.ConsoleTests;

public class Client
{
    public async Task<int> Get100()
    {
        Thread.Sleep(5000);
        return 100;
    }
    
    public async Task<int> Get500()
    {
        Thread.Sleep(3000);
        return 500;
    }
    
    public async Task<int> Get200()
    {
        Thread.Sleep(2000);
        return 200;
    }
}