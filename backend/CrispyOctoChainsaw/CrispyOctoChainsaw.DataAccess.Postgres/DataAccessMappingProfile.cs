using AutoMapper;
using CrispyOctoChainsaw.DataAccess.Postgres.Entities;
using CrispyOctoChainsaw.Domain;

namespace CrispyOctoChainsaw.DataAccess.Postgres
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<UserEntity, User>().ReverseMap();
            CreateMap<CourseEntity, Course>().ReverseMap();
        }
    }
}
