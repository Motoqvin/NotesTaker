using Back.Models;

namespace Back.Repositories.Base;
public interface IHttpLogRepository
{
    public Task InsertAsync(HttpLog log);
}