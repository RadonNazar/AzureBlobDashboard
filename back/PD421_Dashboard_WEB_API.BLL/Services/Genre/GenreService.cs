using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PD421_Dashboard_WEB_API.BLL.Dtos.Genre;
using PD421_Dashboard_WEB_API.DAL.Entitites;
using PD421_Dashboard_WEB_API.DAL.Repositories.Genre;
using System.Net;

namespace PD421_Dashboard_WEB_API.BLL.Services.Genre
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> CreateAsync(CreateGenreDto dto)
        {
            if(await _genreRepository.IsExistsAsync(dto.Name))
            {
                return new ServiceResponse
                {
                    Message = $"Жанр з назвою '{dto.Name}' вже існує",
                    IsSuccess = false,
                    HttpStatusCode = HttpStatusCode.BadRequest
                };
            }

            // CreateGenreDto -> GenreEntity
            var entity = _mapper.Map<GenreEntity>(dto);

            await _genreRepository.CreateAsync(entity);

            return new ServiceResponse 
            {
                Message = $"Жанр '{dto.Name}' створено",
                HttpStatusCode = HttpStatusCode.Created,
            };
        }

        public async Task<ServiceResponse> DeleteAsync(string id)
        {
            var entity = await _genreRepository.GetByIdAsync(id);

            if(entity == null)
            {
                return new ServiceResponse
                {
                    Message = $"Жанр з id '{id}' не знайдено",
                    IsSuccess = false,
                    HttpStatusCode = HttpStatusCode.BadRequest
                };
            }

            await _genreRepository.DeleteAsync(entity);

            return new ServiceResponse
            {
                Message = $"Жанр '{entity.Name}' видалено"
            };
        }

        public async Task<ServiceResponse> GetAllAsync()
        {
            var entities = await _genreRepository.GetAll().ToListAsync();

            var dtos = _mapper.Map<List<GenreDto>>(entities);

            return new ServiceResponse
            {
                Message = "Жанри отримано",
                Data = dtos
            };
        }

        public async Task<ServiceResponse> GetByIdAsync(string id)
        {
            var entity = await _genreRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return new ServiceResponse
                {
                    Message = $"Жанр з id '{id}' не знайдено",
                    IsSuccess = false,
                    HttpStatusCode = HttpStatusCode.BadRequest
                };
            }

            var dto = _mapper.Map<GenreDto>(entity);

            return new ServiceResponse
            {
                Message = $"Жанр '{dto.Name}' отримано",
                Data = dto
            };
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateGenreDto dto)
        {
            if (await _genreRepository.IsExistsAsync(dto.Name))
            {
                return new ServiceResponse
                {
                    Message = $"Жанр з назвою '{dto.Name}' вже існує",
                    IsSuccess = false,
                    HttpStatusCode = HttpStatusCode.BadRequest
                };
            }

            var entity = await _genreRepository.GetByIdAsync(dto.Id);

            if(entity == null)
            {
                return new ServiceResponse
                {
                    Message = $"Жанр з id '{dto.Id}' не знайдено",
                    IsSuccess = false,
                    HttpStatusCode = HttpStatusCode.BadRequest
                };
            }

            // dto -> entity
            entity = _mapper.Map(dto, entity);

            await _genreRepository.UpdateAsync(entity);

            return new ServiceResponse
            {
                Message = $"Жанр '{dto.Name}' оновлено"
            };
        }
    }
}
