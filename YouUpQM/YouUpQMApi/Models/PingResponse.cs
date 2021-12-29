using System;
using System.Collections.Generic;
using System.Text;

namespace YouUpQMApi.Models
{
    public class PingResponse
    {
        public string Status { get; set; }
        public int AttemptCount { get; set; }
        public long ResponseTime { get; set; }
    }
}
