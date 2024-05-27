namespace MicroService.UserApi.Manager
{
    public class NodeManager
    {
        private Guid Guid { get; set; }
        public NodeManager()
        {
            Guid = Guid.NewGuid();
        }

        public string GetInfo()
        {
            return $"UserApi Node {Guid.ToString()}";
        }

        public string GetId()
        {
            return Guid.ToString();
        }
    }
}
