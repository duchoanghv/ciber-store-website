using AutoMapper;
using CiberStoreWebsite.Models;

namespace CiberStoreWebsite
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegistrationModel, Microsoft.AspNetCore.Identity.IdentityUser>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
        }
    }
}
