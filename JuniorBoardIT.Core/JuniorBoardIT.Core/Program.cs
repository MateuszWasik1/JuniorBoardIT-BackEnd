using AutoMapper;
using JuniorBoardIT.Core;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.CQRS.Dispatcher;
using JuniorBoardIT.Core.CQRS.Resources.Accounts.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Accounts.Handlers;
using JuniorBoardIT.Core.CQRS.Resources.Accounts.Queries;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Commands;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Handlers;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Queries;
using JuniorBoardIT.Core.CQRS.Resources.Reports.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Reports.Handlers;
using JuniorBoardIT.Core.CQRS.Resources.Reports.Queries;
using JuniorBoardIT.Core.CQRS.Resources.Roles.Handlers;
using JuniorBoardIT.Core.CQRS.Resources.Roles.Queries;
using JuniorBoardIT.Core.CQRS.Resources.User.Commands;
using JuniorBoardIT.Core.CQRS.Resources.User.Handlers;
using JuniorBoardIT.Core.CQRS.Resources.User.Queries;
using JuniorBoardIT.Core.Entities;
using JuniorBoardIT.Core.Models.ViewModels;
using JuniorBoardIT.Core.Models.ViewModels.JobOffersViewModels;
using JuniorBoardIT.Core.Models.ViewModels.ReportsViewModels;
using JuniorBoardIT.Core.Models.ViewModels.UserViewModels;
using JuniorBoardIT.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using JuniorBoardIT.Core.CQRS.Resources.Bugs.Bugs.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Bugs.Bugs.Handlers;
using JuniorBoardIT.Core.CQRS.Resources.Bugs.Bugs.Queries;
using JuniorBoardIT.Core.CQRS.Resources.Bugs.BugsNotes.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Bugs.BugsNotes.Handlers;
using JuniorBoardIT.Core.CQRS.Resources.Bugs.BugsNotes.Queries;
using JuniorBoardIT.Core.Models.ViewModels.BugsViewModels;
using System.Text;
using JuniorBoardIT.Core.CQRS.Resources.Companies.Queries;
using JuniorBoardIT.Core.Models.ViewModels.CompaniesViewModel;
using JuniorBoardIT.Core.CQRS.Resources.Companies.Handlers;
using JuniorBoardIT.Core.CQRS.Resources.Companies.Commands;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddRazorPages();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

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

//JobOffers
builder.Services.AddScoped<IQueryHandler<GetJobOfferQuery, JobOfferViewModel>, GetJobOfferQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetAllJobOffersQuery, GetAllJobOffersViewModel>, GetAllJobOffersQueryHandler>();

builder.Services.AddScoped<ICommandHandler<AddJobOfferCommand>, AddJobOfferCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateJobOfferCommand>, UpdateJobOfferCommandHandler>();
builder.Services.AddScoped<ICommandHandler<DeleteJobOfferCommand>, DeleteJobOfferCommandHandler>();

//Reports
builder.Services.AddScoped<IQueryHandler<GetReportQuery, GetReportViewModel>, GetReportQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetReportsQuery, GetReportsViewModel>, GetReportsQueryHandler>();

builder.Services.AddScoped<ICommandHandler<ChangeReportStatusCommand>, ChangeReportStatusCommandHandler>();
builder.Services.AddScoped<ICommandHandler<SaveReportCommand>, SaveReportCommandHandler>();

//Bugs
builder.Services.AddScoped<IQueryHandler<GetBugQuery, BugViewModel>, GetBugQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetBugsQuery, GetBugsViewModel>, GetBugsQueryHandler>();

builder.Services.AddScoped<ICommandHandler<SaveBugCommand>, SaveBugsCommandHandler>();
builder.Services.AddScoped<ICommandHandler<ChangeBugStatusCommand>, ChangeBugStatusCommandHandler>();

//BugsNotes
builder.Services.AddScoped<IQueryHandler<GetBugNotesQuery, GetBugsNotesViewModel>, GetBugNotesQueryHandler>();

builder.Services.AddScoped<ICommandHandler<SaveBugNoteCommand>, SaveBugNoteCommandHandler>();

//JobOffers
builder.Services.AddScoped<IQueryHandler<GetCompanyQuery, CompanyViewModel>, GetCompanyQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetCompaniesQuery, GetCompaniesViewModel>, GetCompaniesQueryHandler>();

builder.Services.AddScoped<ICommandHandler<AddCompanyCommand>, AddCompanyCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateCompanyCommand>, UpdateCompanyCommandHandler>();
builder.Services.AddScoped<ICommandHandler<DeleteCompanyCommand>, DeleteCompanyCommandHandler>();
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