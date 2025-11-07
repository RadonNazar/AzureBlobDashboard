using AutoMapper;
using PD421_Dashboard_WEB_API.BLL.Dtos.Genre;
using PD421_Dashboard_WEB_API.DAL.Entitites;

namespace PD421_Dashboard_WEB_API.BLL.MapperProfiles
{
    public class GenreMapperProfile : Profile
    {
        public GenreMapperProfile()
        {
            // UpdateGenreDto -> GenreEntity
            CreateMap<UpdateGenreDto, GenreEntity>()
                .ForMember(dest => dest.NormalizedName, 
                opt => opt.MapFrom(src => src.Name.ToUpper()));

            // CreateGenreDto -> GenreEntity
            CreateMap<CreateGenreDto, GenreEntity>()
                .ForMember(dest => dest.NormalizedName,
                opt => opt.MapFrom(src => src.Name.ToUpper()));

            // GenreEntity -> GenreDto
            CreateMap<GenreEntity, GenreDto>();
        }
    }
}
