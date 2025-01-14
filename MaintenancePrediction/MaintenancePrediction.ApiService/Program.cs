using MaintenancePrediction.ApiService.Data;
using MaintenancePrediction.ApiService.Jobs;
using MaintenancePrediction.ApiService.Middleware;
using MaintenancePrediction.ApiService.MLModels;
using MaintenancePrediction.ApiService.Services;
using MaintenancePrediction.ApiService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

builder.Services.AddControllers();
builder.Services.AddAuthorization(); // Add this line for authorization services

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp", policy =>
    {
        policy.WithOrigins("https://localhost:7074") // Replace with your Blazor app URL
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add the database context to the services.
builder.Services.AddScoped<IMachineDataService, MachineDataService>();
builder.Services.AddScoped<IMachineEventService, MachineEventService>();
builder.Services.AddScoped<IMachineUsageService, MachineUsageService>();
builder.Services.AddScoped<IMachineMaintenanceCheckResultService, MachineMaintenanceCheckResultService>();
builder.Services.AddDbContext<MachineMaintenanceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add ML model service to the services.

builder.Services.AddScoped<IPredictionService, PredictionService>();
builder.Services.AddHostedService<PredictionJob>();

builder.Services.AddSingleton<MLModelWrapper>(sp =>
{
    //var mlContext = new MLContext();
    var mlModelPath = "path/to/your/model.zip";
    //var mlModel = mlContext.Model.Load(mlModelPath, out _);
    //return new MLModelWrapper(mlModel, mlContext);
    return new MLModelWrapper();
});

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(); // add swagger NuGet package

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
    app.MapHealthChecks("/health");
    app.UseMiddleware<ErrorHandlingMiddleware>();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazorApp");
app.UseAuthorization();
app.MapControllers();

app.Run();
