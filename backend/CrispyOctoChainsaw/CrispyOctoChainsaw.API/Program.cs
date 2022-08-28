using CrispyOctoChainsaw.API;
using CrispyOctoChainsaw.BusinessLogic;
using CrispyOctoChainsaw.DataAccess.Postgres;
using CrispyOctoChainsaw.DataAccess.Postgres.Entities;
using CrispyOctoChainsaw.DataAccess.Postgres.Repositories;
using CrispyOctoChainsaw.Domain.Interfaces;
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
    .AddIdentityCore<UserEntity>()
    .AddEntityFrameworkStores<CrispyOctoChainsawDbContext>();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<ApiMappingProfile>();
    config.AddProfile<DataAccessMappingProfile>();
});

builder.Services.AddScoped<IUsersRepository, UsersRepository>();

builder.Services.AddScoped<ISystemAdminsService, SystemAdminsService>();

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