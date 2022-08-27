using AutoMapper;
using CrispyOctoChainsaw.Domain;
using CrispyOctoChainsaw.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CrispyOctoChainsaw.DataAccess.Postgres
{
    public class UsersRepository : IUsersRepository
    {
        private readonly CrispyOctoChainsawDbContext _context;
        private readonly IMapper _mapper;

        public UsersRepository(CrispyOctoChainsawDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User[]> Get()
        {
            var usersEntities = await _context.Users
                .AsNoTracking()
                .ToArrayAsync();

            var users = _mapper.Map<IdentityUser[], User[]>(usersEntities);
            return users;
        }

        public async Task<User?> Get(string id)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (userEntity is null)
            {
                return null;
            }

            var user = _mapper.Map<IdentityUser, User>(userEntity);
            return user;
        }

        public async Task<bool> Delete(string id)
        {
            var userToDelete = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (userToDelete is null)
            {
                return false;
            }

            _context.Remove(userToDelete);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
