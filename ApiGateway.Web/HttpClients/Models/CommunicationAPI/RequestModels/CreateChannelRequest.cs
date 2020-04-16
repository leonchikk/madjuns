namespace ApiGateway.Web.HttpClients.Models.CommunicationAPI.RequestModels
{
    public class CreateChannelRequest
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public ChannelVisibility Visibility { get; set; }
    }

    public enum ChannelVisibility
    {
        Public,
        Private
    }
}
