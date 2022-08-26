using CrispyOctoChainsaw.API;
using CrispyOctoChainsaw.BusinessLogic;
using CrispyOctoChainsaw.DataAccess.Postgre;
using CrispyOctoChainsaw.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CrispyOctoChainsawDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(CrispyOctoChainsawDbContext)));
});

builder.Services
    .AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<CrispyOctoChainsawDbContext>();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<ApiMappingProfile>();
    config.AddProfile<DataAccessMappingProfile>();
});

builder.Services.AddScoped<IUsersRepository, UsersRepository>();

builder.Services.AddScoped<ISystemAdminService, SystemAdminService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();