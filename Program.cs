using PJATK_APBD_Cw5_s32509.Models;

var builder = WebApplication.CreateBuilder(args);

// Rejestracja usług
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Konfiguracja Swaggera
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();