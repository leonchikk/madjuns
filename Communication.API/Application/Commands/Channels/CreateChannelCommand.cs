using Communication.API.Models.ViewModels;
using MediatR;

namespace Communication.API.Application.Commands.Channels
{
    public class CreateChannelCommand: IRequest<ChannelViewModel>
    {
        public CreateChannelCommand(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
