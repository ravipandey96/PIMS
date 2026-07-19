using Microsoft.EntityFrameworkCore;
using PIMS.Persistence.Context;
using PIMS.Persistence.Seed;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();


// Swagger

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


// Database Configuration

builder.Services.AddDbContext<PimsDbContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection")
        )
    );
});


// Add Application Services
// builder.Services.AddApplication();


// Add Infrastructure Services
// builder.Services.AddInfrastructure();



var app = builder.Build();



// ===============================
// Database Seeder
// ===============================

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<PimsDbContext>();

    await DatabaseSeeder.SeedAsync(context);
}



// Configure HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}


app.UseHttpsRedirection();


app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();


app.Run();