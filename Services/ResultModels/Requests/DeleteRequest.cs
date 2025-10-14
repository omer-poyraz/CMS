using Services.Contracts;

namespace Services.ResultModels.Requests
{
    public class DeleteRequest<T> : DecisionModel<T>
    {
        public DeleteRequest(T result, int type, string service)
            : base(result, type, service) { }
    }
}
