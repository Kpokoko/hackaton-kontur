namespace hahaton.Endpoints;

public static class MockEndpoints
{
    public static IEndpointRouteBuilder MapMockEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("api/");

        endpoints.MapGet("mock", FillMockObject);
        
        return endpoints;
    }

    private static async Task<IResult> FillMockObject(
        //[FromBody] MockRequest mockRequest
        //MockService mockService
    )
    {
       // var filledMock = await mockService.Create(mockRequest);
        await Task.Delay(0);
        return Results.Ok();
    }
}