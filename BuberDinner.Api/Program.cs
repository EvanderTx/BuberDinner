using BuberDinner.Application;
using BuberDinner.Infrastructure;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Api.MiddleWare;
using BuberDinner.Api.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using BuberDinner.Api.Errors;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    // builder.Services.AddControllers(options=>
    // {
    //     options.Filters.Add<ErrorHandlingFilterAttribute>();
    // });
    builder.Services.AddControllers();

    //builder.Services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetailsFactory>();

}

var app = builder.Build();
{
    //app.UseMiddleware<ErrorHandlingMidleware>();
    app.UseExceptionHandler("/error");
    app.Map("/error", (HttpContext httpContext) =>
    {
        Exception? exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        return Results.Problem();
    });
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}

