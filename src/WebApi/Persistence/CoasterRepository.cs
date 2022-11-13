using System.Reflection;
using FileHelpers;
using WebApi.Controllers;
using WebApi.Domain;

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
        using var textReader = new StreamReader(stream);
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
        return Coasters
            .OrderBy(on => on.Id)
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .ToList();
    }
}