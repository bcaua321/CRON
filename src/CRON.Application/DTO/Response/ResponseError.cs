using System.Text.Json.Serialization;

namespace CRON.Application.DTO.Response;

// To help with error
public class ResponseError
{
    [JsonIgnore]
    public bool Sucess { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string> Errors { get; set; }

    public ResponseError(string error)
    {
        Sucess = false;
        AddError(error);
    }

    private void AddError(string message)
    {
        if(Errors is null)
            Errors = new List<string>();
        Errors.Add(message);
    }
}