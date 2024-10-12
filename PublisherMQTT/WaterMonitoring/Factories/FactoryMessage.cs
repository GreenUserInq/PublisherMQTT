using System.Text;
using PublisherMQTT.WaterMonitoring.Enums;
using PublisherMQTT.WaterMonitoring.Interfaces;

namespace PublisherMQTT.WaterMonitoring.Factories
{
    class FactoryMessage : IFactoryMessage
    {
        public string GetPostMessage(
            int Id,
            int Water_level = 0,
            int Degree_of_clogging = 0,
            int Structural_deformations = 0,
            int Ambient_temperature = 0,
            int Water_flow_rate = 0,
            int Humidity_inside_the_pipe = 0)
        {
            var builder = new StringBuilder();
            builder.Append($"{{\"id\":{Id}");
            builder.Append(appenedMessage(MonitoringParameters.Water_level, Water_level));
            builder.Append(appenedMessage(MonitoringParameters.Degree_of_clogging, Degree_of_clogging));
            builder.Append(appenedMessage(MonitoringParameters.Structural_deformations, Structural_deformations));
            builder.Append(appenedMessage(MonitoringParameters.Ambient_temperature, Ambient_temperature));
            builder.Append(appenedMessage(MonitoringParameters.Water_flow_rate, Water_flow_rate));
            builder.Append(appenedMessage(MonitoringParameters.Humidity_inside_the_pipe, Humidity_inside_the_pipe));
            builder.Append('}');

            return builder.ToString();
        }

        private string appenedMessage(MonitoringParameters parameter, int value)
        {
            return $",\"name\":\"{parameter.ToString()}\",\"value\":{value}";
        }
    }
}
