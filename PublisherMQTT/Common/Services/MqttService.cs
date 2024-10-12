using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;
using PublisherMQTT.Common.Models;
using PublisherMQTT.WaterMonitoring.Factories;
using System.Text;

namespace PublisherMQTT.Common.Services
{
    class MqttService
    {
        private readonly IMqttClient _mqttClient;
        private readonly MqttClientOptions _mqttOptions;

        public MqttService(
            string brokerAddress,
            int port,
            string username,
            string password,
            string clientId)
        {
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            // Настройки подключения
            _mqttOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(brokerAddress, port)//"m8.wqtt.ru", 18242
                .WithCredentials(username, password)//"u_AWC2CV", "TtkgrFkZ"
                .WithClientId(clientId)//"mrdivdiz@yandex.ru"
                .WithCleanSession()
                .Build();
        }

        public async Task ConnectAsync()
        {
            try
            {
                await _mqttClient.ConnectAsync(_mqttOptions, CancellationToken.None);
                Console.WriteLine("Подключено к брокеру.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка подключения: {ex.Message}");
                return;
            }
        }

        public async Task PublishDataAsync()
        {
            //Пример создания или обновления данных
            var factoryM = new FactoryMessage();
            var rnd = new Random();
            string jsonData = factoryM.GetPostMessage(1, rnd.Next(100), rnd.Next(100), rnd.Next(100), rnd.Next(100), rnd.Next(100), rnd.Next(100)); // Данные в формате JSON

            // Публикуем данные в топик
            var message = new MqttApplicationMessageBuilder()
                .WithTopic("devices/update")  // Указываем топик, куда отправляем данные
                .WithPayload(jsonData)  // Данные (в данном случае JSON)
                .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)  // Уровень QoS
                .WithRetainFlag()
                .Build();

            await _mqttClient.PublishAsync(message);
            Console.WriteLine("Сообщение отправлено в топик.");
        }

        public async Task SubscribeAndReceiveAsync(string topic)
        {
            // Подписываемся на указанный топик
            await _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topic).Build(), CancellationToken.None);
            Console.WriteLine($"Подписка на топик: {topic}");

            // Обработчик входящих сообщений
            _mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                string receivedTopic = e.ApplicationMessage.Topic;
                string payload = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment);

                Console.WriteLine($"Сообщение получено из топика: {receivedTopic}");
                Console.WriteLine($"Содержимое сообщения: {payload}");

                return Task.CompletedTask;
            };
        }



        public async Task Disconect()
        {
            await _mqttClient.DisconnectAsync();
            Console.WriteLine("Отключен от брокера");
        }
    }
}
