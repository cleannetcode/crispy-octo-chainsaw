using AutoMapper;
using CrispyOctoChainsaw.DataAccess.Postgres.Entities;
using CrispyOctoChainsaw.Domain.Interfaces;
using CrispyOctoChainsaw.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace CrispyOctoChainsaw.DataAccess.Postgres.Repositories
{
    public class CourseRepository : ICoursesRepository
    {
        private readonly CrispyOctoChainsawDbContext _context;
        private readonly IMapper _mapper;

        public CourseRepository(CrispyOctoChainsawDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Course[]> Get()
        {
            var courses = await _context.Courses.AsNoTracking().ToArrayAsync();
            return _mapper.Map<CourseEntity[], Course[]>(courses);
        }

        public async Task<Course?> Get(int courseId)
        {
            var course = await _context.Courses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == courseId);
            if (course is null)
            {
                return null;
            }
            return _mapper.Map<CourseEntity, Course>(course);
        }
    }
}
