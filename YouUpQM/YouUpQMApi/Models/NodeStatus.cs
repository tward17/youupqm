using System.Net;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using YouUpQMApi.Enums;

namespace YouUpQMApi.Models
{
    [DataContract]
    public class NodeStatus
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public UpSatus Status { get; set; }

        public int? HttpStatusCode { get; set; }

        public long ResponseTime { get; set; }

        public int AttemptsMade { get; set; }
    }
}
