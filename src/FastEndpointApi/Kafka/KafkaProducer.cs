using Confluent.Kafka;

namespace FastEndpointApi.Kafka
{
    public class KafkaProducer
    {
        private readonly ProducerConfig _config;

        public KafkaProducer()
        {
            _config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };
        }

        public async Task ProduceAsync(string topic, string key, string value)
        {
            using var producer = new ProducerBuilder<string, string>(_config).Build();
            var message = new Message<string, string> { Key = key, Value = value };
            try
            {
                var result = await producer.ProduceAsync(topic, message);
                Console.WriteLine($"Message produced to topic {topic} with offset {result.Offset}");
            }
            catch (ProduceException<string, string> e)
            {
                Console.WriteLine($"Delivery failed: {e.Error.Reason}");
            }
        }
    }
}
