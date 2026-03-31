using Microsoft.AspNetCore.Mvc;
using MockServiceApplication.DTOs;
using MockServiceApplication;

namespace hahaton.Endpoints;

public static class MockEndpoints
{
    public static IEndpointRouteBuilder MapMockEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("api/")
            .RequireAuthorization();

        endpoints.MapPost("mock", FillMockObject);
        endpoints.MapPost("custom", AddCustomType);
        
        return endpoints;
    }

    private static async Task<IResult> FillMockObject(
        [FromBody] MockRequest mockRequest,
        MockService mockService
    )
    {
        var filledMock = mockService.Generate(mockRequest);
        await Task.Delay(0);
        return Results.Ok(filledMock);
    }

    private static async Task<IResult> AddCustomType(
        [FromBody] AddCustomTypeRequest request
        )
    {
        TypeRegistry.RegisterType(request.Name, request.Structure);
        await Task.Delay(0);
        return Results.Ok();
    }
}