﻿using AutoMapper;
using CrispyOctoChainsaw.API.Contracts;
using CrispyOctoChainsaw.Domain.Model;

namespace CrispyOctoChainsaw.API
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<User, GetUserResponse>();
            CreateMap<Course, GetCourseResponse>();
            CreateMap<Course, GetCourseResponse>();
            CreateMap<Exercise, GetExerciseResponse>();
        }
    }
}
