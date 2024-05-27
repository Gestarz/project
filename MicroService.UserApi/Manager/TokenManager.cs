using MicroService.Domain.Cache;
using MicroService.Domain.Class;

namespace MicroService.UserApi.Manager
{
    public class TokenManager
    {
        private string TokenKeyName = "Token";
        private ICacheRepository CacheRepository { get; set; }
        public TokenManager(ICacheRepository cacheRepository)
        {
            CacheRepository = cacheRepository;
        }

        public SecretToken GetToken()
        {
            SecretToken token = null;
            try
            {
                token = CacheRepository.Get<SecretToken>(TokenKeyName);
            }
            catch
            {
                token = new SecretToken
                {
                    Token = $"SuperSecret{Guid.NewGuid()}"
                };
                CacheRepository.Add(TokenKeyName, token);
            }
            return token;
        }

        public bool CheckToken(string token)
        {
            SecretToken secretToken = CacheRepository.Get<SecretToken>(TokenKeyName);
            return secretToken.Token == token;
        }

    }
}
