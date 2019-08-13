using AutoMapper;
using Users.API.Models.Requests;
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
            CreateMap<User, UserResponseModel>();
            CreateMap<UserProfile, ProfileResponseModel>();
            CreateMap<UserSetting, SettingResponseModel>()
                .ForMember(m => m.Id, opt => opt.MapFrom(r => r.Setting.Id))
                .ForMember(m => m.Name, opt => opt.MapFrom(r => r.Setting.Name));

        }
    }
}
