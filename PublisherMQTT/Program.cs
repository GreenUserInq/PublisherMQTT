using PublisherMQTT.Common.Services;
using Microsoft.Extensions.Configuration;
using PublisherMQTT.Common.Models;
class Program
{
    static async Task Main(string[] args)
    {
        var config = new MqttConfig();

        var mqttClient = new MqttService(
            config.BrokerAddress,
            config.Port,
            config.Username,
            config.Password,
            config.ClientId
            );

        await mqttClient.ConnectAsync();
        await mqttClient.SubscribeAndReceiveAsync("devices/update");
        await mqttClient.PublishDataAsync();
        await mqttClient.Disconect();
    }
}