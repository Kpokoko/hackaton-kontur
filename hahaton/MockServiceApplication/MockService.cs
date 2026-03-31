using System.Dynamic;
using MockServiceApplication.DTOs;
using MockServiceApplication.MockServices;

namespace MockServiceApplication;

public class MockService
{
    private readonly Func<string, IMockService?> _mockResolver;

    public MockService(Func<string, IMockService?> mockResolver)
    {
        _mockResolver = mockResolver;
    }

    public dynamic Generate(MockRequest mockRequest)
    {
        var expando = new ExpandoObject() as IDictionary<string, object?>;

        FillObject(mockRequest.Structure, expando);

        return expando;
    }

    private void FillObject(Structure[] structure, IDictionary<string, object?> expando)
    {
        foreach (var field in structure)
        {
            var mockService = _mockResolver(field.Type);
            
            
            if (mockService is null)
            {
                var nestedStructure = TypeRegistry.GetTypeStructure(field.Type);
                if (nestedStructure != null)
                {
                    var nestedExpando = new ExpandoObject() as IDictionary<string, object>;
                    FillObject(nestedStructure, nestedExpando);
                    expando[field.Name] = nestedExpando;
                }
                else
                {
                    expando[field.Name] = null;
                }
            }
            else
            {
                if (field.Type == "array")
                {
                    var array = mockService.Generate(field.Format, field.Count, field.valueType);
                    expando[field.Name] = array.Split(",");
                }
                else
                {
                    expando[field.Name] = mockService.Generate(field.Format, field.Count, field.valueType);
                }
            }
        }
    }
}

public static class TypeRegistry
{
    private static readonly Dictionary<string, Structure[]> _types = new();

    public static void RegisterType(string typeName, Structure[] structure)
    {
        if (!_types.ContainsKey(typeName))
        {
            _types[typeName] = structure;
        }
    }

    public static Structure[]? GetTypeStructure(string typeName)
    {
        _types.TryGetValue(typeName, out var structure);
        return structure;
    }
}