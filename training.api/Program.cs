using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using training.api.Model;
using training.api;
using Microsoft.AspNetCore.Components.Forms;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<TrainingContext, TrainingContext>();
builder.Services.AddDbContext<TrainingContext>((provider, options) =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    options
        .UseLazyLoadingProxies()
        .UseSqlServer(config.GetConnectionString("TrainingContext"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

var options = new DbContextOptionsBuilder<TrainingContext>();
options.UseSqlServer(app.Configuration.GetConnectionString("TrainingContext"));

using (var context = new TrainingContext(options.Options))
{
    context.Database.Migrate();
}

ExcelService excelService = new ExcelService();
excelService.ExportarDados();
excelService.Importa(new TrainingContext(options.Options));

app.Run();




