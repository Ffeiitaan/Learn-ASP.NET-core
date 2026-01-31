using Microsoft.AspNetCore.Mvc;


namespace CrudOrders.Middelwares;


public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(KeyNotFoundException ex)
        {
            await WriteProblemDetailAsync(
                context,
                statusCode: StatusCodes.Status404NotFound,
                title: "Resource not found",
                detail: ex.Message
            );
        }
        catch(ArgumentException ex)
        {
            await WriteProblemDetailAsync(
                context,
                statusCode: StatusCodes.Status400BadRequest,
                title: "Bad request",
                detail: ex.Message
            );
        }
        catch(InvalidOperationException ex)
        {
            await WriteProblemDetailAsync(
                context,
                statusCode: StatusCodes.Status409Conflict,
                title: "Bad request",
                detail: ex.Message
            );
        }
        catch(Exception)
        {
            await WriteProblemDetailAsync(
                context,
                statusCode: StatusCodes.Status500InternalServerError,
                title: "Internal server error",
                detail: "An unexpected error occurred" 
            );
        }
    }

    private static async Task WriteProblemDetailAsync(
        HttpContext context,
        int statusCode,
        string title,
        string detail
    )
    {
        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Instance = context.Request.Path
        };
        
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsJsonAsync(problem);
    }
}