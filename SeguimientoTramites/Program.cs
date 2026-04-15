using SeguimientoTramites.Common;

var builder = WebApplication.CreateBuilder(args);

// Servicios
builder.Services.AddControllers();
builder.Services.AddFluentValidationConfig();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar DbContext (conexion a BD)
builder.Services.AddSingleton<SeguimientoTramites.Data.DbContext>();

// Registrar Services
builder.Services.AddScoped<SeguimientoTramites.Features.Carreras.CarreraService>();
builder.Services.AddScoped<SeguimientoTramites.Features.Alumnos.AlumnoService>();
builder.Services.AddScoped<SeguimientoTramites.Features.Tramites.TramiteService>();

// CORS - para que el FRONT pueda conectarse
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
