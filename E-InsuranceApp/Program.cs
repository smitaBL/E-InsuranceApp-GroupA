using BusinessLayer.Interface;
using BusinessLayer.Service;
<<<<<<< HEAD
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
=======
using E_InsuranceApp.Controllers;
using MediatR;
>>>>>>> Sakshi/Agent-EmployeeRegistration
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using RepositoryLayer.Context;
<<<<<<< HEAD
using RepositoryLayer.Handlers.Login;
=======
using RepositoryLayer.Handlers.Agent;
>>>>>>> Sakshi/Agent-EmployeeRegistration
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using RepositoryLayer.Utilities;
using System.Reflection;
<<<<<<< HEAD
using System.Text;
=======
>>>>>>> Sakshi/Agent-EmployeeRegistration

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

<<<<<<< HEAD
try
=======
// Add services to the container.
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
//DBContext
builder.Services.AddDbContext<EInsuranceDbContext>(options =>
>>>>>>> Sakshi/Agent-EmployeeRegistration
{
    var builder = WebApplication.CreateBuilder(args);

<<<<<<< HEAD
    // Add services to the container.
    //DBContext
    builder.Services.AddDbContext<EInsuranceDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("EInsurance"));
    });
=======
builder.Services.AddScoped<RabbitMQService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<IAgentRL, AgentRL>();
builder.Services.AddScoped<IEmployeeRL,EmployeeRL>();
builder.Services.AddScoped<IAgentBL,AgentBL>();
builder.Services.AddScoped<IEmployeeBL,EmployeeBL>();
builder.Services.AddMediatR(typeof(CreateAgentHandler).Assembly);
>>>>>>> Sakshi/Agent-EmployeeRegistration

    //Mediator
    builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
    builder.Services.AddMediatR(typeof(LoginHandler).Assembly);

    //Login
    builder.Services.AddScoped<ILoginBL, LoginBL>();
    builder.Services.AddScoped<ILoginRL, LoginRL>();

    //RabbitMQ
    builder.Services.AddScoped<RabbitMQService>();

    //EmailService
    builder.Services.AddScoped<EmailService>();

    builder.Services.AddControllers();

    //Authentication(JWT)
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };
    });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //Logger
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex);
    throw (ex);
}
finally
{
    NLog.LogManager.Shutdown();
}