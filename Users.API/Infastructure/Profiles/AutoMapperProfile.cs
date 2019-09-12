using System.Linq;
using Users.API.Models.Responses;
using Users.Core.Domain;
using Profile = AutoMapper.Profile;
using UserProfile = Users.Core.Domain.Profile;

namespace Users.API.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Address, AddressResponseModel>();

            CreateMap<User, BaseUserResponseModel>()
                .ForMember(m => m.UserId, opt => opt.MapFrom(r => r.Id));

            CreateMap<User, UserResponseModel>()
                .ForMember(m => m.UserId, opt => opt.MapFrom(r => r.Id))
                .ForMember(m => m.BlackList, opt => opt.MapFrom(r => r.BlackList.Select(entity => entity.BannedUser)))
                .ForMember(m => m.Friends, opt => opt.MapFrom(r => r.Friends.Select(f => f.Friend)))
                .ForMember(m => m.SubscribesTo, opt => opt.MapFrom(r => r.SubscribesTo.Select(entity => entity.User)))
                .ForMember(m => m.Subscribers, opt => opt.MapFrom(r => r.Subscribers.Select(entity => entity.Subscriber)));

            CreateMap<UserProfile, ProfileResponseModel>();

            CreateMap<UserSetting, SettingResponseModel>()
                .ForMember(m => m.Id, opt => opt.MapFrom(r => r.Setting.Id))
                .ForMember(m => m.Name, opt => opt.MapFrom(r => r.Setting.Name));

        }
    }
}
