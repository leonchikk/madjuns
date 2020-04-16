using Communication.API.Models.ViewModels;
using Communication.Core.Entities;
using MediatR;

namespace Communication.API.Application.Commands.Channels
{
    public class CreateChannelCommand: IRequest<ChannelViewModel>
    {
        public CreateChannelCommand(string name, string logoUrl, ChannelVisibility visibility)
        {
            Name = name;
            LogoUrl = logoUrl;
            Visibility = visibility;
        }

        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public ChannelVisibility Visibility { get; set; }
    }
}
