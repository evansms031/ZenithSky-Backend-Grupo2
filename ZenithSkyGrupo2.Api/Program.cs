
using ZenithSkyGrupo2.Api.Business;
using ZenithSkyGrupo2.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurar controladores
builder.Services.AddControllers();

// Registrar la conexión a SQL Server
builder.Services.AddScoped<SqlConnectionFactory>();

// Registrar clases de Data
builder.Services.AddScoped<AirplaneTypeData>();
builder.Services.AddScoped<AirlineData>();
builder.Services.AddScoped<AirportData>();
builder.Services.AddScoped<AirplaneData>();

// Registrar clases de Business
builder.Services.AddScoped<AirplaneTypeService>();
builder.Services.AddScoped<AirlineService>();
builder.Services.AddScoped<AirportService>();
builder.Services.AddScoped<AirplaneService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar el flujo de la aplicación
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();