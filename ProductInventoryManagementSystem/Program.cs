using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PIMS.Infrastructure.Configurations;
using PIMS.Persistence.Context;
using PIMS.API.Middleware;
using PIMS.Persistence.Seed;
using System.Text;


var builder = WebApplication.CreateBuilder(args);



// =================================
// Add Controllers
// =================================

builder.Services.AddControllers();



// =================================
// Swagger
// =================================

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();



// =================================
// Database Configuration
// =================================

builder.Services.AddDbContext<PimsDbContext>(options =>
{
    options.UseMySql(
        builder.Configuration
            .GetConnectionString("DefaultConnection"),

        ServerVersion.AutoDetect(
            builder.Configuration
                .GetConnectionString("DefaultConnection"))
    );
});



// =================================
// JWT Configuration
// =================================

builder.Services.Configure<JwtOptions>(
    builder.Configuration
        .GetSection(JwtOptions.SectionName));



// =================================
// JWT Authentication
// =================================

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;


        options.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,


                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration
                                .GetSection("Jwt")["Key"]!
                        )
                    ),


                ValidateIssuer = true,


                ValidIssuer =
                    builder.Configuration
                        .GetSection("Jwt")["Issuer"],


                ValidateAudience = true,


                ValidAudience =
                    builder.Configuration
                        .GetSection("Jwt")["Audience"],


                ValidateLifetime = true,


                ClockSkew =
                    TimeSpan.Zero
            };
    });



// =================================
// Application Services
// =================================

// Uncomment after creating extension method

// builder.Services.AddApplication();



// =================================
// Infrastructure Services
// =================================

// Uncomment after creating extension method

// builder.Services.AddInfrastructure(
//     builder.Configuration);



var app = builder.Build();



// =================================
// Database Seeder
// =================================

using (var scope = app.Services.CreateScope())
{
    var context =
        scope.ServiceProvider
            .GetRequiredService<PimsDbContext>();


    await DatabaseSeeder.SeedAsync(context);
}



// =================================
// HTTP Pipeline
// =================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}


app.UseHttpsRedirection();


app.UseGlobalExceptionHandling();
// IMPORTANT ORDER

app.UseAuthentication();

app.UseAuthorization();



app.MapControllers();



app.Run();