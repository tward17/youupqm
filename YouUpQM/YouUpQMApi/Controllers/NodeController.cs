using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text.Json;
using YouUpQMApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YouUpQMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NodeController : ControllerBase
    {
        private readonly NodeService _nodeService;

        public NodeController()
        {
            _nodeService = new NodeService();
        }

        // GET: api/<NodeController>
        [HttpGet]
        public IEnumerable<Node> Get()
        {
            return _nodeService.GetNodes();
        }

        // GET api/<NodeController>/5
        [HttpGet("{id}")]
        public Node Get(int id)
        {
            var node = _nodeService.GetNode(id);

            return node;
        }

        // GET api/<NodeController>/5/status
        [HttpGet("{id}/status")]
        public NodeStatusResponse Status(int id)
        {
            var node = _nodeService.GetNode(id);

            if (node == null)
                return null;

            var status = new NodeStatus();

            switch (node.CheckType)
            {
                case Enums.DestinationCheckType.Ping:
                    status = _nodeService.PerformPingCheck(node);
                    break;
                case Enums.DestinationCheckType.Http:
                    status = _nodeService.PerformHttpCheck(node);
                    break;
            }

            return new NodeStatusResponse { Node = node, Status = status };
        }
    }
}
