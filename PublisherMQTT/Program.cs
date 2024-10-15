using PublisherMQTT.Common.Services;
using Microsoft.Extensions.Configuration;
using PublisherMQTT.Common.Models;
using PublisherMQTT.WaterMonitoring.Enums;
class Program
{
    static async Task Main(string[] args)
    {
        var data = JsonService.LoadFromJsonFile<PublisherMQTT.WaterMonitoring.Models.MonitoringParameters>("MonitoringData.json");
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


        for (int i = 0; i < 10; i++) { }


        ////await mqttClient.PublishDataAsync();
        //await mqttClient.Disconect();
    }
}