using System.CodeDom.Compiler;
using System.Dynamic;
using System.Text.Json;

namespace MockServiceApplication;

public class Builder
{
    public dynamic Serialize(string json)
    {
        var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
        dynamic obj = new ExpandoObject();
        obj = FromDictionary(dict, obj);
        var resultedJson = JsonSerializer.Serialize<dynamic>(obj);
        return resultedJson;
    }

    public dynamic FromDictionary(Dictionary<string, object> dict, dynamic obj)
    {
        var child = new ExpandoObject();
        var objDict = (IDictionary<string, object>)obj;
        var childDict = (IDictionary<string, object>)child;
        string name = null, type = null, format = null;
        Dictionary<string, object>[] props = null;
        
        foreach (var kvp in dict)
            switch (kvp.Key)
            {
                case "name": 
                    name = dict[kvp.Key].ToString();
                    break;
                case "type":
                    type = dict[kvp.Key].ToString();
                    break;
                case "props":
                    props = JsonSerializer.Deserialize<Dictionary<string, object>[]>(kvp.Value.ToString());
                    break;
                default:
                    break;
            }

        if (name != null)
        {
            if (props != null && type == "class")
            {
                foreach (var prop in props)
                {
                    child = FromDictionary(prop, child);
                }
                objDict[name] = child;
            }

            if (type != "class")
            {
                objDict[name] = Generate(type, format);
            }
        }
        
        return obj;
    }
}