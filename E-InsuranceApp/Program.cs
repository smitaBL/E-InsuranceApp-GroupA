using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using RepositoryLayer.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//DBContext
builder.Services.AddDbContext<EInsuranceDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EInsurance"));
});

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
