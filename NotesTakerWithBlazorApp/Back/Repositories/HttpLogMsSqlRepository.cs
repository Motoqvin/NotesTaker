using Back.Models;
using Back.Repositories.Base;

namespace Back.Repositories;
public class HttpLogMsSqlRepository : IHttpLogRepository
{
    public Task InsertAsync(HttpLog log)
    {
        throw new NotImplementedException();
    }
}