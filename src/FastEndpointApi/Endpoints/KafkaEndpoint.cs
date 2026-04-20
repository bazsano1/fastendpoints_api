using FastEndpointApi.Domain;
using FastEndpoints;

namespace FastEndpointApi.Endpoints
{
    public class KafkaEndpoint : Endpoint<SendMessageRequest>
    {
        override public void Configure()
        {
            Post("api/kafka/send");
            AllowAnonymous();
        }

        override public async Task HandleAsync(SendMessageRequest req, CancellationToken ct)
        {
            var producer = new Kafka.KafkaProducer();
            await producer.ProduceAsync(req.Topic, req.Key, req.Value);
            await Send.OkAsync(cancellation: ct);
        }
    }
}
