using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text.Json;
using YouUpQMApi.Models;

namespace YouUpQMApi
{
    public class NodeService
    {
        public NodeStatus PerformPingCheck(Node node)
        {
            var status = new NodeStatus();

            Ping ping = new Ping();
            PingReply pingReply = null;

            for (int i = 0; i < node.CheckAttempts; i++)
            {
                pingReply = ping.Send(node.DestinationAddress, node.CheckTimeout);

                status.AttemptsMade = i + 1;

                if (pingReply.Status == IPStatus.Success)
                {
                    status.Status = Enums.UpSatus.Up;
                    status.ResponseTime = pingReply.RoundtripTime;
                    break;
                }
                else
                {
                    status.Status = Enums.UpSatus.Down;
                    status.ResponseTime = Math.Max(status.ResponseTime, pingReply.RoundtripTime);
                }
            }

            return status;
        }

        public NodeStatus PerformHttpCheck(Node node)
        {
            var status = new NodeStatus();

            var httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromMilliseconds(node.CheckTimeout)
            };

            var sw = new Stopwatch();

            for (int i = 0; i < node.CheckAttempts; i++)
            {
                status.AttemptsMade = i + 1;
                try
                {
                    sw.Start();
                    var response = Task.Run(async () => await httpClient.GetAsync(new Uri(node.DestinationAddress))).Result;
                    sw.Stop();

                    if (response.IsSuccessStatusCode)
                    {
                        status.Status = Enums.UpSatus.Up;
                        status.HttpStatusCode = Convert.ToInt32(response.StatusCode);
                        status.ResponseTime = sw.ElapsedMilliseconds;
                        break;
                    }
                    else
                    {
                        status.Status = Enums.UpSatus.Down;
                        status.HttpStatusCode = Convert.ToInt32(response.StatusCode);
                        status.ResponseTime = sw.ElapsedMilliseconds;
                        Thread.Sleep(500);
                    }
                }
                catch (Exception ex)
                {
                    status.Status = Enums.UpSatus.Down;
                }

                sw.Reset();
            }

            return status;
        }

        public List<Node> GetNodes()
        {
            var nodeConfig = System.IO.File.ReadAllText("config/nodesConfig.json");

            if (string.IsNullOrWhiteSpace(nodeConfig))
                throw new Exception("No config file found");

            var nodes = JsonSerializer.Deserialize<List<Node>>(nodeConfig);

            if (nodes == null || !nodes.Any())
                throw new Exception("Invalid nodes config");

            if (nodes.Count() != nodes.GroupBy(x => x.Id).Count())
                throw new Exception("Same Id used multiple times in config");

            return nodes;
        }

        public Node GetNode(int id)
        {
            var nodes = GetNodes();

            var node = nodes.FirstOrDefault(x => x.Id == id);

            return node;
        }
    }
}
