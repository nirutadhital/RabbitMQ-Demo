using Microsoft.OpenApi.Models;
using Rabbit.API.Data;
using Rabbit.API.RabbitMQ;
using Rabbit.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rabbit.API", Version = "v1" });
});
builder.Services.AddControllers();

//additionally added
builder.Services.AddScoped < IProductServices, ProductServices > ();
builder.Services.AddDbContext < DbContextClass > ();
builder.Services.AddScoped < IRabbitMQProducer, RabbitMQProducer > ();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rabbit.API"));
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();


