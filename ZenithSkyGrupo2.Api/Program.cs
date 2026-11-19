using ZenithSkyGrupo2.Api.Business;
using ZenithSkyGrupo2.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<SqlConnectionFactory>();

// Data
builder.Services.AddScoped<AirplaneTypeData>();
builder.Services.AddScoped<AirlineData>();

// Business
builder.Services.AddScoped<AirplaneTypeService>();
builder.Services.AddScoped<AirlineService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();