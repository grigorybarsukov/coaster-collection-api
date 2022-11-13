using FileHelpers;

namespace WebApi.Domain;

[DelimitedRecord(",")]
public class Coaster
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string Brand { get; set; }
    public string Kind { get; set; }
    public string Country { get; set; }
    public string Shape { get; set; }
    public string Reverse { get; set; }
    public IEnumerable<string> BeerType { get; set; }

    public Coaster(CoasterRecord record)
    {
        Id = record.Id;
        Type = record.Type;
        Brand = record.Brand;
        Kind = record.Kind;
        Country = record.Country;
        Shape = record.Shape;
        Reverse = record.Reverse;
        
        var beerType = new List<string> { record.Hierarchy };
        if (string.IsNullOrEmpty(record.Hierarchy2) == false)
        {
            beerType.Add(record.Hierarchy2);
        }
        if (string.IsNullOrEmpty(record.Hierarchy3) == false)
        {
            beerType.Add(record.Hierarchy3);
        }
        if (string.IsNullOrEmpty(record.Hierarchy4) == false)
        {
            beerType.Add(record.Hierarchy4);
        }
        if (string.IsNullOrEmpty(record.Hierarchy5) == false)
        {
            beerType.Add(record.Hierarchy5);
        }

        BeerType = beerType;
    }
}