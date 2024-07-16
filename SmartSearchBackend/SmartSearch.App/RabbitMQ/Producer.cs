using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace SmartSearch.App.RabbitMQ
{
    public class Producer
    {
        private readonly string _hostName = "localhost"; // имя хоста RabbitMQ
        private readonly int _port = 5672;              // порт RabbitMQ

        public void SendMessage<T>(string exchangeName, string routingKey, /*string queueName,*/ T message)
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
                channel.BasicPublish(exchange: exchangeName,//user or searchItem
                                     routingKey: routingKey,//add delete update
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine($" [x] Sent {message}");
            }
        }
    }
}
