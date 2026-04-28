namespace NetLearnSamples
{
    public interface IDataProcessor<T>
    {
        void Process(IEnumerable<T> data);
    }


    public class CsvExporter<T> : IDataProcessor<T>
    {
        public List<string> Output { get; } = [];

        public void Process(IEnumerable<T> data)
        {
            if (!data.Any())
            {
                foreach (var item in data)
                {
                    Output.Add(item!.ToString()!);
                }
            }
        }
    }

    public class SaleRecord
    {
        public int SalesPersonId { get; set; }
        public required string Region { get; set; }
        public decimal Amount { get; set; }
    }


    public class SampleClass
    {
        public void RunProcessor<T>(IDataProcessor<T> processor, IEnumerable<T> data)
        {
            processor.Process(data);
        }

        public void AggregateSalesRecords(IEnumerable<SaleRecord> salesRecords)
        {
            var winners = salesRecords
    .GroupBy(s => s.Region)
    .Select(regionGrp => regionGrp
        .GroupBy(s => s.SalesPersonId)
        .Select(spGrp => new
        {
            Region = regionGrp.Key,
            SalesPersonId = spGrp.Key,
            Total = spGrp.Sum(s => s.Amount)
        })
        .MaxBy(x => x.Total)!
    );
        }
    }

    //public class ReportService
    //{
    //    public async Task<string> GetReportSync()
    //    {
    //        // Bug is here somewhere...
    //        return await GenerateReportAsync().ConfigureAwait(false);
    //    }

    //    public async void SaveReportAsync(string content)
    //    {
    //        await _fileService.WriteAsync(content);
    //        _logger.Log("Saved");
    //    }

    //    private async Task<string> GenerateReportAsync()
    //    {
    //        var data = await _db.FetchAsync();
    //        return Process(data);
    //    }
    //}

    public abstract record PaymentResult;
    public record Success(string TransactionId) : PaymentResult;
    public record Failure(string Reason) : PaymentResult;
    public record Pending(int EstimatedSeconds) : PaymentResult;

    public class PaymentProcessor
    {
        public string Describe(PaymentResult result)
        {
            return result switch
            {
                Success s => $"Payment succeeded with transaction ID: {s.TransactionId}",
                Failure f => $"Payment failed due to: {f.Reason}",
                Pending p => $"Payment is pending, estimated time: {p.EstimatedSeconds} seconds",
                _ => throw new ArgumentOutOfRangeException(nameof(result), "Unknown payment result type")
            };
        }
    }

    public interface IEntity
    {
        int Id { get; }
    }

    public interface IRepository<T> where T : IEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task SaveAsync(T entity);
    }

    public class MemoryRepository<T> : IRepository<T> where T: IEntity
    {
        private readonly Dictionary<int, T> _store = [];

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(_store.Values.AsEnumerable());
        }

        public Task<T?> GetByIdAsync(int id)
        {
            return Task.FromResult(_store.GetValueOrDefault(id));
        }

        public Task SaveAsync(T entity)
        {
            _store[entity.Id] = entity;
            return Task.CompletedTask;
        }
    }
}




