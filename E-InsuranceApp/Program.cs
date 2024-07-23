using BusinessLayer.Interface;
using BusinessLayer.Service;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using RepositoryLayer.Handlers.Admin;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using RepositoryLayer.Utilities;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(typeof(CreateAdminHandler).Assembly);
// Add services to the container.
//DBContext
builder.Services.AddDbContext<EInsuranceDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EInsurance"));
});

builder.Services.AddScoped<IAdminRL, AdminRL>();
builder.Services.AddScoped<IAdminBL, AdminBL>();
builder.Services.AddScoped<ICustomerRL, CustomerRL>();
builder.Services.AddScoped<ICustomerBL, CustomerBL>();
builder.Services.AddScoped<RabbitMQService>();
builder.Services.AddScoped<EmailService>();

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
