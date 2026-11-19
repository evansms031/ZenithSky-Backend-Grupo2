using ZenithSkyGrupo2.Api.Business;
using ZenithSkyGrupo2.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<SqlConnectionFactory>();
// Configurar controladores
builder.Services.AddControllers();

// Registrar la conexión a SQL Server
builder.Services.AddScoped<SqlConnectionFactory>();

// Registrar clases de Data
builder.Services.AddScoped<AirplaneTypeData>();
builder.Services.AddScoped<AirplaneData>();

// Registrar clases de Business
builder.Services.AddScoped<AirplaneTypeService>();
builder.Services.AddScoped<AirplaneService>();

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


