using WebApi.Controllers;
using WebApi.Persistence.Model;

namespace WebApi.Persistence;

public interface ICoasterRepository
{
    IEnumerable<Coaster> GetAll(CoasterFilter filter);
    IEnumerable<Coaster> Get(CoasterParameters parameters, CoasterFilter filter);
}