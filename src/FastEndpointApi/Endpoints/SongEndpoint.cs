using FastEndpointApi.Domain;
using FastEndpoints;

namespace FastEndpointApi.Endpoints
{
    public class SongEndpoint : Endpoint<SongRequest, SongResponse>
    {
        public override void Configure()
        {
            Post("api/songs");
            AllowAnonymous();
        }

        public override async Task HandleAsync(SongRequest req, CancellationToken ct)
        {
            if (req.Year < 2026)
            {
                await Send.NotFoundAsync(ct);
            }
            else
            {
                await Send.OkAsync(new SongResponse
                {
                    Author = "6363",
                    Title = "etiket",
                    Genre = "hiphop",
                    Year = 2026
                }, cancellation: ct);
            }

        }
    }
}
