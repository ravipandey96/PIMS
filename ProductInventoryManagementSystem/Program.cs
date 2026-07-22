using Microsoft.EntityFrameworkCore;
using PIMS.API.Middleware;
using PIMS.Application.DependencyInjection;
using PIMS.Infrastructure.DependencyInjection;
using PIMS.Persistence.Context;
using PIMS.Persistence.DependencyInjection;
using PIMS.Persistence.Seed;

var builder = WebApplication.CreateBuilder(args);

// =================================
// Add Controllers
// =================================

builder.Services.AddControllers();

// =================================
// Application Layer
// =================================

builder.Services.AddApplication();

// =================================
// Infrastructure Layer
// =================================

builder.Services.AddInfrastructure(
    builder.Configuration);

// =================================
// Persistence Layer
// =================================

builder.Services.AddPersistence(
    builder.Configuration);

// =================================
// Build Application
// =================================

var app = builder.Build();

// =================================
// Database Seeder
// =================================

using (IServiceScope scope = app.Services.CreateScope())
{
    PimsDbContext context =
        scope.ServiceProvider
            .GetRequiredService<PimsDbContext>();

    // Apply pending migrations automatically
    await context.Database.MigrateAsync();

    // Seed default data
    await DatabaseSeeder.SeedAsync(context);
}

// =================================
// HTTP Pipeline
// =================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.DisplayRequestDuration();

        options.EnablePersistAuthorization();
    });
}

app.UseHttpsRedirection();

// =================================
// Custom Middleware
// =================================

app.UseGlobalExceptionHandling();

// Uncomment after RequestLoggingMiddleware is implemented
// app.UseRequestLogging();

// =================================
// Authentication & Authorization
// =================================

app.UseAuthentication();

app.UseAuthorization();

// =================================
// Endpoints
// =================================

app.MapControllers();

app.Run();