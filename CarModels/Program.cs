using CarModels.Extensions;
using CarModels.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration);

builder.Logging.AddConsole();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddCustomMiddlewares();

app.UseAuthorization();

app.MapControllers();

app.Run();
