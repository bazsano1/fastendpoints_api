namespace FastEndpointApi.Domain
{
    public class SendMessageRequest
    {
        public required string Topic { get; set; }

        public required string Key { get; set; }

        public required string Value { get; set; }
    }
}
