using System.Reflection;
using FileHelpers;
using WebApi.Controllers;
using WebApi.Persistence.Model;

namespace WebApi.Persistence;

public class CoasterRepository : ICoasterRepository
{
    private const string DbResourceName = "WebApi.Persistence.DB.csv";
    private IEnumerable<Coaster> Coasters { get; }

    public CoasterRepository()
    {
        var fileHelperEngine = new FileHelperEngine<CoasterRecord>();
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream(DbResourceName);
        using var textReader = new StreamReader(stream!);
        var stringDb = textReader.ReadToEnd();
        var records = fileHelperEngine.ReadString(stringDb);

        Coasters = records.Select(r => new Coaster(r));
    }

    public IEnumerable<Coaster> GetAll()
    {
        return Coasters;
    }

    public IEnumerable<Coaster> Get(CoasterParameters parameters)
    {
        var query = Coasters.AsQueryable();

        query = ApplySort(parameters, query);

        return query
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .ToList();
    }

    private IQueryable<Coaster> ApplySort(CoasterParameters parameters, IQueryable<Coaster> query)
    {
        if (string.IsNullOrEmpty(parameters.SortBy) && string.IsNullOrEmpty(parameters.SortByDescending))
        {
            return query.OrderBy(c => c.Id);
        }

        var propertyInfo = typeof(Coaster).GetProperty(parameters.SortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        
        return string.IsNullOrEmpty(parameters.SortByDescending)
            ? query.OrderBy(c => propertyInfo == null ? c.Id : propertyInfo.GetValue(c))
            : query.OrderByDescending(c => propertyInfo == null ? c.Id : propertyInfo.GetValue(c));
    }
}