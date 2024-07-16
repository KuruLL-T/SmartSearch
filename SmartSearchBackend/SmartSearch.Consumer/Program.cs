using System;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Channels;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SmartSearch.Domain.SearchItemModel;
using SmartSearch.Domain.SearchItemTypeModel;
using SmartSearch.Domain.ServiceModel;
using SmartSearch.Domain.UserModel;
using SmartSearch.Infrastructure;
using SmartSearch.Infrastructure.UserModel;

public class MessageHandler
{
    private readonly string _hostName = "localhost";  //  имя хоста RabbitMQ
    private readonly int _port = 5672;               //  порт RabbitMQ
    private readonly IModel _channel;
    private readonly ISearchItemRepository _itemrepository;
    private readonly IServiceRepository _servicerepository;
    private readonly IUserRepository _userrepository;
    private readonly ISearchItemTypeRepository _typerepository;

    public MessageHandler(ISearchItemRepository itemrepository, IServiceRepository servicerepository, IUserRepository userrepository, ISearchItemTypeRepository typerepository)
    {
        //Соединение с RabbitMQ
        var factory = new ConnectionFactory
        {
            HostName = _hostName,
            Port = _port
        };
        var connection = factory.CreateConnection();
        _channel = connection.CreateModel();
        _itemrepository = itemrepository;
        _servicerepository = servicerepository;
        _userrepository = userrepository;
        _typerepository = typerepository;
    }

    //Открываем канал
    public void StartListening()
    {
        ConfigureQueueAndConsumer("searchitem", ProcessSearchItemMessageAsync);
        ConfigureQueueAndConsumer("service", ProcessServiceMessageAsync);
        ConfigureQueueAndConsumer("user", ProcessUserMessageAsync);
        ConfigureQueueAndConsumer("type", ProcessTypeMessageAsync);
        Console.WriteLine(" [*] Waiting for messages.");
    }

    //Принимает `routingPrefix`, для формирования ключей маршрутизации,
    //          `messageProcessor`, который указывает, какой метод будет использоваться для обработки сообщений.
    private void ConfigureQueueAndConsumer(string routingPrefix, Func<string, string, Task> messageProcessor)
    {
        //Объявляем обмен и его тип
        string exchangeName = "smartsearch_exchange";
        _channel.ExchangeDeclare(exchange: exchangeName,
                                type: ExchangeType.Topic);

        // Объявляем очередь и ее свойства
        var queueName = _channel.QueueDeclare().QueueName;

        var routingKeys = new string[] { $"add_{routingPrefix}", $"update_{routingPrefix}", $"delete_{routingPrefix}" };

        foreach (var routingKey in routingKeys)
        {
            _channel.QueueBind(queue: queueName,
                               exchange: exchangeName,
                               routingKey: routingKey);
        }

        _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

        // Подписываемся на сообщения от производителя
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            await messageProcessor(message, eventArgs.RoutingKey);
        };

