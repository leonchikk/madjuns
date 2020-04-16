using Communication.Core.Entities;

namespace Communication.API.Models.ViewModels
{
    public class ChannelViewModel
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public ChannelVisibility Visibility { get; set; }
    }
}
