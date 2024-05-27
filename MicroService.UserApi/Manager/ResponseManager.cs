using MicroService.Shared.Response;

namespace MicroService.UserApi.Manager
{
    public class ResponseManager
    {
        private readonly NodeManager _nodeManager;
        public ResponseManager(NodeManager nodeManager)
        {
            _nodeManager = nodeManager;
        }

        public DetailedResponse<T> Create<T>(T value)
        {
            return new DetailedResponse<T>
            {
                Value = value,
                NodeInfo = _nodeManager.GetInfo(),
                NodeId = _nodeManager.GetId()
            };
        }


    }
}
