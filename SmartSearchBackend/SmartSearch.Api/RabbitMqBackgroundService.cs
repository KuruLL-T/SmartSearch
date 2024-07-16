//Инициализирует `MessageHandler`

public class RabbitMqBackgroundService : BackgroundService
{
    private readonly MessageHandler _messageHandler;

    public RabbitMqBackgroundService(MessageHandler messageHandler)
    {
        _messageHandler = messageHandler;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _messageHandler.StartListening();
        return Task.CompletedTask;
    }
}