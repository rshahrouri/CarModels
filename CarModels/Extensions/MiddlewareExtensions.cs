using CarModels.Middlewares;

namespace CarModels.Extensions;

public static class MiddlewareExtensions
{
    /// <summary>
    /// Adds custom middlewares to the application pipeline.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance.</param>
    /// <returns>The same instance of the <see cref="IApplicationBuilder"/> to allow method chaining.</returns>
    public static IApplicationBuilder AddCustomMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();

        return app;
    }
}
