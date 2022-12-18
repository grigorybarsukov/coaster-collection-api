using FileHelpers;
// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CS8618

namespace WebApi.Persistence.Model;

[DelimitedRecord(",")]
[IgnoreFirst]
public class CoasterRecord
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string Brand { get; set; }
    public string Kind { get; set; }
    public string Country { get; set; }
    public string Shape { get; set; }
    public string Reverse { get; set; }
    public string Hierarchy { get; set; }
    public string Hierarchy2 { get; set; }
    public string Hierarchy3 { get; set; }
    public string Hierarchy4 { get; set; }
    public string Hierarchy5 { get; set; }
    public string DummyField { get; set; }
    public string DummyField2 { get; set; }
    public string DummyField3 { get; set; }
    public string DummyField4 { get; set; }
}