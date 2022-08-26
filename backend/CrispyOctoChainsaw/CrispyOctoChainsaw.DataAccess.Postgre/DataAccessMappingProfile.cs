using AutoMapper;
using CrispyOctoChainsaw.Domain;
using Microsoft.AspNetCore.Identity;

namespace CrispyOctoChainsaw.DataAccess.Postgre
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<IdentityUser, User>().ReverseMap();
        }
    }
}
