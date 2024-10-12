using PublisherMQTT.Common.Services;

class Program
{
    static async Task Main(string[] args)
    {
        var mqttClient = new MqttService();
        await mqttClient.ConnectAsync();
        await mqttClient.PublishDataAsync();
    }
}