using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;
using PublisherMQTT.WaterMonitoring.Factories;

namespace PublisherMQTT.Common.Services
{
    class MqttService
    {
        private readonly IMqttClient mqttClient;
        private readonly MqttClientOptions mqttOptions;

        public MqttService()
        {
            var factory = new MqttFactory();
            mqttClient = factory.CreateMqttClient();

            // Настройки подключения
            mqttOptions = new MqttClientOptionsBuilder()
                .WithTcpServer("m8.wqtt.ru", 18242)
                .WithCredentials("u_AWC2CV", "TtkgrFkZ")
                .WithClientId("mrdivdiz@yandex.ru")
                .WithCleanSession()
                .Build();
        }

        public async Task ConnectAsync()
        {
            try
            {
                await mqttClient.ConnectAsync(mqttOptions, CancellationToken.None);
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

            await mqttClient.PublishAsync(message);
            Console.WriteLine("Сообщение отправлено в топик.");

            await mqttClient.DisconnectAsync();
        }
    }
}
