using DogsHouse.DataAccess;
using DogsHouse.Application;
using DogsHouse.DataAccess.Data;
using DogsHouse.Api.Middleware.ExceptionHandler;
using DogsHouse.Api.Middleware.RequestLimit;
using DogsHouse.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.AddPresentationServices();

var app = builder.Build();

app.UseCustomExceptionHandler();

app.UseRequestLimitHandler();

app.MapControllers();  

if (app.Environment.IsDevelopment())
{
    await app.InitializeDbAsync();
    app.UseSwagger();
    app.UseSwaggerUI(conf =>
    {
        conf.RoutePrefix = string.Empty;
        conf.SwaggerEndpoint("swagger/v1/swagger.json", "Dogs API");
    });
}

app.Run();
