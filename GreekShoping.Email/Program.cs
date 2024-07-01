using Microsoft.EntityFrameworkCore;
using GreekShoping.Email.Context;
using GreekShoping.Email.Repository;
using GreekShoping.Email.MessegeConsumer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var conecction = builder.Configuration["MySqlConnection:MySqlConnectionString"];

builder.Services.AddDbContext<MySqlContext>(options => options.UseMySql(
              conecction, new MySqlServerVersion(new Version(8, 4, 0))));

var dbContextBuilder = new DbContextOptionsBuilder<MySqlContext>();
dbContextBuilder.UseMySql(conecction,
    new MySqlServerVersion(new Version(8, 4, 0))
);

//Injectando dependencias 
builder.Services.AddSingleton(new EmailRepository(dbContextBuilder.Options));
builder.Services.AddScoped<IEmailRepository, EmailRepository>();

builder.Services.AddHostedService<RabbitMQPaymenttConsumer>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
