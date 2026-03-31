using Application.Identity.Services;
using MockServiceApplication.Identity.DTO;

namespace hahaton.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("api/auth")
            .AllowAnonymous()
            .WithTags("Authentication")
            .WithOpenApi();
        
        endpoints.MapPost("register", Register)
            .Produces(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status409Conflict);
        
        endpoints.MapPost("login", Login)
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);
        
        return endpoints;
    }

    private static async Task<IResult> Register(
        RegisterRequest request,
        AuthService authService)
    {
        await Task.Delay(0);
        
        var userId =  authService.Register(
            request.Email,  request.Password);

        return Results.Created("/", new { id = userId});
    }

    private static async Task<IResult> Login(
        LoginRequest request,
        AuthService authService)
    {
        await Task.Delay(0);
        var token = authService.Login(request.Email, request.Password);

        return Results.Ok(token);
    }
}