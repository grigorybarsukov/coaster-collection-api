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

    public IEnumerable<Coaster> GetAll(CoasterFilter filter)
    {
        var query = Coasters.AsQueryable();
        query = ApplyFilter(filter, query);
        return query.ToList();
    }

    public IEnumerable<Coaster> Get(CoasterParameters parameters, CoasterFilter filter)
    {
        var query = Coasters.AsQueryable();
        query = ApplySort(parameters, query);
        query = ApplyFilter(filter, query);
        query = ApplyPagination(parameters, query);
        return query.ToList();
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

    private IQueryable<Coaster> ApplyPagination(CoasterParameters parameters, IQueryable<Coaster> query)
    {
        if (parameters.PageSize.HasValue == false)
        {
            return query;
        }

        return query.Skip((parameters.PageNumber - 1) * parameters.PageSize.Value)
            .Take(parameters.PageSize.Value);
    }

    private IQueryable<Coaster> ApplyFilter(CoasterFilter filter, IQueryable<Coaster> query)
    {
        if (filter.Type is not null)
        {
            query = query.Where(c => filter.Type.Contains(c.Type));
        }
        if (filter.Brand is not null)
        {
            query = query.Where(c => filter.Brand.Contains(c.Brand));
        }
        if (filter.Kind is not null)
        {
            query = query.Where(c => filter.Kind.Contains(c.Kind));
        }
        if (filter.Country is not null)
        {
            query = query.Where(c => filter.Country.Contains(c.Country));
        }
        if (filter.Shape is not null)
        {
            query = query.Where(c => filter.Shape.Contains(c.Shape));
        }
        if (filter.Reverse is not null)
        {
            query = query.Where(c => filter.Reverse == c.Reverse);
        }
        if (filter.Type is not null)
        {
            query = query.Where(c => filter.Type.Contains(c.Type));
        }

        return query;
    }
}