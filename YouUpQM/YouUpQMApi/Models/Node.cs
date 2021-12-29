using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using YouUpQMApi.Enums;

namespace YouUpQMApi.Models
{
    public class Node
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DestinationAddress { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DestinationCheckType CheckType { get; set; }

        public int CheckTimeout { get; set; }

        public int CheckAttempts { get; set; }

        public bool Enabled { get; set; }
    }
}
