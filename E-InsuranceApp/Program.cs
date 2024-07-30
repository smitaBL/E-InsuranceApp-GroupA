using BusinessLayer.Interface;
using BusinessLayer.Service;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using E_InsuranceApp.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using RepositoryLayer.Context;
using RepositoryLayer.Handlers.Login;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using RepositoryLayer.Utilities;
using System.Reflection;
using System.Text;
using Microsoft.OpenApi.Models;
using Org.BouncyCastle.Asn1.X509;


var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{ 
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    //DBContext
    builder.Services.AddDbContext<EInsuranceDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("EInsurance"));
    });

    //Mediator
    builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
    builder.Services.AddMediatR(typeof(LoginHandler).Assembly);

    //Agent
    builder.Services.AddScoped<IAgentBL, AgentBL>();
    builder.Services.AddScoped<IAgentRL, AgentRL>();

    //Commission
    builder.Services.AddScoped<ICommissionBL, CommissionBL>();
    builder.Services.AddScoped<ICommissionRL,CommissionRL>();

    //Employee
    builder.Services.AddScoped<IEmployeeRL, EmployeeRL>();
    builder.Services.AddScoped<IEmployeeBL, EmployeeBL>();

    //Login
    builder.Services.AddScoped<ILoginBL, LoginBL>();
    builder.Services.AddScoped<ILoginRL, LoginRL>();

    //Admin
    builder.Services.AddScoped<IAdminBL, AdminBL>();
    builder.Services.AddScoped<IAdminRL, AdminRL>();    

    //Customer
    builder.Services.AddScoped<ICustomerBL, CustomerBL>();
    builder.Services.AddScoped<ICustomerRL, CustomerRL>();

    //InsurancePlan
    builder.Services.AddScoped<IInsurancePlanBL, InsurancePlanBL>();
    builder.Services.AddScoped<IInsurancePlanRL, InsurancePlanRL>();

    //Policy
    builder.Services.AddScoped<IPolicyBL, PolicyBL>();
    builder.Services.AddScoped<IPolicyRL, PolicyRL>();

    //Scheme
    builder.Services.AddScoped<ISchemeBL, SchemeBL>();
    builder.Services.AddScoped<ISchemeRL, SchemeRL>();


    //RabbitMQ
    builder.Services.AddScoped<RabbitMQService>();

    builder.Services.AddControllers();

    //Authentication(JWT)
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,options =>
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
    // builder.Services.AddSwaggerGen();

    //Swagger
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore API", Version = "v1" });

        var securityScheme = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Enter JWT Bearer token **_only_**",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };

        c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
        { securityScheme, Array.Empty<string>() }
        });
    });

    //CORS
    const string policyName = "CorsPolicy";
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: policyName, builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
    });


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

    app.UseCors();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex);
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}