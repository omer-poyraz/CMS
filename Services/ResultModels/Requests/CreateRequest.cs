using Services.Contracts;

namespace Services.ResultModels.Requests
{
    public class CreateRequest<T> : DecisionModel<T>
    {
        public CreateRequest(T result, int type, string service)
            : base(result, type, service) { }
    }
}
