namespace Auth.ExternalProviders.Models
{
    public class ProviderUser
    {
        public string ProviderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
    }
}
