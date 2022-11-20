using WebApi.Controllers;
using WebApi.Persistence.Model;

namespace WebApi.Persistence;

public interface ICoasterRepository
{
    IEnumerable<Coaster> GetAll();
    IEnumerable<Coaster> Get(CoasterParameters parameters);
}