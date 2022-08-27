using AutoMapper;
using CrispyOctoChainsaw.Domain;
using Microsoft.AspNetCore.Identity;

namespace CrispyOctoChainsaw.DataAccess.Postgres
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<IdentityUser, User>().ReverseMap();
        }
    }
}
