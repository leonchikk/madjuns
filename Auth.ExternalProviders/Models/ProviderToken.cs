using System.ComponentModel.DataAnnotations;

namespace Auth.ExternalProviders.Models
{
    public class ProviderToken
    {
        [Required]
        public string Token { get; set; }
    }
}
