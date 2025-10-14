using Services.Contracts;

namespace Services.ResultModels.Requests
{
    public class GetRequest<T> : DecisionModel<T>
    {
        public GetRequest(T result, int type, string service)
            : base(result, type, service) { }
    }
}
