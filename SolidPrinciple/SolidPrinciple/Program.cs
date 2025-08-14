// Interfaces
public interface IOrderTypeHandler
{
    void HandleOrder(double amount);
}

public interface IDatabaseSaver
{
    void Save();
}

public interface IEmailSender
{
    void SendEmail();
}

// Implementations
public class OnlineOrderHandler : IOrderTypeHandler
{
    public void HandleOrder(double amount)
    {
        Console.WriteLine("Online order placed.");
    }
}

public class StoreOrderHandler : IOrderTypeHandler
{
    public void HandleOrder(double amount)
    {
        Console.WriteLine("Store order placed.");
    }
}

public class DatabaseSaver : IDatabaseSaver
{
    public void Save()
    {
        Console.WriteLine("Order saved to database.");
    }
}

public class EmailSender : IEmailSender
{
    public void SendEmail()
    {
        Console.WriteLine("Email sent to customer.");
    }
}

// Order Processor
public class OrderProcessor
{
    private readonly IOrderTypeHandler _orderHandler;
    private readonly IDatabaseSaver _dbSaver;
    private readonly IEmailSender _emailSender;

    public OrderProcessor(IOrderTypeHandler orderHandler, IDatabaseSaver dbSaver, IEmailSender emailSender)
    {
        _orderHandler = orderHandler;
        _dbSaver = dbSaver;
        _emailSender = emailSender;
    }

    public void ProcessOrder(double amount)
    {
        Console.WriteLine("Order processing started...");
        _orderHandler.HandleOrder(amount);
        _dbSaver.Save();
        _emailSender.SendEmail();
    }
}
class Program
{
    static void Main()
    {
        IOrderTypeHandler orderHandler = new OnlineOrderHandler(); // or new StoreOrderHandler()
        IDatabaseSaver dbSaver = new DatabaseSaver();
        IEmailSender emailSender = new EmailSender();

        var processor = new OrderProcessor(orderHandler, dbSaver, emailSender);
        processor.ProcessOrder(100.0);
    }
}

