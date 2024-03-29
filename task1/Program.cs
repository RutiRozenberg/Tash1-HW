using TaskService.Services;
using Utilities;
using MyMiddleware.LogerMiddleware;
using Token.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(cfg =>
    {
        cfg.RequireHttpsMetadata = false;
        cfg.TokenValidationParameters = TokenService.GetTokenValidationParameters();
    });
builder.Services.AddAuthorization(cfg =>
    {
        cfg.AddPolicy("User", policy => policy.RequireClaim("type", "User","Admin"));
        cfg.AddPolicy("Admin", policy => policy.RequireClaim("type", "Admin"));

    });
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();

builder.Services.AddTask();
builder.Services.AddUser();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FBI", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    { 
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer"}
        },
        new string[] {}
    }
    });
});
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();





app.UseLogMiddleware("file.log");
app.UseHttpsRedirection();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseDefaultFiles();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.Run();
