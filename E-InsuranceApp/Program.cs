using BusinessLayer.Interface;
using BusinessLayer.Service;
using E_InsuranceApp.Controllers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using RepositoryLayer.Handlers.Agent;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using RepositoryLayer.Utilities;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
//DBContext
builder.Services.AddDbContext<EInsuranceDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EInsurance"));
});

builder.Services.AddScoped<RabbitMQService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<IAgentRL, AgentRL>();
builder.Services.AddScoped<IEmployeeRL,EmployeeRL>();
builder.Services.AddScoped<IAgentBL,AgentBL>();
builder.Services.AddScoped<IEmployeeBL,EmployeeBL>();
builder.Services.AddMediatR(typeof(CreateAgentHandler).Assembly);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
