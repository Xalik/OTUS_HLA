using AutoMapper;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using OtusSocNet.Dtos;
using OtusSocNet.Exceptions;
using OtusSocNet.Extensions;
using OtusSocNet.Middleware;
using OtusSocNet.Models.Requests;
using OtusSocNet.Models.Responses;
using OtusSocNet.Services.Interfaces;
using OtusSocNet.Swagger;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.Duration | HttpLoggingFields.RequestMethod | HttpLoggingFields.RequestPath;
    logging.MediaTypeOptions.AddText("application/json");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
    logging.CombineLogs = true;
});

services.AddAutoMapper(Assembly.GetExecutingAssembly());

services.AddBusinessLogic(configuration);
services.AddDataLayer(configuration);

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Use the same JSON naming policy as the API
    c.UseAllOfToExtendReferenceSchemas();
    c.SchemaFilter<SnakeCaseSchemaFilter>();
});

services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    options.SerializerOptions.WriteIndented = true;
});

var app = builder.Build();

app.ApplyMigration();
app.UseHttpLogging();
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/login", async ([FromBody] LoginRequest request, [FromServices] IUserService userService, CancellationToken cancellationToken) =>
{
    if (!request.IsValid())
        throw new BadRequestException();

    var token = await userService.LoginUserAsync(request.Id, request.Password, cancellationToken);
    return new LoginResponse
    {
        Token = token
    };
});

app.MapPost("/user/register", async ([FromBody] RegisterRequest request, [FromServices] IMapper mapper, [FromServices] IUserService userService, CancellationToken cancellationToken) =>
{
    if (!request.IsValid())
        throw new BadRequestException();

    var userId = await userService.RegisterUserAsync(mapper.Map<RegisterParameters>(request), cancellationToken);
    return new RegisterResponse
    {
        UserId = userId
    };
});

app.MapGet("/user/get/{userId}", async (Guid userId, [FromServices] IMapper mapper, [FromServices] IUserService userService, CancellationToken cancellationToken) =>
{
    if (userId == default)
        throw new BadRequestException();

    var user = await userService.GetUserAsync(userId, cancellationToken);
    return mapper.Map<UserResponse>(user);
});

app.Run();