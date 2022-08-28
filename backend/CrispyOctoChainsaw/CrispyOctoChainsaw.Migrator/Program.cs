using CrispyOctoChainsaw.DataAccess.Postgres;
using CrispyOctoChainsaw.DataAccess.Postgres.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CrispyOctoChainsawDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(CrispyOctoChainsawDbContext)));
});

builder.Services
    .AddIdentityCore<UserEntity>()
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<CrispyOctoChainsawDbContext>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

var input = Console.ReadLine();
if (input == null)
{
    throw new ArgumentNullException();
}

var splitInput = input.Split(' ');
var email = splitInput[0];
var password = splitInput[1];

var roleName = builder.Configuration.GetSection(nameof(IdentityRole)).Value;

var roleExist = await roleManager.RoleExistsAsync(roleName);
if (!roleExist)
{
    var roleEntity = new IdentityRole<Guid>
    {
        Name = roleName
    };

    await roleManager.CreateAsync(roleEntity);
}

var user = await userManager.FindByEmailAsync(email);
if (user == null)
{
    var userEntity = new UserEntity
    {
        UserName = email,
        Email = email,
    };

    var createSystemAdmin = await userManager.CreateAsync(userEntity, password);
    if (createSystemAdmin.Succeeded)
    {
        await userManager.AddToRoleAsync(userEntity, roleName);
    }
}