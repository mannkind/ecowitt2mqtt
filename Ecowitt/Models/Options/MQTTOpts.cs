using TwoMQTT.Models;

namespace Ecowitt.Models.Options;

/// <summary>
/// The sink options
/// </summary>
public record MQTTOpts : MQTTManagerOptions
{
    public const string Section = "Ecowitt:MQTT";
    public const string TopicPrefixDefault = "home/ecowitt";
    public const string DiscoveryNameDefault = "ecowitt";
}
