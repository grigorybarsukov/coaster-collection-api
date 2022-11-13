using WebApi.Controllers;
using WebApi.Domain;

namespace WebApi.Persistence;

public interface ICoasterRepository
{
    IEnumerable<Coaster> GetAll();
    IEnumerable<Coaster> Get(CoasterParameters parameters);
}