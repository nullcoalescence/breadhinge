using SpotifyAPI.Web;

namespace breadhinge.Services
{
    public class SpotifyService
    {
        private readonly ILogger<SpotifyService> _logger;
        private readonly SpotifyClientBuilderService _spotifyClientBuilderService;

        private SpotifyClient _spotify;

        public SpotifyService(ILogger<SpotifyService> logger, SpotifyClientBuilderService spotifyClientBuilderService)
        {
            _logger = logger;
            _spotifyClientBuilderService = spotifyClientBuilderService;
        }

        public async Task BuildSpotify()
        {
            _spotify = await _spotifyClientBuilderService.BuildSpotifyClient();
        }
    }
}
