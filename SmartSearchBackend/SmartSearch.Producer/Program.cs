// Должен находиться на стороне Главного сервиса, от которого поступают сообщения

using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

public class MessageProducer
{
    private readonly string _hostName = "localhost"; // имя хоста RabbitMQ
    private readonly int _port = 5672;              // порт RabbitMQ

    public void SendMessage<T>(string exchangeName, string routingKey, T message)
    {
        //Соединение с RabbitMQ
        var factory = new ConnectionFactory
        {
            HostName = _hostName,
            Port = _port
        };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            //Объявляем обмен и его тип
            channel.ExchangeDeclare(exchange: exchangeName, ExchangeType.Topic);

            //Преобразуем сообщение
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            //Добавляем данные в сообщение
            channel.BasicPublish(exchange: exchangeName,
                                 routingKey: routingKey,
                                 basicProperties: null,
                                 body: body);

            Console.WriteLine($" [x] Sent {message}");
        }
    }
}

/*

// Пример DTO
public class Student
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public string Age { get; set; }
}


// Пример использования Producer'а
public static class Program
{
    public static void Main()
    {
        var producer = new MessageProducer();
        var exchangeName = "student_exchange";

        // Пример добавления студента
        var newStudent = new Student
        {
            StudentId = 10,
            Name = "Петров",
            Age = "21"
        };
        producer.SendMessage(exchangeName, "add_student", newStudent);

        // Пример обновления студента
        var updateStudent = new Student
        {
            StudentId = 10,
            Name = "Петров",
            Age = "22"
        };
        producer.SendMessage(exchangeName, "update_student", updateStudent);

        // Пример удаления студента
        var deleteStudent = new { StudentId = 10 };
        producer.SendMessage(exchangeName, "delete_student", deleteStudent);
    }
}

*/