        // Регистрируем потребителя для прослушивания определенной очереди
        _channel.BasicConsume(queue: queueName,
                              autoAck: true,
                              consumer: consumer);
    }

     //Обработчик для `Searchitem`
    private async Task ProcessSearchItemMessageAsync(string message, string routingKey)
    {
        try
        {
            //Конвертируем получаемое сообщение в DTO объект
            var data = ResponseRawContentParser.Parse(message);
            var tmp = await _typerepository.GetByGuidId(Guid.Parse(data["type_id"].ToString()));
            var searchItem = new SearchItem(tmp.Id,
                                            data["external_id"].ToObject<Guid>(),
                                            data["header"].ToString(),
                                            data["description"].ToString(),            
                                            data["image"].ToString(),
                                            data["link"].ToString(),
                                            JsonConvert.DeserializeObject<Dictionary<string, string>>(data["access_rights"].ToString()));        

            //Проверка на корректность 
            if (searchItem == null)
            {
                Console.WriteLine("Invalid message format");
                return;
            }

            //В зависимости от получаемого ключа(routingKey) выполняем add, update или delete
            switch (routingKey)
            {
                case "add_searchitem":
                    await _itemrepository.Add(searchItem);
                    break;
                case "update_searchitem":
                    await _itemrepository.Update(searchItem);
                    break;
                case "delete_searchitem":
                    await _itemrepository.Delete(searchItem);
                    break;
                default:
                    Console.WriteLine("Unknown operation");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing message: {ex.Message}");
        }
    }

    //Обработчик для `Service`
    private async Task ProcessServiceMessageAsync(string message, string routingKey)
    {
        try
        {
            //Конвертируем получаемое сообщение в DTO объект
            var data = ResponseRawContentParser.Parse(message);
            var service = new Service(data["name"].ToString(),
                                      data["service_id"].ToObject<Guid>());

            //Проверка на корректность 
            if (service == null)
            {
                Console.WriteLine("Invalid message format");
                return;
            }

            //В зависимости от получаемого ключа(routingKey) выполняем add, update или delete
            switch (routingKey)
            {
                case "add_service":
                    await _servicerepository.Add(service);
                    break;
                case "update_service":
                    await _servicerepository.Update(service);
                    break;
                case "delete_service":
                    await _servicerepository.Delete(service);
                    break;
                default:
                    Console.WriteLine("Unknown operation");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing message: {ex.Message}");
        }
    }

    //Обработчик для `User`
    private async Task ProcessUserMessageAsync(string message, string routingKey)
    {
        try
        {
            //Конвертируем получаемое сообщение в DTO объект
            var data = ResponseRawContentParser.Parse(message);
            var userType = data["userType"].ToObject<int>();
            var strId = data["externalId"].ToString();
            bool isStudent = false;
            if (userType == 0)
            {
                isStudent = true;
            }
            Guid? studentId = isStudent ? new Guid(strId) : null;
            Guid? personId = isStudent ? null : new Guid(strId);             
            var accessRights = JsonConvert.DeserializeObject<Dictionary<string, string>>(data["access_rights"].ToString());
            //var tmp1 = data["access_rights"].ToString();
            //var jsonRights1 = (JToken)JObject.Parse(tmp1);
            //var jsonRights = (JObject)jsonRights1;

            //Dictionary<string, string> accessRights = [];
            //foreach (var item in jsonRights)
            //{
            //    accessRights.Add(item.Key, item.Value.ToString());
            //}
            var user = new User(studentId, personId,
                accessRights);
            // var user = new User(data["student_id"].ToObject<Guid>(),
            //                    data["personal_id"].ToObject<Guid>(),
            var tmp = await _typerepository.GetByGuidId(Guid.Parse(data["typeId"].ToString()));
            var searchItem = new SearchItem(tmp.Id,
                                            data["externalId"].ToObject<Guid>(),
                                            data["header"].ToString(),
                                            data["description"].ToString(),
                                            data["image"].ToString(),
                                            data["link"].ToString(),
                                            accessRights);

            //Проверка на корректность 
            if (user == null)
            {
                Console.WriteLine("Invalid message format");
                return;
            }

            //В зависимости от получаемого ключа(routingKey) выполняем add, update или delete
            switch (routingKey)
            {
                case "add_user":
                    await _userrepository.Add(user);
                    await _itemrepository.Add(searchItem);
                    break;
                case "update_user":
                    await _userrepository.Update(user);
                    await _itemrepository.Update(searchItem);
                    break;
                case "delete_user":
                    await _userrepository.Delete(user);
                    await _itemrepository.Delete(searchItem);
                    break;
                default:
                    Console.WriteLine("Unknown operation");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing message: {ex.Message}");
        }
    }

    //Обработчик для `SearchItemType`
    private async Task ProcessTypeMessageAsync(string message, string routingKey)
    {
        try
        {
            //Конвертируем получаемое сообщение в DTO объект
            var data = ResponseRawContentParser.Parse(message);
            var tmp = await _servicerepository.GetByGuidId(Guid.Parse(data["service_id"].ToString()));
            var searchItemType = new SearchItemType(data["name"].ToString(),
                                                    tmp.Id,
                                                    Guid.Parse(data["type_id"].ToString()),
                                                    Guid.Parse(data["service_id"].ToString()),
                                                    data["priority"].ToObject<int>());

            //Проверка на корректность 
            if (searchItemType == null)
            {
                Console.WriteLine("Invalid message format");
                return;
            }

            //В зависимости от получаемого ключа(routingKey) выполняем add, update или delete
            switch (routingKey)
            {
                case "add_type":
                    await _typerepository.Add(searchItemType);
                    break;
                case "update_type":
                    await _typerepository.Update(searchItemType);
                    break;
                case "delete_type":
                    await _typerepository.Delete(searchItemType);
                    break;
                default:
                    Console.WriteLine("Unknown operation");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing message: {ex.Message}");
        }
    }
}

/* Комментарии к командам

_channel.BasicQos(prefetchSize: 0, 
                  prefetchCount: 1, 
                  global: false);
Отвечает за равномерное распределение сообщений, где:
                  prefetchSize - размер сообщения (0 = ∞);
                  prefetchCount - количество одновременных сообщений;
                  global - глобализация(true)-для всей системы, (false)-текущий экземпляр. 


_channel.QueueDeclare(queue: "queue_name",
                      durable: true,
                      exclusive: false,
                      autoDelete: false,
                      arguments: null);
Объявляет очередь, где:
                      queue - "имя_очереди";
                      durable - сохранять очередь, после перезапуска;
                      exclusive - один(true) или несколько(false) одновременных потребителей;
                      autoDelete - автоудаление очереди;
                      arguments - доп. параметры.



*/