using AutoMapper;
using CrispyOctoChainsaw.DataAccess.Postgres.Entities;
using CrispyOctoChainsaw.Domain;
using CrispyOctoChainsaw.Domain.Interfaces;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace CrispyOctoChainsaw.DataAccess.Postgres
{
    public class CourseAdminRepository : ICourseAdminRepository
    {
        private readonly CrispyOctoChainsawDbContext _context;
        private readonly IMapper _mapper;

        public CourseAdminRepository(CrispyOctoChainsawDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<int>> Create(Course newCourse)
        {
            var courseEntity = _mapper.Map<Course, CourseEntity>(newCourse);

            await _context.Courses.AddAsync(courseEntity);
            await _context.SaveChangesAsync();

            return courseEntity.Id;
        }

        public async Task<Result<int>> Edit(Course editCourse)
        {
            var courseEntity = _mapper.Map<Course, CourseEntity>(editCourse);

            _context.Courses.Update(courseEntity);
            await _context.SaveChangesAsync();

            return editCourse.Id;
        }

        public async Task<Result<Course>> FindCourseById(int courseId)
        {
            if (courseId <= default(int))
            {
                return Result.Failure<Course>($"{nameof(courseId)} is not valid.");
            }

            var course = await _context.Courses
                .TagWith("Find course by Id")
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == courseId);

            return _mapper.Map<CourseEntity, Course>(course);
        }

        public async Task<Result<Course[]>> GetAdminCourses(Guid courseAdminId)
        {
            if (courseAdminId == Guid.Empty)
            {
                return Result.Failure<Course[]>($"{nameof(courseAdminId)} is not valid.");
            }

            var adminCourses = await _context.Courses
                .TagWith("Get admin courses")
                .AsNoTracking()
                .Where(x => x.CourseAdminId == courseAdminId)
                .ToArrayAsync();

            return _mapper.Map<CourseEntity[], Course[]>(adminCourses);
        }

        public async Task<Result<int>> Delete(int courseId)
        {
            var course = await FindCourseById(courseId);
            if (course.IsFailure)
            {
                return Result.Failure<int>("Course not found.");
            }

            _context.Courses.Remove(new CourseEntity { Id = course.Value.Id });

            return course.Value.Id;
        }
    }
}
