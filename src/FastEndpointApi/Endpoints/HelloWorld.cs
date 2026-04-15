using FastEndpoints;

namespace FastEndpointApi.Endpoints
{
    public class HelloWorldEndpoint : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Get("/");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            await Send.OkAsync("Hello World from FastEndpoint!", cancellation: ct);
        }
    }
}
