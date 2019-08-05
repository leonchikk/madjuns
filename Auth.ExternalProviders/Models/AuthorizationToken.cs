namespace Auth.ExternalProviders.Models
{
    public class AuthorizationToken
    {
        public string Token { get; set; }
        public long Expiry { get; set; }
    }
}
