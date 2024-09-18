using MQTTnet;
using MQTTnet.Client;
using PublisherMQTT;
using System;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var factory = new MqttFactory();
        var mqttClient = factory.CreateMqttClient();

        // Настройки подключения
        var mqttOptions = new MqttClientOptionsBuilder()
            .WithTcpServer("m8.wqtt.ru", 18242)  // Адрес и порт брокера
            .WithCredentials("u_AWC2CV", "TtkgrFkZ")  // Логин и пароль
            .WithClientId("mrdivdiz@yandex.ru")  // Уникальный ID клиента
            .WithCleanSession()
            .Build();

        // Подключаемся к брокеру
        try
        {
            await mqttClient.ConnectAsync(mqttOptions, System.Threading.CancellationToken.None);
            Console.WriteLine("Подключено к брокеру.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка подключения: {ex.Message}");
            return;
        }

        ////Пример создания или обновления данных
        //var factoryM = new FactoryMessage();
        //var rnd = new Random();
        //string jsonData = factoryM.GetPostMessage(1, rnd.Next(100), rnd.Next(100), rnd.Next(100), rnd.Next(100), rnd.Next(100), rnd.Next(100), rnd.Next(100)); // Данные в формате JSON

        //// Публикуем данные в топик
        //var message = new MqttApplicationMessageBuilder()
        //    .WithTopic("devices/update")  // Указываем топик, куда отправляем данные
        //    .WithPayload(jsonData)  // Данные (в данном случае JSON)
        //    .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)  // Уровень QoS
        //    .WithRetainFlag()
        //    .Build();

        //await mqttClient.PublishAsync(message);
        //Console.WriteLine("Сообщение отправлено в топик.");

        //Настройка обработчика входящих сообщений
        mqttClient.ApplicationMessageReceivedAsync += e =>
        {
            Console.WriteLine("Получено сообщение:");
            Console.WriteLine($"Топик: {e.ApplicationMessage.Topic}");
            Console.WriteLine($"Сообщение: {Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment)}");

            return Task.CompletedTask;
        };

        // Подписываемся на топик
        await mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic("devices/update").Build());
        Console.WriteLine("Подписка на топик выполнена.");

        // Ожидание для получения сообщений
        Console.ReadLine();
        
        // Закрываем соединение
        await mqttClient.DisconnectAsync();
    }
}