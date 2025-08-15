using Application;
using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Core.CrossCuttingConcerns.SeriLog.ConfigurationModels;
using Core.Mailing.Dto;
using Core.Security;
using Microsoft.OpenApi.Models;
using Persistence;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<Program>() // ?? Bu satýr çok önemli
    .AddEnvironmentVariables();

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceService(builder.Configuration);
builder.Services.AddSecurityServices();
builder.Services.AddHttpContextAccessor();
builder.Services.AddYamlResourceLocalization();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});
// Lea

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition(
        name: "Bearer",
        securityScheme: new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description =
                "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer YOUR_TOKEN\". \r\n\r\n"
                + "`Enter your token in the text input below.`"
        }
    );
   // opt.OperationFilter<BearerSecurityRequirementOperationFilter>();
});
builder.Services.AddCors(opt =>
    opt.AddDefaultPolicy(p =>
    {
        p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    })
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//if (app.Environment.IsProduction())
    app.configureExceptionMiddleware();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.UseCors(opt => opt.WithOrigins().AllowAnyHeader().AllowAnyMethod().AllowCredentials());


app.Run();
