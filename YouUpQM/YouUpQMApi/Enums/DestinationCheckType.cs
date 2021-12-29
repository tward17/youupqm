using System.Runtime.Serialization;

namespace YouUpQMApi.Enums
{
    public enum DestinationCheckType
    {
        [EnumMember(Value = "Ping")]
        Ping,
        [EnumMember(Value = "Http")]
        Http
    }
}
