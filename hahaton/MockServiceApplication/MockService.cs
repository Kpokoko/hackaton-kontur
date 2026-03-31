using System.Dynamic;
using System.Text.Json;
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
                    var array = mockService.Generate(field.Format, null, field.Count, field.valueType, null);
                    expando[field.Name] = array.Split(",");
                } else if (field.Type == "dictionary")
                {
                    var dictionary = mockService.Generate(field.Format, field.FormatKey, field.Count, field.valueType, field.keyType);

                    expando[field.Name] = JsonSerializer.Deserialize<Dictionary<string, object>>(dictionary);
                }
                else if (field.Type == "double")
                {
                    var number = mockService.Generate(field.Format, field.FormatKey, field.Count, field.valueType, field.keyType);
                    expando[field.Name] = double.Parse(number);
                }
                else if (field.Type == "int")
                {
                    var number = mockService.Generate(field.Format, field.FormatKey, field.Count, field.valueType, field.keyType);
                    expando[field.Name] = int.Parse(number);
                }
                else
                {
                    expando[field.Name] = mockService.Generate(field.Format, null, field.Count, field.valueType, null);
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