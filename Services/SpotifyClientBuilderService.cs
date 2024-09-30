using Microsoft.AspNetCore.Authentication;
using SpotifyAPI.Web;

namespace breadhinge.Services
{
    public class SpotifyClientBuilderService
    {
        private ILogger<SpotifyClientBuilderService> _logger;
        private readonly IHttpContextAccessor _httpContextAcessor;
        private readonly SpotifyClientConfig _spotifyClientConfig;

        public SpotifyClientBuilderService(ILogger<SpotifyClientBuilderService> logger, IHttpContextAccessor httpContextAccessor, SpotifyClientConfig spotifyClientConfig)
        {
            _logger = logger;
            _httpContextAcessor = httpContextAccessor;
            _spotifyClientConfig = spotifyClientConfig;
        }

        public async Task<SpotifyClient> BuildSpotifyClient()
        {
            var token = await _httpContextAcessor.HttpContext.GetTokenAsync("Spotify", "access_token");

            if (token == null)
            {
                throw new Exception("Failure to build Spotify Client");
            }

            return new SpotifyClient(_spotifyClientConfig.WithToken(token));
        }
    }
}
