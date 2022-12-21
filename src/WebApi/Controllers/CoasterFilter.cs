namespace WebApi.Controllers;

public class CoasterFilter
{
    public IEnumerable<string>? Type { get; set; }
    public IEnumerable<string>? Brand { get; set; }
    public IEnumerable<string>? Kind { get; set; }
    public IEnumerable<string>? Country { get; set; }
    public IEnumerable<string>? Shape { get; set; }
    public string? Reverse { get; set; }
}