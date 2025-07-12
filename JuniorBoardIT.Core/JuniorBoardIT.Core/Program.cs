using AutoMapper;
using JuniorBoardIT.Core;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.CQRS.Dispatcher;
using JuniorBoardIT.Core.CQRS.Resources.Accounts.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Accounts.Handlers;
using JuniorBoardIT.Core.CQRS.Resources.Accounts.Queries;
using JuniorBoardIT.Core.CQRS.Resources.Roles.Handlers;
using JuniorBoardIT.Core.CQRS.Resources.Roles.Queries;
using JuniorBoardIT.Core.CQRS.Resources.User.Commands;
using JuniorBoardIT.Core.CQRS.Resources.User.Handlers;
using JuniorBoardIT.Core.CQRS.Resources.User.Queries;
using JuniorBoardIT.Core.Entities;
using JuniorBoardIT.Core.Models.ViewModels;
using JuniorBoardIT.Core.Models.ViewModels.UserViewModels;
using JuniorBoardIT.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddRazorPages();
builder.Services.AddControllers().AddNewtonsoftJson();

//mapper start
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
    mc.DestinationMemberNamingConvention = ExactMatchNamingConvention.Instance;
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//mapper end

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
    options.AddPolicy(name: "LibraryPolicy",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    })
);

builder.Services.AddIdentity<Users, IdentityRole>(config =>
{
    config.SignIn.RequireConfirmedEmail = true;
}).AddEntityFrameworkStores<DataContext>()
  .AddDefaultTokenProviders();

builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();
builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddScoped<IDispatcher, Dispatcher>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

//CQRS
#region CQRS
//Accounts
builder.Services.AddScoped<IQueryHandler<LoginQuery, string>, LoginQueryHandler>();

builder.Services.AddScoped<ICommandHandler<RegisterUserCommand>, RegisterUserCommandHandler>();

//User
builder.Services.AddScoped<IQueryHandler<GetAllUsersQuery, GetUsersAdminViewModel>, GetAllUsersQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetUserByAdminQuery, UserAdminViewModel>, GetUserByAdminQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetUserQuery, UserViewModel>, GetUserQueryHandler>();

builder.Services.AddScoped<ICommandHandler<SaveUserCommand>, SaveUserCommandHandler>();
builder.Services.AddScoped<ICommandHandler<SaveUserByAdminCommand>, SaveUserByAdminCommandHandler>();
builder.Services.AddScoped<ICommandHandler<DeleteUserCommand>, DeleteUserCommandHandler>();

//Roles
builder.Services.AddScoped<IQueryHandler<GetUserRolesQuery, RolesViewModel>, GetUserRolesQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetIsUserPremiumQuery, bool>, GetIsUserPremiumQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetIsUserRecruiterQuery, bool>, GetIsUserRecruiterQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetIsUserSupportQuery, bool>, GetIsUserSupportQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetIsUserAdminQuery, bool>, GetIsUserAdminQueryHandler>();
#endregion

//Authentications
var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

builder.Services.AddSingleton(authenticationSettings);

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(configuration =>
{
    configuration.RequireHttpsMetadata = false;
    configuration.SaveToken = true;
    configuration.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JWTIssuer,
        ValidAudience = authenticationSettings.JWTIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JWTKey)),
    };
});

//Building
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapRazorPages();
app.UseAuthentication();
app.UseHttpsRedirection();

app.MapControllers();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("LibraryPolicy"); 
app.UseAuthorization();
app.Run();
app.Services.GetRequiredService<DataContext>().Database.EnsureCreated();