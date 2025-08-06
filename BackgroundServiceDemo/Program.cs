using BackgroundServiceDemo.Data;
using BackgroundServiceDemo.Interfaces;
using BackgroundServiceDemo.Services;
using BackgroundServiceDemo.Services.Background;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUserImportService, UserImportService>();
builder.Services.AddSingleton<IBGSStatusService, BGSStatusService>();

builder.Services.AddSingleton<IUserImportTrigger, UserImportBGS>();
builder.Services.AddHostedService(sp => (UserImportBGS)sp.GetRequiredService<IUserImportTrigger>());

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

DbInitializer.Initialize();

app.Run();
