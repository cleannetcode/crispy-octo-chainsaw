using CrispyOctoChainsaw.API;
using CrispyOctoChainsaw.API.Options;
using CrispyOctoChainsaw.BusinessLogic.Services;
using CrispyOctoChainsaw.DataAccess.Postgres;
using CrispyOctoChainsaw.DataAccess.Postgres.Entities;
using CrispyOctoChainsaw.DataAccess.Postgres.Repositories;
using CrispyOctoChainsaw.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JWTSecretOptions>(
    builder.Configuration.GetSection(JWTSecretOptions.JWTSecret));

builder.Services.AddScoped<ICmsCoursesService, CmsCoursesService>();
builder.Services.AddScoped<ICmsCoursesRepository, CmsCoursesRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CrispyOctoChainsawDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(CrispyOctoChainsawDbContext)));
});

builder.Services
    .AddIdentityCore<UserEntity>()
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<CrispyOctoChainsawDbContext>();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<ApiMappingProfile>();
    config.AddProfile<DataAccessMappingProfile>();
});

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<ICoursesRepository, CourseRepository>();
builder.Services.AddScoped<ISystemAdminsService, SystemAdminsService>();
builder.Services.AddScoped<ICoursesService, CoursesService>();
builder.Services.AddScoped<ISessionsRepository, SessionsRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWTSecret:Secret").Value))
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(option =>
{
    option.WithHeaders().AllowAnyHeader();
    option.WithOrigins().AllowAnyOrigin();
    option.AllowAnyMethod();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }