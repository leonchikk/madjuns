using Communication.Core.Entities;

namespace Communication.API.Models.RequestModels
{
    public class CreateChannelRequestModel
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public ChannelVisibility Visibility { get; set; }
    }
}
