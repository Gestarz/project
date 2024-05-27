namespace MicroService.Shared.Response
{
    public class DetailedResponse<T>
    {
        public T Value;
        public string NodeInfo;
        public string NodeId;
    }
}
