using AutoMapper;
using PD421_Dashboard_WEB_API.BLL.Dtos.Auth;
using PD421_Dashboard_WEB_API.DAL.Entitites.Identity;

namespace PD421_Dashboard_WEB_API.BLL.MapperProfiles
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            // RegisterDto -> ApplicationUser
            CreateMap<RegisterDto, ApplicationUser>();
        }
    }
}
