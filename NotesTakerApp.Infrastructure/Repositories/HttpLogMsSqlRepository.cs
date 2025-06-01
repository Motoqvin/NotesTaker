using NotesTakerApp.Core.Models;
using NotesTakerApp.Core.Repositories;

namespace NotesTakerApp.Infrastructure.Repositories;
public class HttpLogMsSqlRepository : IHttpLogRepository
{
    public Task InsertAsync(HttpLog log)
    {
        throw new NotImplementedException();
    }
}