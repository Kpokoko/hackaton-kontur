using MockServiceApplication;

namespace MockServiceTests;

public class Tests
{

    [Test]
    public void Simple()
    {
        new Builder().Serialize(
            "{\n    \"name\": \"Person\",\n    \"type\": \"class\",\n    \"props\":\n    [\n        {\n            \"name\": \"FirstName\",\n            \"type\": \"string\"\n        },\n        {\n            \"name\": \"LastName\",\n            \"type\": \"string\"\n        }\n    ]\n}"
            );
    }
    
    [Test]
    public void Hard()
    {
        new Builder().Serialize(
            "{\n    \"name\": \"Person\",\n    \"type\": \"class\",\n    \"props\":\n    [\n        {\n            \"name\": \"FirstName\",\n            \"type\": \"string\"\n        },\n        {\n            \"name\": \"LastName\",\n            \"type\": \"string\"\n        },\n        {\n            \"name\": \"Child\",\n            \"type\": \"class\",\n            \"props\":\n            [\n                {\n                    \"name\": \"FirstName\",\n                    \"type\": \"string\"\n                },\n                {\n                    \"name\": \"LastName\",\n                    \"type\": \"string\"\n                }\n            ]\n        }\n\n    ]\n}"
            );
    }
}